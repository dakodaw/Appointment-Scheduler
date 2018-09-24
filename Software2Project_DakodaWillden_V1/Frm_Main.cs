//Version 4 Scheduling software 
//
//FOR: Tom Weidner
//
//
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Software2Project_DakodaWillden_V1.Classes;
using System.IO;
using System.Globalization;
using Windows.Devices.Geolocation;

namespace Software2Project_DakodaWillden_V1
{
    public partial class Frm_Main : Form
    {
        // Set Class Variables
        User user1 = new User();
        string connecterString = "Persist Security Info=False;server=52.206.157.109;database=U04MmA;uid=U04MmA;password=53688281290;SslMode=none";
        string dataPassword = "";
        string userName = "";
        string loggedInUser = "";
        string passwordIncorrectAlert = "";
        string userNameNonExistantAlert = "";
        string locationMessage = "";
        string welcomeMessage = "";
        int loggedInUserId = 0;
        string getAllUsersQuery = "SELECT * FROM user";
        string userLanguage = "";
        string timeZoneLocation = "";

        //Language Options
        string englishSetting = "English";
        string portugueseSetting = "Portuguese";

        //location strings
        string phoenixLocation = "Phoenix, Arizona";
        string newYorkLocation = "New York, New York";
        string londonLocation = "London, England";

        public string mySelectedTimeZoneString { get; set; }
        DataChanges db = new DataChanges();
        DataTable dtCustomers = new DataTable();
        DataTable dtAppointments = new DataTable();

        //Set up Lists of Customers, and Appointments to hold them.
        List<Customer> customers = new List<Customer>();
        public List<Appointment> appointments = new List<Appointment>();
        List<string> logsList = new List<string>();

        //Constructors/Loaders
        /// <summary>
        /// Main Constructor
        /// </summary>
        public Frm_Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form Loader. Includes What should be loaded with the Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //Get the Language the system being used is set to, remove extra information
            var culture = CultureInfo.CurrentUICulture.EnglishName.ToString();
            userLanguage = culture.Split(' ')[0];
            
            //Set the login labels and button to Portuguese if used
            if(userLanguage == portugueseSetting)
            {
                Lbl_Username.Text = "Username: ";
                Lbl_Password.Text = "Senha: ";
                Btn_Login.Text = "Entrar";
            }

            //Get the Location from Time Zome
            timeZoneLocation = TimeZoneInfo.Local.DisplayName;
            mySelectedTimeZoneString = TimeZoneInfo.Local.Id;
            

            using (var conn = new MySqlConnection(connecterString))
            {
                //Populate the Clock with Current Time
                StartTimer();

                // Create User with Username and Password Test if one isn't already created
                string checkUser = "";
                var reader = DataChanges.GetMySqlReader(conn, getAllUsersQuery);
                while(reader.Read())
                {
                    checkUser = reader["userName"].ToString();
                    //If test user is found, exit the loop
                    if (checkUser == "Test")
                    {
                        return;
                    }
                }
                conn.Close();
                if (checkUser == "Test")
                {
                    MessageBox.Show("TestUser is already entered in Database");
                }
                else
                {
                    CreateNewUser("Test", "Test", "Dakoda");
                }
            }
        }


