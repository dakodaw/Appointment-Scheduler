using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Software2Project_DakodaWillden_V1.Classes;

namespace Software2Project_DakodaWillden_V1
{
    public partial class Frm_AddAppointment : Form
    {
        //Class Variables
        string connecterString = "Persist Security Info=False;server=52.206.157.109;database=U04MmA;uid=U04MmA;password=53688281290;SslMode=none";
        Customer c;
        List<Customer> customers;
        public List<Appointment> appointments;
        public int selectedAppointmentId { get; set; } = 0;
        public Appointment selectedAppointment;
        public string loggedInUser { get; set; }
        public int loggedInUserId { get; set; } = 0;
        public bool appointmentAdded { get; set; } = false;

        //location strings
        string phoenixLocation = "Phoenix, Arizona";
        string newYorkLocation = "New York, New York";
        string londonLocation = "London, England";

        int selectedCustomerId = 0;
        public string selectedTimeZone { get; set; }
        public string timeZoneName { get; set; }


        //Constructors/Loaders
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Frm_AddAppointment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads Needed information when form is loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAppointment_Load(object sender, EventArgs e)
        {
            timeZoneName = selectedTimeZone;
            using (var conn = new MySqlConnection(connecterString))
            {
                //Load list of customers
                customers = new List<Customer>();
                string getCustomersQuery = "SELECT * FROM customer WHERE active = 1";
                var reader = DataChanges.GetMySqlReader(conn, getCustomersQuery);
                while (reader.Read())
                {
                    int customerId = (int)reader["customerId"];
                    string customerName = reader["customerName"].ToString();
                    int addressId = (int)reader["addressId"];
                    bool active = (bool)reader["active"];
                    string createdBy = reader["createdBy"].ToString();
                    string lastUpdateBy = reader["lastUpdateBy"].ToString();
                    Customer customer = new Customer(customerId, customerName, addressId, active, createdBy, lastUpdateBy);
                    customers.Add(customer);
                }
                conn.Close();

                foreach (var customer in customers)
                {
                    comboBox_CustomerSelect.Items.Add(customer.customerID.ToString() + " - " + customer.customerName);
                }
                //Fill in Locations Dropdown with a few Appointment Types for the consulting company. Sales Call,   
                comboBox_SelectLocation.Items.Add(phoenixLocation);
                comboBox_SelectLocation.Items.Add(newYorkLocation);
                comboBox_SelectLocation.Items.Add(londonLocation);

                /*Fill in Appointment Types Dropdown
                    - Introduction call(from referral/ Sign Up from sales department/ website)
                    - Regular Consulting Meeting
                    - Evaluation Meeting
                    - Discharge Meeting
                */
                comboBox_AppointmentType.Items.Add("Introduction Call");
                comboBox_AppointmentType.Items.Add("Regular Consulting Meeting");
                comboBox_AppointmentType.Items.Add("Evaluation Meeting");
                comboBox_AppointmentType.Items.Add("Discharge Meeting");

                if (selectedAppointmentId != 0)
                {
                    //set to update if appointment was selected to edit
                    Text = "Update Appointment";
                    Btn_Add.Text = "Update";

                    //Fill in form
                    Txt_AppointmentId.Text = selectedAppointment.appointmentId.ToString();
                    Txt_Title.Text = selectedAppointment.title;
                    Txt_Description.Text = selectedAppointment.description;

                    var location = selectedAppointment.location;


                    TimeZoneInfo selectedTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneName);
                    dateTimePicker_AppointmentDateStartTime.Text = (TimeZoneInfo.ConvertTimeFromUtc(selectedAppointment.start,selectedTimeZone)).ToString();
                    dateTimePicker_AppointmentDateEndTime.Text = (TimeZoneInfo.ConvertTimeFromUtc(selectedAppointment.end,selectedTimeZone)).ToString();
                    comboBox_AppointmentType.Text = selectedAppointment.type.ToString();
                    comboBox_CustomerSelect.Text = selectedAppointment.customerObject.customerName;
                    comboBox_SelectLocation.Text = selectedAppointment.location;
                }
            }
        }


        //Event Handlers
        /// <summary>
        /// Closes the form when pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Attempts to Add or Update an Appointment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                //Throw an error if any information for an appointment is missing
                string location;
                string appointmentType;
                try
                {
                    //
                    if (selectedAppointmentId == 0 || comboBox_CustomerSelect.Text != selectedAppointment.customerObject.customerName)
                    {
                        int newIndex = comboBox_CustomerSelect.SelectedIndex;
                        c = customers.ElementAt(newIndex);
                        selectedCustomerId = c.customerID;
                    }
                    else
                    {
                        c = selectedAppointment.customerObject;
                        selectedCustomerId = c.customerID;
                    }
                }
                catch
                {
                    throw new Exception("Please select a customer to schedule a meeting");
                }
                try
                {
                    location = comboBox_SelectLocation.SelectedItem.ToString();
                }
                catch
                {
                    throw new Exception("Please select a location to hold meeting");
                }
                try
                {
                    appointmentType = comboBox_AppointmentType.SelectedItem.ToString();
                }
                catch
                {
                    throw new Exception("Please select an appointment type");
                }
                //Capture Start and end times and make sure that neither are outside the bounds of the office hours
                //  of the time zone of the given location

