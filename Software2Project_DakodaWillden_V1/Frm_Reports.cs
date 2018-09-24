using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Software2Project_DakodaWillden_V1.Classes;
using MySql.Data.MySqlClient;

namespace Software2Project_DakodaWillden_V1
{
    public partial class Frm_Reports : Form
    {
        //Set up Lists, and Variables to use in this form
        public List<Appointment> appointments = new List<Appointment>();
        public List<Customer> customers;
        public List<User> users = new List<User>();
        public List<string> reportLogs = new List<string>();

        public string loggedInUser { get; set; } = "";
        public string connecterString { get; set; } = "";

        string reportAppointmentMonthOption = "Number Of Appointments By Month";
        string reportConsultantScheduleOption = "Schedule By Consultant";
        string reportCustomerAppointmentsOption = "Summary of Appointments By Customer";

        string selectMonth = "--Select A Month--";
        string selectConsultant = "--Select a Consultant--";
        string selectCustomer = "--Select a Customer--";
        
        //Constructors/Form Loader
        /// <summary>
        /// Constructor
        /// </summary>
        public Frm_Reports()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads all the lists and variables needed to create reports
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_Reports_Load(object sender, EventArgs e)
        {
            comboBox_SelectReport.Items.Add(reportAppointmentMonthOption);
            comboBox_SelectReport.Items.Add(reportConsultantScheduleOption);
            comboBox_SelectReport.Items.Add(reportCustomerAppointmentsOption);

            //Load all appointments into Lists to be able to search them quickly once this loads
            using (var conn = new MySqlConnection(connecterString))
            {
                string getAppointments = "SELECT * FROM appointment";
                var reader = DataChanges.GetMySqlReader(conn, getAppointments);
                while (reader.Read())
                {
                    //Fill Appointment Object Minus the Customer object
                    Appointment appointment = new Appointment();
                    appointment.appointmentId = (int)reader["appointmentId"];
                    appointment.customerId = (int)reader["customerId"];
                    appointment.title = reader["title"].ToString();
                    appointment.description = reader["description"].ToString();
                    appointment.location = reader["location"].ToString();
                    appointment.contact = reader["contact"].ToString();
                    appointment.start = Convert.ToDateTime(reader["start"]);
                    appointment.end = Convert.ToDateTime(reader["end"]);
                    appointment.createDate = Convert.ToDateTime(reader["createDate"]);
                    appointment.createdBy = reader["createdBy"].ToString();
                    appointment.lastUpdate = Convert.ToDateTime(reader["lastUpdate"]);
                    appointment.lastUpdateBy = reader["lastUpdateBy"].ToString();
                    appointment.type = reader["appointmentType"].ToString();
                    appointment.userId = (int)reader["userId"];
                    
                    //Add Appointment to list of Appointments one by one
                    appointments.Add(appointment);
                }
                reader.Close();
                foreach(var appointment in appointments)
                {
                    appointment.customerObject = customers.Where(i => i.customerID == appointment.customerId).First();
                }
                
                //Load all Users into a list to be able to search those quickly
                string getUsersQuery = "SELECT * FROM user";
                var usersReader = DataChanges.GetMySqlReader(conn, getUsersQuery);
                while (usersReader.Read())
                {
                    User readUser = new User();
                    readUser.userId = (int)usersReader["userId"];
                    readUser.userName = usersReader["userName"].ToString();
                    users.Add(readUser);
                }
                usersReader.Close();
            }
            //Load all Customer Names and ID's (This is being done in Main Form when this form is created)
        }

        //Event Handlers
        /// <summary>
        /// Closes the form when button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Changes Report Options based on what is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_SelectReport_TextChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("This Event works: " + comboBox_SelectReport.Text);
            //Based on what is selected, change the second dropdown
            if(comboBox_SelectReport.Text == reportAppointmentMonthOption)
            {
                comboBox_VariableSelections.Items.Clear();
                comboBox_VariableSelections.Items.Add("January");
                comboBox_VariableSelections.Items.Add("February");
                comboBox_VariableSelections.Items.Add("March");
                comboBox_VariableSelections.Items.Add("April");
                comboBox_VariableSelections.Items.Add("May");
                comboBox_VariableSelections.Items.Add("June");
                comboBox_VariableSelections.Items.Add("July");
                comboBox_VariableSelections.Items.Add("August");
                comboBox_VariableSelections.Items.Add("September");
                comboBox_VariableSelections.Items.Add("October");
                comboBox_VariableSelections.Items.Add("November");
                comboBox_VariableSelections.Items.Add("December");
                comboBox_VariableSelections.Text = selectMonth;
                comboBox_VariableSelections.Enabled = true;
                Btn_CreateReport.Enabled = false;
            }
            else if(comboBox_SelectReport.Text == reportConsultantScheduleOption)
            {
                comboBox_VariableSelections.Items.Clear();
                foreach(var consultant in users)
                {
                    comboBox_VariableSelections.Items.Add(consultant.userName);
                }
                comboBox_VariableSelections.Text = selectConsultant;
                comboBox_VariableSelections.Enabled = true;
                Btn_CreateReport.Enabled = false;
            }
            else if(comboBox_SelectReport.Text == reportCustomerAppointmentsOption)
            {
                comboBox_VariableSelections.Items.Clear();
                foreach(var customer in customers)
                {
                    comboBox_VariableSelections.Items.Add(customer.customerName);
                }
                comboBox_VariableSelections.Text = selectCustomer;
                comboBox_VariableSelections.Enabled = true;
                Btn_CreateReport.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please Choose a Report");
            }
        }