        // Class Methods
        /// <summary>
        /// Attempts to log in based on username and password. Throws errors if Username doesn't exist, or password is wrong
        /// </summary>
        /// <param name="attemptedPassword"></param>
        /// <param name="attemptedUsername"></param>
        private void AttemptLogIn(string attemptedPassword, string attemptedUsername)
        {

            //Set Messages and Alerts to use based on language selected at time of login attempt
            if (userLanguage == englishSetting)
            {
                passwordIncorrectAlert = "Incorrect Password! Please Try Again";
                userNameNonExistantAlert = "Username Doesn't exist. Please Try Again";
                welcomeMessage = "Welcome ";
                locationMessage = "Location Time Zone: " + mySelectedTimeZoneString;
            }
            else if (userLanguage == portugueseSetting)
            {
                passwordIncorrectAlert = "Senha Incorreto. Por Favor Tente De Novo";
                userNameNonExistantAlert = "Username não existe. Por Favor Tente De Novo";
                welcomeMessage = "Bem Vindos ";
                locationMessage = "Fuso Horário da Localização: " + mySelectedTimeZoneString;
            }
            
            using (var conn = new MySqlConnection(connecterString))
            {
                progressBar_Main.Step = 30;
                //Open Connection to the Database
                conn.Open();

                //Create MySQL Command to pull password Data out of Database
                MySqlCommand cmd = conn.CreateCommand();
                try
                {
                    //Create the command to select the user from Database
                    cmd.CommandText = "SELECT * FROM user WHERE username = '" + attemptedUsername + "'";
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        dataPassword = reader["password"].ToString();
                        userName = reader["userName"].ToString();
                        loggedInUserId = (int)reader["userId"];

                        if (dataPassword == attemptedPassword)
                        {
                            //If Username is found, and password matches, set the logged in user, find the location, and add a second language option
                            // Hide the login portion of the Window, show a welcome message, location and second language option, and a logout option in its place,
                            //and show the rest of the options.

                            //Show user that Content is Loading
                            Lbl_Loading.Visible = true;
                            Lbl_Loading.Text = "Loading Database Data...";

                            //fill Customer and Appointment Lists based on Login
                            fillInitialLists();
                            progressBar_Main.PerformStep();

                            //fill tables
                            Lbl_Loading.Text = "Preparing Customer Table...";
                            createCustomerTable();
                            progressBar_Main.PerformStep();

                            Lbl_Loading.Text = "Preparing Appointment Table";
                            createMonthViewAppointmentTable();
                            progressBar_Main.PerformStep();

                            //Make things visible after loading Table Information
                            RBtn_CalendarView.Checked = true;
                            Txt_Password.Visible = false;
                            Txt_Username.Visible = false;
                            Lbl_Password.Text = locationMessage;
                            Lbl_Username.Text = welcomeMessage + userName + "!";
                            Btn_Login.Visible = false;
                            Btn_LogOut.Visible = true;
                            loggedInUser = userName;
                            dataGridViewCustomers.Visible = true;
                            Btn_AddCustomer.Visible = true;
                            Btn_UpdateCustInfo.Visible = true;
                            Btn_DeleteCustomer.Visible = true;
                            groupBox_Customers.Visible = true;
                            groupBox_AppointmentView.Visible = true;
                            dataGridView_UserAppointments.Visible = true;
                            Btn_AddAppointment.Visible = true;
                            Btn_DeleteAppointment.Visible = true;
                            Btn_UpdateAppointment.Visible = true;
                            Btn_NewUser.Visible = true;
                            Btn_Reports.Visible = true;
                            monthCalendar_Main.Visible = true;

                            //Write to File the User Logged in, and Time
                            string successlog = "[" + DateTime.Now.ToString() + "]-" + loggedInUser 
                                + " has successfully logged in from the " + timeZoneLocation + " time zone in the "
                                + userLanguage + " language";
                            logsList.Add(successlog);
                            Lbl_Loading.Text = "Complete";
                            Lbl_Loading.Visible = false;
                            progressBar_Main.Hide();
                            return;
                        }
                        else
                        {
                            // If the Username is found, but the password isn't correct, Alert the User that an incorrect password was given
                            
                            MessageBox.Show(passwordIncorrectAlert);
                            Txt_Password.Text = "";
                            Txt_Password.Focus();

                            //Log the Failed Login Attempt
                            string failedLog = "[" + DateTime.Now.ToString() + "]-Failed Login attempt with username " 
                                + userName;
                            logsList.Add(failedLog);
                            //Don't continue
                            return;
                        }
                    }
                    //If The username was not found, Alert the user that the Username wasn't found

                    MessageBox.Show(userNameNonExistantAlert);
                    //Log failed attempt
                    string failedAttemptLog = "[" + DateTime.Now.ToString() + "]-User attempted to log in with invalid username: " 
                        + Txt_Username.Text;
                    logsList.Add(failedAttemptLog);
                }
                catch
                {
                    MessageBox.Show("Query failed");
                    string failedQueryLog = "[" + DateTime.Now.ToString() + "]-Query Failed";
                    logsList.Add(failedQueryLog);
                }

                //Close the connection to the Database
                conn.Close();
            }
        }
        
        /// <summary>
        /// Fills the Customer and Appointment Lists from the Database
        /// </summary>
        private void fillInitialLists()
        {
            using (var conn = new MySqlConnection(connecterString))
            {
                customers.Clear();
                string customerQuery = "SELECT * FROM customer WHERE active = 1";
                MySqlDataReader reader = DataChanges.GetMySqlReader(conn, customerQuery);
                while (reader.Read())
                {
                    int id = (int)reader["customerId"];
                    string name = reader["customerName"].ToString();
                    int addressId = (int)reader["addressId"];
                    var active = (bool)reader["active"];
                    string createdBy = reader["createdBy"].ToString();
                    DateTime createDate = Convert.ToDateTime(reader["createDate"]);
                    DateTime lastUpdate = Convert.ToDateTime(reader["lastUpdate"]);
                    string updatedBy = reader["lastUpdateBy"].ToString();

                    Customer customer = new Customer(id, name, addressId, active, createdBy, updatedBy);
                    customer.createDate = createDate;
                    customer.lastUpdate = lastUpdate;
                    customers.Add(customer);
                }
                conn.Close();

                appointments.Clear();
                string appointmentQuery = "SELECT * FROM appointment WHERE userId = " + loggedInUserId.ToString();
                MySqlDataReader appointmentReader = DataChanges.GetMySqlReader(conn, appointmentQuery);
                while (appointmentReader.Read())
                {
                    int appointmentId = (int)appointmentReader["appointmentId"];
                    int customerId = (int)appointmentReader["customerId"];
                    string title = appointmentReader["title"].ToString();
                    string description = appointmentReader["description"].ToString();
                    string location = appointmentReader["location"].ToString();
                    string contact = appointmentReader["contact"].ToString();
                    string url = appointmentReader["url"].ToString();
                    DateTime startTime = Convert.ToDateTime(appointmentReader["start"]);
                    DateTime endTime = Convert.ToDateTime(appointmentReader["end"]);
                    string createdBy = appointmentReader["createdBy"].ToString();
                    DateTime createDate = Convert.ToDateTime(appointmentReader["createDate"]);
                    DateTime lastUpdate = Convert.ToDateTime(appointmentReader["lastUpdate"]);
                    string updatedBy = appointmentReader["lastUpdateBy"].ToString();
                    string appointmentType = appointmentReader["appointmentType"].ToString();

                    Appointment appointment = new Appointment("Fill", appointmentId, customerId, loggedInUserId, title, description, location, contact, url, startTime, endTime, createdBy, createDate, updatedBy, lastUpdate, appointmentType, loggedInUser);
                    appointments.Add(appointment);
                }
                conn.Close();
            }
        }

        /// <summary>
        /// Creates the Customer Table at first
        /// </summary>
        public void createCustomerTable()
        {
            //Set up Customer DataTable
            dtCustomers.Columns.Clear();
            dtCustomers.Columns.Add("ID", typeof(int));
            dtCustomers.Columns.Add("Name", typeof(string));
            fillCustomerTable();
            dataGridViewCustomers.DataSource = dtCustomers;

        }

        /// <summary>
        /// Creates the Format for the Month View of the Appointment Table
        /// </summary>
        public void createMonthViewAppointmentTable()
        {
            // Create the DataTable Structure for the Month View
            dtAppointments.Columns.Clear();
            dtAppointments.Columns.Add("ID", typeof(int));
            dtAppointments.Columns.Add("Name", typeof(string));
            dtAppointments.Columns.Add("Start", typeof(string));
            
            //Fill the Appointment Table
            fillMonthViewAppointmentTable(DateTime.Now);
            
            //Bind the dataGridView to the DataTable
            dataGridView_UserAppointments.DataSource = dtAppointments;
        }

        /// <summary>
        /// Creates the Format for the Week view of the Appointment Table
        /// </summary>
        public void createWeekViewAppointmentTable(DateTime todayDate)
        {
            // Create the DataTable Structure for the Month View
            dtAppointments.Columns.Clear();
            //Get the day of the day entered as the first day of the week view
            var todaysDay = todayDate.DayOfWeek;
            //Get a new start on a known Sunday to use as a Sunday if the current day isn't sunday
            DateTime newStart = Convert.ToDateTime("2018-09-02");
            //Set up a variable to add days to the day of the week so we can reset it to zero if the last day was Saturday
            int daysToAdd = 0;

            dtAppointments.Columns.Add("Time", typeof(string));
            //Cycle through 7 days. For each: Add a Day. If it's Saturday, set the day for the next column 
            // equal to Sunday, and the days to zero, and skip the increment
            dtAppointments.Columns.Add((todaysDay + daysToAdd).ToString(), typeof(string));
            if ((todaysDay + daysToAdd).ToString() == "Saturday")
            {
                todaysDay = newStart.DayOfWeek;
                daysToAdd = 0;
            }
            else
            daysToAdd++;

            dtAppointments.Columns.Add((todaysDay + daysToAdd).ToString(), typeof(string));
            if ((todaysDay + daysToAdd).ToString() == "Saturday")
            {
                todaysDay = newStart.DayOfWeek;
                daysToAdd = 0;
            }
            else
            daysToAdd++;

            dtAppointments.Columns.Add((todaysDay + daysToAdd).ToString(), typeof(string));
            if ((todaysDay + daysToAdd).ToString() == "Saturday")
            {
                todaysDay = newStart.DayOfWeek;
                daysToAdd = 0;
            }
            else
            daysToAdd++;

            dtAppointments.Columns.Add((todaysDay + daysToAdd).ToString(), typeof(string));
            if ((todaysDay + daysToAdd).ToString() == "Saturday")
            {
                todaysDay = newStart.DayOfWeek;
                daysToAdd = 0;
            }
            else
            daysToAdd++;

            dtAppointments.Columns.Add((todaysDay + daysToAdd).ToString(), typeof(string));
            if ((todaysDay + daysToAdd).ToString() == "Saturday")
            {
                todaysDay = newStart.DayOfWeek;
                daysToAdd = 0;
            }
            else
            daysToAdd++;

            dtAppointments.Columns.Add((todaysDay + daysToAdd).ToString(), typeof(string));
            if ((todaysDay + daysToAdd).ToString() == "Saturday")
            {
                todaysDay = newStart.DayOfWeek;
                daysToAdd = 0;
            }
            else
            daysToAdd++;

            dtAppointments.Columns.Add((todaysDay + daysToAdd).ToString(), typeof(string));


            //Set Each Column's width so you don't need to scroll to the right and left to see the whole week
            foreach (DataGridViewColumn column in dataGridView_UserAppointments.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.Width = 65;
            }

            //Fill the Appointment Table
            fillWeekViewAppointmentTable(DateTime.Now);

            //Bind the dataGridView to the DataTable
            dataGridView_UserAppointments.DataSource = dtAppointments;
        }

        /// <summary>
        /// Fills the Customer Table with Database information 
        /// </summary>
        public void fillCustomerTable()
        {
            //Fill List
            dtCustomers.Rows.Clear();
            //put list into DataTable
            foreach(Customer customer in customers)
            {
                DataRow dr = dtCustomers.NewRow();
                dr["ID"] = customer.customerID;
                dr["Name"] = customer.customerName;
                dtCustomers.Rows.Add(dr);
            }
            //bind datatable to datagridview
        }

        /// <summary>
        /// Fills the Appointment Table
        /// </summary>
        public void fillMonthViewAppointmentTable(DateTime selectedDate)
        {
            //Fill List
            dtAppointments.Rows.Clear();

            //put list into DataTable
            var d = appointments[0].start.Date;
            foreach (Appointment appointment in appointments.Where(i=>i.start.ToLocalTime().Date == selectedDate.Date))
            {
                using (var conn = new MySqlConnection(connecterString))
                {
                    appointment.fillCustomerObject(conn);
                    DataRow dr = dtAppointments.NewRow();
                    dr["ID"] = appointment.appointmentId;
                    dr["Name"] = appointment.customerObject.customerName;
                    //Convert the UTC stored to local time, and put it into the datatable
                    dr["Start"] = appointment.start.ToLocalTime().ToString("MM/dd/yy HH:mm");
                    dtAppointments.Rows.Add(dr);
                }
            }
            //bind datatable to datagridview
        }

        /// <summary>
        /// Fills the Week View of the Appointment Table
        /// </summary>
        public void fillWeekViewAppointmentTable(DateTime selectedDate)
        {
            //Fill List
            dtAppointments.Rows.Clear();
            //Cycle through a row for each hour per business day
            for(int i = 0; i < 24; i++)
            {
                DataRow dr = dtAppointments.NewRow();
                dr["Time"] = i.ToString() + ":00";
                //Check if there is an appointment for each day that matches the date and hour of the day of the week
                //Check if the dates match the day of the week selected
                var sunday = appointments.Where(j => j.start.ToLocalTime().DayOfWeek.ToString() == "Sunday" && j.start.ToLocalTime().Hour == i && j.start.ToLocalTime().Date >= DateTime.Now.Date && j.start.ToLocalTime().Date <= (DateTime.Now.AddDays(6).Date)).FirstOrDefault();
                if(sunday != null)
                    dr["Sunday"] = sunday.appointmentId + "-" + sunday.customerObject.customerName;

                var monday = appointments.Where(j => j.start.ToLocalTime().DayOfWeek.ToString() == "Monday" && j.start.ToLocalTime().Hour == i && j.start.ToLocalTime().Date >= DateTime.Now.Date && j.start.ToLocalTime().Date <= (DateTime.Now.AddDays(6).Date)).FirstOrDefault();
                if (monday != null)
                    dr["Monday"] = monday.appointmentId + "-" + monday.customerObject.customerName;

                var tuesday = appointments.Where(j => j.start.ToLocalTime().DayOfWeek.ToString() == "Tuesday" && j.start.ToLocalTime().Hour == i && j.start.ToLocalTime().Date >= DateTime.Now.Date && j.start.ToLocalTime().Date <= (DateTime.Now.AddDays(6).Date)).FirstOrDefault();
                if (tuesday != null)
                    dr["Tuesday"] = tuesday.appointmentId + "-" + tuesday.customerObject.customerName;

                var wednesday = appointments.Where(j => j.start.ToLocalTime().DayOfWeek.ToString() == "Wednesday" && j.start.ToLocalTime().Hour == i && j.start.ToLocalTime().Date >= DateTime.Now.Date && j.start.ToLocalTime().Date <= (DateTime.Now.AddDays(6).Date)).FirstOrDefault();
                if (wednesday != null)
                    dr["Wednesday"] = wednesday.appointmentId + "-" + wednesday.customerObject.customerName;

                var thursday = appointments.Where(j => j.start.ToLocalTime().DayOfWeek.ToString() == "Thursday" && j.start.ToLocalTime().Hour == i && j.start.ToLocalTime().Date >= DateTime.Now.Date && j.start.ToLocalTime().Date <= (DateTime.Now.AddDays(6).Date)).FirstOrDefault();
                if (thursday != null)
                    dr["Thursday"] = thursday.appointmentId + "-" + thursday.customerObject.customerName;

                var friday = appointments.Where(j => j.start.ToLocalTime().DayOfWeek.ToString() == "Friday" && j.start.ToLocalTime().Hour == i && j.start.ToLocalTime().Date >= DateTime.Now.Date && j.start.ToLocalTime().Date <= (DateTime.Now.AddDays(6).Date)).FirstOrDefault();
                if (friday != null)
                    dr["Friday"] = friday.appointmentId + "-" + friday.customerObject.customerName;

                var saturday = appointments.Where(j => j.start.ToLocalTime().DayOfWeek.ToString() == "Saturday" && j.start.ToLocalTime().Hour == i && j.start.ToLocalTime().Date >= DateTime.Now.Date && j.start.ToLocalTime().Date <= (DateTime.Now.AddDays(6).Date)).FirstOrDefault();
                if (saturday != null)
                    dr["Saturday"] = saturday.appointmentId + "-" + saturday.customerObject.customerName;

                dtAppointments.Rows.Add(dr);
            }
        }

        /// <summary>
        /// Creates a new User
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="userCreating"></param>
        private void CreateNewUser(string username, string password, string userCreating)
        {
            using (var conn = new MySqlConnection(connecterString))
            {
                int numOfUsers = 0;
                // Create a user based on the Username and password given
                //Get the number of users currently in the database, Add one, and save it as a variable to use as a new ID
                MySqlDataReader reader = DataChanges.GetMySqlReader(conn, getAllUsersQuery);
                while (reader.Read())
                {
                    numOfUsers++;
                }
                conn.Close();
                numOfUsers++;
                DateTime utcToday = DateTime.UtcNow;
                DateTime today = DateTime.Now;


                User newUser = new User
                {
                    userId = numOfUsers,
                    userName = username,
                    password = password,
                    lastUpdate = today,
                    lastUpdatedBy = userCreating,
                    createdBy = userCreating,
                    createDate = today,
                    active = 1
                };


                //Create a Command to insert a new user with the id as the count + 1 of users, the username and password given, the user adding the user.
                string insertCommand = "INSERT INTO user VALUES (" + newUser.userId.ToString() + ", '" + newUser.userName + "', '" + newUser.password + "', " + newUser.active.ToString()
                    + ", '" + newUser.createdBy + "', '" + newUser.createDate.ToString("yyyy-mm-dd") + "', '" + newUser.lastUpdate.ToString("yyyy-mm-dd") + "', '" + newUser.lastUpdatedBy + "')";
                DataChanges.ExecuteMySqlCommand(insertCommand, conn);
                //See how many users are in Database
                //reader = DataChanges.GetMySqlReader(conn, "SELECT * FROM user");
                string addedUserLog = "[" + DateTime.Now.ToString() + "]-User Test has been created";
                logsList.Add(addedUserLog);
            }
        }

        System.Windows.Forms.Timer t = null;
        private void StartTimer()
        {
            t = new System.Windows.Forms.Timer();
            t.Interval = 1000;
            t.Tick += new EventHandler(t_Tick);
            t.Enabled = true;
        }

        /// <summary>
        /// Changes the label to show the current time based on login, uses time as trigger to other events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void t_Tick(object sender, EventArgs e)
        {
            DateTime selectedTime = DateTime.Now;
            //if (mySelectedTimeZoneString != null)
            //{
            //    //Show the user the current time within the Form once it is logged in
            //    TimeZoneInfo selectedTimeZone = TimeZoneInfo.FindSystemTimeZoneById(mySelectedTimeZoneString);
            //    selectedTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, selectedTimeZone);
            //    Lbl_Clock.Text = selectedTime.ToString();
            //}
            //else
            //{

            //Store the current utc time, and Show the current local time
            selectedTime = DateTime.UtcNow;
            Lbl_Clock.Text = DateTime.Now.ToString();
            //}
            if (loggedInUser != "")
            {
                foreach (var appt in appointments)
                {
                    //Add 15 minutes to the current time, and check if any appointments match that time
                    DateTime checkTime = selectedTime.AddMinutes(15);
                    if (checkTime.Date == appt.start.Date && checkTime.Hour == appt.start.Hour && checkTime.Minute == appt.start.Minute && checkTime.Second == 00)
                    {
                        //throw new Exception("Hello");
                        string appointmentReminder = "Reminder:\nAppointment with " + appt.customerObject.customerName
                            + " in 15 minutes";
                        MessageBox.Show(appointmentReminder);
                    }
                    else
                    {
                      
                    }
                }
            }
        }


        /// <summary>
        /// Method to use for dropdowns and text change events for Changing the Location
        /// </summary>
        private void textChangeLocationSelectEvent()
        {
            try
            {
                DateTime itIsNow = DateTime.UtcNow;
                string selectedZone = "";

                if (timeZoneLocation == newYorkLocation)
                {
                    selectedZone = "Eastern Standard Time";
                }
                else if(timeZoneLocation == londonLocation)
                {
                    selectedZone = "GMT Standard Time";
                }
                else if(timeZoneLocation == phoenixLocation)
                {
                    selectedZone = "Mountain Standard Time";
                }
                TimeZoneInfo selectedTimeZone = TimeZoneInfo.FindSystemTimeZoneById(selectedZone);
                DateTime selectedTime = TimeZoneInfo.ConvertTimeFromUtc(itIsNow, selectedTimeZone);
                mySelectedTimeZoneString = selectedZone;
                /*Example with Daylight Savings Check:
                    DateTime timeUtc = DateTime.UtcNow;
                    try
                    {
                       TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                       DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                       Console.WriteLine("The date and time are {0} {1}.", 
                                         cstTime, 
                                         cstZone.IsDaylightSavingTime(cstTime) ?
                                                 cstZone.DaylightName : cstZone.StandardName);
                    }
                    catch (TimeZoneNotFoundException)
                    {
                       Console.WriteLine("The registry does not define the Central Standard Time zone.");
                    }                           
                    catch (InvalidTimeZoneException)
                    {
                       Console.WriteLine("Registry data on the Central Standard Time zone has been corrupted.");
                    }*/
            }
            catch
            {
                MessageBox.Show("Please Choose A Location");
            }
        }

        //Event Handlers
        //  Button Presses
        /// <summary>
        /// Attempts to log a user in when Clicking the Login Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Login_Click(object sender, EventArgs e)
        {
            progressBar_Main.Value = 0;
            AttemptLogIn(Txt_Password.Text.ToString(), Txt_Username.Text.ToString());
        }

        /// <summary>
        /// Logs user out when Logout Button Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_LogOut_Click(object sender, EventArgs e)
        {
            string loggedOutLog = "[" + DateTime.Now.ToString() + "]-" + loggedInUser + " has successfully logged out";
            logsList.Add(loggedOutLog);
            Txt_Password.Visible = true;
            Txt_Username.Visible = true;
            Lbl_Password.Text = "Password";
            Lbl_Username.Text = "Username";
            Btn_Login.Visible = true;
            Btn_LogOut.Visible = false;
            Txt_Password.Text = "";
            Txt_Username.Text = "";
            loggedInUser = "";
            loggedInUserId = 0;
            dataGridViewCustomers.Visible = false;
            Btn_AddCustomer.Visible = false;
            Btn_UpdateCustInfo.Visible = false;
            Btn_DeleteCustomer.Visible = false;
            groupBox_Customers.Visible = false;
            groupBox_AppointmentView.Visible = false;
            dataGridView_UserAppointments.Visible = false;
            Btn_AddAppointment.Visible = false;
            Btn_DeleteAppointment.Visible = false;
            Btn_UpdateAppointment.Visible = false;
            Btn_NewUser.Visible = false;
            Btn_Reports.Visible = false;
            monthCalendar_Main.Visible = true;
            
            //Write logs to file, and clear the list for the next user to log in
            DataChanges.WriteToLogsFile(logsList);
            logsList.Clear();
        }

        /// <summary>
        /// Closes The Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            //If user hasn't logged out when they exit, note that, and write the logslist to file
            if (loggedInUserId != 0)
                DataChanges.WriteToLogsFile(logsList);
            Close();
        }

        /// <summary>
        /// Opens a form to add a new customer when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddCustomer_Click(object sender, EventArgs e)
        {
            progressBar_Main.Show();
            progressBar_Main.Value = 0;
            progressBar_Main.Step = 50;
            Frm_AddCustomer newCustomer = new Frm_AddCustomer();
            newCustomer.loggedInUser = loggedInUser;
            newCustomer.connecterString = connecterString;
            newCustomer.ShowDialog();

            //check if customer was created. Fill objects needed and add to table if it has been
            if (newCustomer.customerAdded == true)
            {
                Customer newCustomerObject = newCustomer.myCustomer;
                DataRow dr = dtCustomers.NewRow();
                dr["ID"] = newCustomerObject.customerID;
                dr["Name"] = newCustomerObject.customerName;
                dtCustomers.Rows.Add(dr);
                customers.Add(newCustomerObject);
                progressBar_Main.PerformStep();
                //Note that the user has added a customer
                string successfullCustomerAdd = "[" + DateTime.Now.ToString() + "]-" + loggedInUser
                    + " has successfully added customer " + newCustomerObject.customerName + " with id " 
                    + newCustomerObject.customerID.ToString();
                logsList.Add(successfullCustomerAdd);
            }
            progressBar_Main.PerformStep();
            progressBar_Main.Hide();
        }

        /// <summary>
        /// Updates Customer Information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_UpdateCustInfo_Click(object sender, EventArgs e)
        {
            progressBar_Main.Show();
            progressBar_Main.Value = 0;
            progressBar_Main.Step = 50;
            //Get ID from Table
            int custId = (int)dataGridViewCustomers.CurrentRow.Cells[0].Value;
            Frm_AddCustomer editCustomer = new Frm_AddCustomer();
            editCustomer.selectedCustomer = custId;
            editCustomer.loggedInUser = loggedInUser;
            editCustomer.connecterString = connecterString;
            editCustomer.ShowDialog();

            if (editCustomer.customerAdded == true)
            {
                Customer updatedCustomer = editCustomer.myCustomer;
                // Update Values in DataTable - Use Lambda for updating the table to be quicker and more efficient than 
                var checkCustomer = customers.Where(i => i.customerID == updatedCustomer.customerID).First();
                int customerIndex = customers.IndexOf(checkCustomer);
                customers[customerIndex] = updatedCustomer;

                // update table information
                dataGridViewCustomers[1, dataGridViewCustomers.CurrentRow.Index].Value = customers.Where(i => i.customerID == custId).First().customerName;
                progressBar_Main.PerformStep();
                //Note that the customer was updated in the logs
                string successfullCustomerUpdated = "[" + DateTime.Now.ToString() + "]-" + loggedInUser
                    + " has successfully updated customer " + updatedCustomer.customerName + " with id "
                    + updatedCustomer.customerID.ToString();
                logsList.Add(successfullCustomerUpdated);
                progressBar_Main.PerformStep();
                progressBar_Main.Hide();
            }
            //fillCustomerTable();
            //Get Information from textboxes
            //Use this information to update Customer Information
        }

        /// <summary>
        /// Sets a customer to inactive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeleteCustomer_Click(object sender, EventArgs e)
        {
            progressBar_Main.Show();
            progressBar_Main.Value = 0;
            progressBar_Main.Step = 50;
            using (var conn = new MySqlConnection(connecterString))
            {
                try
                {
                    //Get the customer id selected
                    int custId = (int)dataGridViewCustomers.CurrentRow.Cells[0].Value;
                    DialogResult result;
                    //check to make sure there is not an appointment with the customer id
                    foreach (var appointment in appointments)
                    {
                        if(appointment.customerId == custId)
                        {
                            result = MessageBox.Show("This customer has an appointment scheduled. \nAre you sure you want to delete this customer? The scheduled appointment will also be deleted.", "Warning",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if(result.ToString() == "No")
                            {
                                return;
                            }
                        }
                    }

                    string myUpdateCustomersQuery = "UPDATE customer "
                                + "SET active = 0 "
                                + "WHERE customerId = " + custId.ToString();
                    //DataChanges dt = new DataChanges();
                    DataChanges.ExecuteMySqlCommand(myUpdateCustomersQuery, conn);
                    progressBar_Main.PerformStep();
                    //Remove from Table
                    dtCustomers.Rows.RemoveAt(dataGridViewCustomers.CurrentRow.Index);

                    //Delete Appointment with associated customerId
                    string myUpdateAppointmentCustomersQuery = "DELETE FROM appointment WHERE customerId = "
                        + custId.ToString();
                    DataChanges.ExecuteMySqlCommand(myUpdateAppointmentCustomersQuery, conn);

                    //Note that the customer was deleted in the logs
                    string successfullCustomerDeleted = "[" + DateTime.Now.ToString() + "]-" + loggedInUser
                        + " has successfully updated customer with id "
                        + custId.ToString();
                    logsList.Add(successfullCustomerDeleted);

                    List<Appointment> appointmentsToDelete = appointments.Where(i => i.customerId == custId).ToList();
                    foreach(var appointment in appointmentsToDelete)
                    {
                        //Find Datarows with the appointmentid's that have the customer being deleted
                        DataRow[] drs = dtAppointments.Select("ID = " + appointment.appointmentId.ToString());
                        //Go through each DataRow that matches, and remove them
                       foreach (var dd in drs)
                        {
                            dtAppointments.Rows.Remove(dd);
                                //(This gets the value from the data row): dd.ItemArray[0].ToString());
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Something went wrong with the Delete Query");
                }
            }
            progressBar_Main.PerformStep();
            progressBar_Main.Hide();
        }

        /// <summary>
        /// Adds An Appointment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddAppointment_Click(object sender, EventArgs e)
        {
            progressBar_Main.Show();
            progressBar_Main.Value = 0;
            progressBar_Main.Step = 30;
            //int appointmentId = (int)dataGridView_UserAppointments.CurrentRow.Cells[0].Value;
            Frm_AddAppointment newAppointment = new Frm_AddAppointment();
            newAppointment.loggedInUser = loggedInUser;
            newAppointment.loggedInUserId = loggedInUserId;
            newAppointment.selectedTimeZone = mySelectedTimeZoneString;
            newAppointment.appointments = appointments;
            //newAppointment.time
            newAppointment.ShowDialog();
            appointments = newAppointment.appointments;
            progressBar_Main.PerformStep();

            //Check if an appointment is made. If it has been made, check the last appointment, and 
            // fill the information needed in the object and table
            if (newAppointment.appointmentAdded == true)
            {
                Appointment appointment = appointments.Last();
                using (var conn = new MySqlConnection(connecterString))
                    appointment.fillCustomerObject(conn);
                if (RBtn_CalendarView.Checked == true)
                {
                    if(appointment.start.ToLocalTime().Date == monthCalendar_Main.SelectionRange.Start)
                    {
                        DataRow dr = dtAppointments.NewRow();
                        dr["ID"] = appointment.appointmentId;
                        dr["Name"] = appointment.customerObject.customerName;
                        dr["Start"] = appointment.start.ToLocalTime().ToString("MM/dd/yy HH:mm");
                        dtAppointments.Rows.Add(dr);
                        dataGridView_UserAppointments.Refresh();
                    }
                    progressBar_Main.PerformStep();
                    //THIS NEXT LINE WAS USED INSTEAD OF THE ABOVE STATEMENT PREVIOUSLY
                    //fillMonthViewAppointmentTable(monthCalendar_Main.SelectionRange.Start);
                    //DataRow dr = dtAppointments.NewRow();
                    //dr["ID"] = appointment.appointmentId;
                    //dr["Name"] = appointment.customerObject.customerName;
                    //dr["Start"] = appointment.start.ToLocalTime().ToString("MM/dd/yy HH:mm");
                    //dtAppointments.Rows.Add(dr);
                }
                else
                {
                    appointments.Add(appointment);
                    fillWeekViewAppointmentTable(DateTime.Now);
                    progressBar_Main.PerformStep();
                }
                
                //Note that the appointment was added in the logs
                string successfullAppointmentAdd = "[" + DateTime.Now.ToString() + "]-" + loggedInUser
                    + " has successfully added an appointment with " + appointment.customerObject.customerName + " on "
                    + appointment.start.Date.ToString() + " at " + appointment.start.TimeOfDay.ToString() + " in " 
                    + appointment.location;
                logsList.Add(successfullAppointmentAdd);
                progressBar_Main.PerformStep();
                progressBar_Main.Hide();
            }
        }

        /// <summary>
        /// Updates a selected appointment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_UpdateAppointment_Click(object sender, EventArgs e)
        {
            progressBar_Main.Show();
            progressBar_Main.Value = 0;
            progressBar_Main.Step = 30;
            try
            { 
                int selectedAppointmentId;
                if (RBtn_CalendarView.Checked == true)
                {
                    selectedAppointmentId = (int)dataGridView_UserAppointments.CurrentRow.Cells[0].Value;
                }
                else
                {
                    //Get the Value of the cell and parse out the id value by the - that was put into it during creation
                    var inputString = dataGridView_UserAppointments.CurrentCell.Value.ToString();
                    var inputTest = inputString.Split('-');
                    selectedAppointmentId = Convert.ToInt32(inputTest[0]);
                }
                Frm_AddAppointment updateAppointment = new Frm_AddAppointment();
                updateAppointment.loggedInUser = loggedInUser;
                updateAppointment.loggedInUserId = loggedInUserId;
                updateAppointment.selectedAppointmentId = selectedAppointmentId;
                updateAppointment.appointments = appointments;
                updateAppointment.selectedTimeZone = mySelectedTimeZoneString;
                //Use lambda to get the appointment from the list, and return the appointment so we can use that to fill the update form
                Appointment getAppointment = appointments.Where(i => i.appointmentId.Equals(selectedAppointmentId)).ElementAt(0);
                updateAppointment.selectedAppointment = getAppointment;
                updateAppointment.ShowDialog();
                progressBar_Main.PerformStep();

                //Set the list of appointments equal to the one updated in the add appointment form
                appointments = updateAppointment.appointments;
                progressBar_Main.PerformStep();
                // Update Values in DataTable - Use Lambda for updating the table to be quicker and more efficient than 
                // going to the database again, and refilling all of the objects
                if (RBtn_CalendarView.Checked == true)
                {
                    fillMonthViewAppointmentTable(getAppointment.start);
                    //dataGridView_UserAppointments[1, dataGridView_UserAppointments.CurrentRow.Index].Value = appointments.Where(i => i.appointmentId == selectedAppointmentId).First().customerObject.customerName;
                    //dataGridView_UserAppointments[2, dataGridView_UserAppointments.CurrentRow.Index].Value = appointments.Where(i => i.appointmentId == selectedAppointmentId).First().start.ToLocalTime();
                }
                progressBar_Main.PerformStep();
                //Note that the appointment was updated in the logs
                string successfullAppointmentUpdate = "[" + DateTime.Now.ToString() + "]-" + loggedInUser
                    + " has successfully updated an appointment with " + getAppointment.customerObject.customerName + " on "
                    + getAppointment.start.Date.ToString("MM/dd/yyyy") + " at " + getAppointment.start.TimeOfDay.ToString() + " in "
                    + getAppointment.location;
                logsList.Add(successfullAppointmentUpdate);
            }
            catch
            {
                MessageBox.Show("No Appointments Selected To Update");
            }
            progressBar_Main.Hide();
}

        /// <summary>
        /// Deletes an Appointment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeleteAppointment_Click(object sender, EventArgs e)
        {
            progressBar_Main.Show();
            progressBar_Main.Value = 0;
            progressBar_Main.Step = 50;
            using(var conn = new MySqlConnection(connecterString))
            {
                int selectedAppointmentId;
                //Get the value of the id of the selected row
                if (RBtn_CalendarView.Checked == true)
                    selectedAppointmentId = (int)dataGridView_UserAppointments.CurrentRow.Cells[0].Value;
                else
                {
                    //Get the Value of the cell and parse out the id value by the - that was put into it during creation
                    var inputString = dataGridView_UserAppointments.CurrentCell.Value.ToString();
                    var inputTest = inputString.Split('-');
                    selectedAppointmentId = Convert.ToInt32(inputTest[0]);
                }
                //Set up a query
                string deleteAppointmentQuery = "DELETE FROM appointment WHERE appointmentId = " + selectedAppointmentId.ToString();
                //Execute Query
                DataChanges.ExecuteMySqlCommand(deleteAppointmentQuery, conn);
                progressBar_Main.PerformStep();
                //Remove Row from Table
                if(RBtn_CalendarView.Checked == true)
                    dtAppointments.Rows.RemoveAt(dataGridView_UserAppointments.CurrentRow.Index);
                else
                {
                    fillWeekViewAppointmentTable(DateTime.Now);
                }
                appointments.Remove(appointments.Where(i => i.appointmentId == selectedAppointmentId).First());
                progressBar_Main.PerformStep();

                //Note that the appointment was deleted in the logs
                string successfullAppointmentAdd = "[" + DateTime.Now.ToString() + "]-" + loggedInUser
                    + " has successfully deleted appointment with the id" + selectedAppointmentId.ToString();
                logsList.Add(successfullAppointmentAdd);
                progressBar_Main.Hide();
            }
        }

        //  Key Presses
        /// <summary>
        /// Presses the Login Button if enter is pressed within the Password Box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Btn_Login_Click(this, new EventArgs());
            }
        }

        //  Drop Downs/Text Change

        /// <summary>
        /// Saves the Location to be used in the success login message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_SelectLocation_DropDownClosed(object sender, EventArgs e)
        {
            textChangeLocationSelectEvent();
        }

        /// <summary>
        /// Saves the Location when text is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_SelectLocation_TextChanged(object sender, EventArgs e)
        {
            textChangeLocationSelectEvent();
        }

        /// <summary>
        /// Opens a form to add a new user when pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_NewUser_Click(object sender, EventArgs e)
        {
            // Open the new User form, insert the connection string and logged in user for use in adding a user
            Frm_NewUser addUser = new Frm_NewUser();
            addUser.connecterString = connecterString;
            addUser.loggedInUser = loggedInUser;
            addUser.ShowDialog();
            //Note that the appointment was added in the logs
            string userAddedLog = "[" + DateTime.Now.ToString() + "]-" + loggedInUser
                + " has successfully added a new user.";
            logsList.Add(userAddedLog);
        }

        /// <summary>
        /// Opens a form to create reports when pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Reports_Click(object sender, EventArgs e)
        {
            Frm_Reports getReports = new Frm_Reports();
            getReports.customers = customers;
            getReports.loggedInUser = loggedInUser;
            getReports.connecterString = connecterString;
            getReports.ShowDialog();

            //Get logs from reports viewed
            foreach(var log in getReports.reportLogs)
            {
                logsList.Add(log);
            }
        }

        /// <summary>
        /// Shows the list of Appointments for the day when a day in Month View is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthCalendar_Main_DateChanged(object sender, DateRangeEventArgs e)
        {
            var selectedDate = monthCalendar_Main.SelectionRange.Start;
            fillMonthViewAppointmentTable(selectedDate);
        }

        /// <summary>
        /// Switches to Calendar View when Calendar View Radio Button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RBtn_CalendarView_CheckedChanged(object sender, EventArgs e)
        {
            monthCalendar_Main.Visible = true;
            dataGridView_UserAppointments.Location = new Point(254, 44);
            dataGridView_UserAppointments.Size = new Size(398, 159);
            createMonthViewAppointmentTable();

            //Log the Calendar View Switch
            if (loggedInUser != "")
            {
                string calendarViewLog = "[" + DateTime.Now.ToString() + "]-" + loggedInUser
                    + " has switched to Calendar View";
                logsList.Add(calendarViewLog);
            }
        }

        /// <summary>
        /// Changes to Week View when Week View Radio Button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RBtn_WeekView_CheckedChanged(object sender, EventArgs e)
        {
            monthCalendar_Main.Visible = false;
            dataGridView_UserAppointments.Location = new Point(12, 44);
            dataGridView_UserAppointments.Size = new Size(640, 159);

            createWeekViewAppointmentTable(DateTime.Now);

            //Log the Week View Switch
            string weekViewLog = "[" + DateTime.Now.ToString() + "]-" + loggedInUser
                + " has switched to Week View";
            logsList.Add(weekViewLog);
        }
    }
}