                //    //Show the user the current time within the Form once it is logged in
                TimeZoneInfo selectedTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneName);

                var inputStart = Convert.ToDateTime(dateTimePicker_AppointmentDateStartTime.Text);
                var inputEnd = Convert.ToDateTime(dateTimePicker_AppointmentDateEndTime.Text);

                //Convert times from selected time zone to Universal Time Zone
                DateTime startTime = TimeZoneInfo.ConvertTimeToUtc(inputStart, selectedTimeZone);
                DateTime endTime = TimeZoneInfo.ConvertTimeToUtc(inputEnd, selectedTimeZone);
                try
                {
                    //Check Time Errors with Time in selected time zone.
                    checkTimeZoneTime(timeZoneName, inputStart, inputEnd, selectedAppointmentId);
                }
                catch(Exception exc)
                {
                    throw exc;
                }
                //throw an error if endtime is earlier than start time
                if (endTime < startTime)
                {
                    throw new Exception("An appointment cannot end before it begins");
                }
                //throw error if text or description are empty
                if(Txt_Title.Text == "" || Txt_Description.Text == "")
                {
                    throw new Exception("Please Enter a Title and Description");
                }
                // Add Escape Characters to any single or double quotes to the string so that the quotes can be entered into the Database 
                string singleQuoteToReplace = "\'";
                string singleReplaceWith = "\\\'";
                string doubleQuoteToReplace = "\"";
                string doubleReplaceWith = "\\\"";
                Regex reg1 = new Regex(singleQuoteToReplace);
                Regex reg2 = new Regex(doubleQuoteToReplace);

                //  For Title:
                string titleToUse = "";
                if (Txt_Title.Text.Contains(@"\"))
                {
                    titleToUse = Txt_Title.Text;
                }
                else
                {
                    string titleBegin = reg1.Replace(Txt_Title.Text, singleReplaceWith);
                    titleToUse = reg2.Replace(titleBegin, doubleReplaceWith);
                }

                //  For Description:
                string descriptionToUse = "";
                if (Txt_Description.Text.Contains(@"\"))
                {
                    descriptionToUse = Txt_Description.Text;
                }
                else
                {
                    string descriptionBegin = reg1.Replace(Txt_Description.Text, singleReplaceWith);
                    descriptionToUse = reg2.Replace(descriptionBegin, doubleReplaceWith);
                }
                //If the ID is 0, Add a new Appointment, If it is another ID, update the given appointment ID
                string successMessagePart = "Appointment Scheduling Successfully ";
                if (selectedAppointmentId == 0)
                {
                    Appointment newAppointment = new Appointment(selectedCustomerId, loggedInUserId, titleToUse, descriptionToUse, location, loggedInUser, "", startTime, endTime, loggedInUser, loggedInUser, appointmentType);
                    successMessagePart += "Added";
                    appointments.Add(newAppointment);
                }
                else
                {
                    Appointment updatedAppointment = new Appointment("UPDATE", selectedAppointmentId, selectedCustomerId, loggedInUserId, titleToUse, descriptionToUse, location, selectedAppointment.contact, "", startTime, endTime, selectedAppointment.createdBy, selectedAppointment.createDate, loggedInUser, DateTime.Now, appointmentType, loggedInUser);
                    successMessagePart += "Updated for Appointment ID " + selectedAppointmentId.ToString();
                    
                    //Update Values in appointment object with values
                    var checkAppointment = appointments.Where(i => i.appointmentId == updatedAppointment.appointmentId).First();
                    int appointmentIndex = appointments.IndexOf(checkAppointment);
                    appointments[appointmentIndex] = updatedAppointment;
                    List<Appointment> newAppointmentsList = new List<Appointment>();
                    //Add all the appointments to a new list, and replace the appointment with the id being updated with the updated appointment
                    var j = 0;
                    foreach (var appointment in appointments)
                    {
                        if (j != appointmentIndex)
                            newAppointmentsList.Add(appointments[j]);
                        else
                        {
                            newAppointmentsList.Add(updatedAppointment);
                        }
                        j++;
                    }
                }
         

                string successMessage = successMessagePart + " \n Here are the Details:"
                    + "\n Appointment Type: " + appointmentType
                    + "\n Appointment With: " + c.customerName + "\n Location: " + location 
                    + "\n Start Time: " + startTime.ToLocalTime().ToString() + " In Local Time\n End Time: " 
                    + endTime.ToLocalTime().ToString() + " In Local Time\n Title: " + Txt_Title.Text + "\n Description: " 
                    + Txt_Description.Text;
                MessageBox.Show(successMessage);

                //Set the appointmentAdded bool to true so we know whether or not to add this appointment to the list and table
                appointmentAdded = true;
                Close();
            }
            catch(Exception ee)
            {
                //Show user the error that occurred
                string exception2 = ee.Message;
                MessageBox.Show("One or more errors occured:\n" + exception2);
            }

        }

        /// <summary>
        /// Opens Customer information form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ViewCustInfo_Click(object sender, EventArgs e)
        {
            //Open the Customer form with the Customer information from the Appointment Form
            Frm_AddCustomer editCustomer = new Frm_AddCustomer();
            editCustomer.selectedCustomer = selectedCustomerId;
            editCustomer.loggedInUser = loggedInUser;
            editCustomer.connecterString = connecterString;
            editCustomer.ShowDialog();
        }

        /// <summary>
        /// Gets the Customer ID When a customer is selected, and shows the view customer info button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_CustomerSelect_DropDownClosed(object sender, EventArgs e)
        {
            //Get the ID to link customer information to appointment form
            if (selectedAppointmentId == 0 || comboBox_CustomerSelect.Text != selectedAppointment.customerObject.customerName)
            {
                int newIndex = comboBox_CustomerSelect.SelectedIndex;
                c = customers.ElementAt(newIndex);
                selectedCustomerId = c.customerID;
            }
            else
            {
                c = selectedAppointment.customerObject;
                selectedCustomerId = c.customerID;
            }
            Btn_ViewCustInfo.Visible = true;
        }

        /// <summary>
        /// Check times and throw errors if it is out of the open hours for a given location, if the end time is before start, etc
        /// </summary>
        /// <param name="TimeZone"></param>
        /// <param name="GivenStartTime"></param>
        /// <param name="GivenEndTime"></param>
        /// <param name="appointmentId"></param>
        public void checkTimeZoneTime(string TimeZone, DateTime GivenStartTime, DateTime GivenEndTime, int appointmentId)
        {
            TimeZoneInfo selectedTimeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZone);
            //DateTime selectedTime = TimeZoneInfo.ConvertTimeFromUtc(GivenStartTime, selectedTimeZone);
            //Don't allow user to select a time before now
            DateTime minDateTime = DateTime.Now;
            //Set Max and Min time to compare given time 
            int minTime = 9;
            int maxTime = 17;
            string errorMessage = "";
            if (GivenStartTime > minDateTime)
            {
                if (GivenStartTime.Hour >= minTime)
                {
                    if (GivenEndTime.Hour <= maxTime)
                    {
                        if(GivenStartTime.Hour == maxTime)
                        {
                            errorMessage = "Cannot begin appointment at closing time";
                            throw new Exception(errorMessage);
                        }
                    }
                    else
                    {
                        errorMessage = "Appointment cannot extend past " + maxTime.ToString() + " in local time for "
                            + selectedTimeZone + " Time Zone";
                        throw new Exception(errorMessage);
                    }
                }
                else
                {
                    errorMessage = "Cannot schedule earlier than " + minTime.ToString() + " in local time for "
                            + selectedTimeZone + " Time Zone";
                    throw new Exception(errorMessage);
                }
            }
            else
            {
                errorMessage = "Cannot schedule appointment before now";
                throw new Exception(errorMessage);
            }
            //Throw an error when Appointment overlaps with other appointments
            //List<Appointment> checkAppointments = new List<Appointment>();
            //var checkAppointment = appointments.Where(i => i.start.Equals(GivenStartTime));
            foreach(var appt in appointments)
            {
                if (appt.appointmentId != appointmentId)
                {
                    string exceptionMessage = "Cannot schedule an appointment at this time. There is already an appointment scheduled between "
                    + appt.start.ToString() + " and " + appt.end.ToString();
                    if (appt.start == GivenStartTime)
                    {
                        throw new Exception(exceptionMessage);
                    }
                    if (appt.start < GivenStartTime && GivenStartTime < appt.end)
                    {
                        throw new Exception(exceptionMessage);
                    }
                    if (GivenEndTime < appt.end && GivenEndTime > appt.start)
                    {
                        throw new Exception(exceptionMessage);
                    }
                }
            }
            
            //If the start is greater than or equal to start, and smaller than end,
            //If the end is greater than start or smaller than end
        }
    }
}