        /// <summary>
        /// Creates Reports based on what option is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_CreateReport_Click(object sender, EventArgs e)
        {
            if(comboBox_SelectReport.Text == reportAppointmentMonthOption)
            {
                //Add the report viewed to logs:
                string reportViewedLog = "[" + DateTime.Now.ToString() + "]-" + loggedInUser + " viewed the "
                    + reportAppointmentMonthOption + " report";
                reportLogs.Add(reportViewedLog);

                //find the month int from the month selected
                int selectedMonthNo = comboBox_VariableSelections.SelectedIndex + 1;

                //use the month to find the appointments that have that month
                var foundAppointments = appointments.Where(i => i.start.Month == selectedMonthNo);
                //put them on the main textbox to show the report
                if(foundAppointments.Count() > 0)
                {
                    string appointmentTypeIntro = "Introduction Call";
                    string appointmentTypeRegular = "Regular Consulting Meeting";
                    string appointmentTypeEval = "Evaluation Meeting";
                    string appointmentTypeDischarge = "Discharge Meeting";

                    int introCount = 0;
                    int regularCount = 0;
                    int evalCount = 0;
                    int dischargeCount = 0;

                    //Use Lambda here instead of looping through just to count
                    introCount = foundAppointments.Where(i => i.type == appointmentTypeIntro).Count();
                    regularCount = foundAppointments.Where(i => i.type == appointmentTypeRegular).Count();
                    evalCount = foundAppointments.Where(i => i.type == appointmentTypeEval).Count();
                    dischargeCount = foundAppointments.Where(i => i.type == appointmentTypeDischarge).Count();

                    //Set up the report to show
                    string textToReport = "Here are the totals of each appointment type for " + comboBox_VariableSelections.Text
                       + ":\r\nNumber of Total Appointments: " + foundAppointments.Count().ToString()
                       + "\r\nIntroduction Appointments: " + introCount.ToString()
                       + "\r\nRegular Consulting Appointments: " + regularCount.ToString()
                       + "\r\nEvaluation Appointments: " + evalCount.ToString()
                       + "\r\nDischarge Appointments: " + dischargeCount.ToString();
                    //Show Report
                    Txt_ShowReport.Text = textToReport;
                }
                else
                {
                    Txt_ShowReport.Text = "No Results Found";
                }

            }
            else if(comboBox_SelectReport.Text == reportConsultantScheduleOption)
            {
                //Add the report viewed to logs:
                string reportViewedLog = "[" + DateTime.Now.ToString() + "]-" + loggedInUser + " viewed the "
                    + reportConsultantScheduleOption + " report";
                reportLogs.Add(reportViewedLog);

                //Set up string for report
                string textToReport = "Here is the schedule for consultant " + comboBox_VariableSelections.Text + ": ";
                //Get the index of the selected text
                int selectedConsultantIndex = comboBox_VariableSelections.SelectedIndex;
                //Get the consultant's ID
                int selectedUserId = users[selectedConsultantIndex].userId;
                var foundAppointments = appointments.Where(i => i.userId == selectedUserId);
                if (foundAppointments.Count() > 0)
                { 
                    //Get the list of appointments, and add them to the string when it cycles through each consultant
                    foreach (var appt in foundAppointments)
                    {
                        textToReport += "\r\nMeeting with " + appt.customerObject.customerName 
                            + " from " + appt.start.ToString()
                            + " to " + appt.end.ToString() + " - " + appt.type + ": " + appt.title;
                    }
                    Txt_ShowReport.Text = textToReport;
                }
                else
                {
                    Txt_ShowReport.Text = "No Results Found";
                }
            }
            else if(comboBox_SelectReport.Text == reportCustomerAppointmentsOption)
            {
                //Add the report viewed to logs:
                string reportViewedLog = "[" + DateTime.Now.ToString() + "]-" + loggedInUser + " viewed the "
                    + reportCustomerAppointmentsOption + " report";
                reportLogs.Add(reportViewedLog);

                //Get Customer ID
                int indexOfSelection = comboBox_VariableSelections.SelectedIndex;
                int customerId = customers[indexOfSelection].customerID;

                var foundAppointments = appointments.Where(i => i.customerId == customerId);
                if (foundAppointments.Count() > 0)
                {

                    //Set up report string
                    string textToReport = "Here is the schedule for " + comboBox_VariableSelections.Text + ": ";

                    //Get appointments matching the customer
                    foreach (var appt in foundAppointments)
                    {
                        textToReport += "\r\nMeeting with " + users.Where(i => i.userId == appt.userId).First().userName
                            + " from " + appt.start.ToString()
                            + " to " + appt.end.ToString() + " - " + appt.type + ": " + appt.title;
                    }
                    //Fill the Textbox
                    Txt_ShowReport.Text = textToReport;
                }
                else
                {
                    Txt_ShowReport.Text = "No results found";
                }
            }
            else
            {
                MessageBox.Show("Please select a report option");
            }
        }

        /// <summary>
        /// Enables the button to create a report if the text isn't the please select messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_VariableSelections_TextChanged(object sender, EventArgs e)
        {
            if(comboBox_VariableSelections.Text != selectMonth && comboBox_VariableSelections.Text != selectConsultant && comboBox_VariableSelections.Text != selectCustomer)
            {
                Btn_CreateReport.Enabled = true;
            }
        }
    }
}
