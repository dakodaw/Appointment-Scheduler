using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Software2Project_DakodaWillden_V1.Classes
{
    public class Appointment
    {
        //Class Properties
        public int appointmentId { get; set; }
        public int customerId { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string contact { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public DateTime createDate { get; set; }
        public string createdBy { get; set; }
        public DateTime lastUpdate { get; set; }
        public string lastUpdateBy { get; set; }

        public string loggedInUser { get; set; }
        public string errorMessage { get; set; }

        string connecterString = "Persist Security Info=False;server=52.206.157.109;database=U04MmA;uid=U04MmA;password=53688281290;SslMode=none";
        int highestAppointmentId;
        public Customer customerObject;

        //Constructors
        /// <summary>
        /// Basic Constructor
        /// </summary>
        public Appointment()
        {

        }

        /// <summary>
        /// Constructor to Add Customer to Database
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="UserId"></param>
        /// <param name="Title"></param>
        /// <param name="Description"></param>
        /// <param name="Location"></param>
        /// <param name="Contact"></param>
        /// <param name="Type"></param>
        /// <param name="Url"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="UpdatedBy"></param>
        /// <param name="AppointmentType"></param>
        public Appointment(int CustomerId, int UserId, string Title, string Description, string Location, string Contact, string Url, DateTime StartTime, DateTime EndTime, string CreatedBy, string UpdatedBy, string AppointmentType)
        {
            using (var conn = new MySqlConnection(connecterString))
            {
                
                string findHighestAppointmentId = "SELECT MAX(appointmentId) FROM appointment";
                var reader = DataChanges.GetMySqlReader(conn, findHighestAppointmentId);
                while (reader.Read())
                {
                    try
                    {
                        highestAppointmentId = (int)reader["MAX(appointmentId)"];
                    }
                    catch
                    {
                        highestAppointmentId = 0;
                    }
                }
                conn.Close();

                type = AppointmentType;
                highestAppointmentId++;
                appointmentId = highestAppointmentId;
                customerId = CustomerId;
                userId = UserId;
                title = Title;
                description = Description;
                location = Location;
                contact = Contact;
                url = Url;
                start = StartTime;
                end = EndTime;
                createdBy = CreatedBy;
                createDate = DateTime.Now;
                lastUpdate = DateTime.Now;
                lastUpdateBy = UpdatedBy;

                string addAppointmentQuery = "INSERT INTO appointment VALUES(" + appointmentId.ToString() 
                    + ", " + customerId.ToString() + ", '" + title + "', '" + description + "', '" 
                    + location + "', '" + contact + "', '" +  url + "', '" 
                    + start.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + end.ToString("yyyy-MM-dd HH:mm:ss") + "', '" 
                    + createDate.ToString("yyyy-MM-dd") + "', '" + createdBy + "', '" + lastUpdate.ToString("yyyy-MM-dd")
                    + "', '" + lastUpdateBy + "', '" + type + "', " + userId.ToString() + ")";
                //DataChanges db = new DataChanges();
                DataChanges.ExecuteMySqlCommand(addAppointmentQuery, conn);
            }
        }

        /// <summary>
        /// Constructor to Fill Data from Data Given, or update record
        /// </summary>
        /// <param name="AppointmentId"></param>
        /// <param name=""></param>
        public Appointment(string UpdateOrFill, int AppointmentId, int CustomerId, int UserId, string Title, string Description, string Location, string Contact, string Url, DateTime StartTime, DateTime EndTime, string CreatedBy, DateTime CreateDate, string UpdatedBy, DateTime UpdateDate, string AppointmentType, string UserLoggedIn)
        {
            using (var conn = new MySqlConnection(connecterString))
            {
                loggedInUser = UserLoggedIn;
                type = AppointmentType;
                appointmentId = AppointmentId;
                customerId = CustomerId;
                fillCustomerObject(conn);
                userId = UserId;
                title = Title;
                description = Description;
                location = Location;
                contact = Contact;
                url = Url;
                start = StartTime;
                end = EndTime;
                createdBy = CreatedBy;
                createDate = CreateDate;
                lastUpdate = UpdateDate;
                lastUpdateBy = UpdatedBy;
                if (UpdateOrFill.ToUpper() == "UPDATE")
                {
                    lastUpdate = DateTime.Now;
                    lastUpdateBy = loggedInUser;

                    string updateQuery = "UPDATE appointment SET customerId = " + customerId.ToString()
                        + ", title = '" + title + "', description = '" + description + "', location = '" + location
                        + "', contact = '" + contact + "', url = '" + url + "', start = '" + start.ToString("yyyy-MM-dd HH:mm")
                        + "', end = '" + end.ToString("yyyy-MM-dd HH:mm") + "', createDate = '" + createDate.ToString("yyyy-MM-dd")
                        + "', createdBy = '" + createdBy + "', lastUpdateBy = '" + lastUpdateBy
                        + "', appointmentType = '" + type + "', userId = " + userId.ToString() + " WHERE appointmentId = " + appointmentId;
                    //DataChanges db = new DataChanges();
                    DataChanges.ExecuteMySqlCommand(updateQuery, conn);

                }
                else
                {

                }
            }
        }

        //Class Methods
        /// <summary>
        /// Fills Customer Object. Use only after other Appointment Information is filled
        /// </summary>
        /// <param name="existingCustomerId"></param>
        /// <param name="conn"></param>
        public void fillCustomerObject(MySqlConnection conn)
        {
            using (conn)
            {
                customerObject = new Customer(customerId, conn);
            }
        }

       
    }
}
