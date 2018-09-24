using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Software2Project_DakodaWillden_V1.Classes;

namespace Software2Project_DakodaWillden_V1.Classes
{
    public class User
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public int active { get; set; }
        public DateTime createDate { get; set; }
        public string createdBy { get; set; }
        public DateTime lastUpdate { get; set; }
        public string lastUpdatedBy { get; set; }

        public string connecterString { get; set; }

        /// <summary>
        /// Generic Constructor
        /// </summary>
        public User()
        {

        }

        /// <summary>
        /// Constructor to create a new user
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="userCreating"></param>
        /// <param name="Connection"></param>
        public User(string Username, string Password, string userCreating, MySqlConnection Connection)
        {
            using (Connection)
            {
                try
                {
                    int numOfUsers = 0;
                    string getAllUsersQuery = "SELECT * FROM user";
                    // Create a user based on the Username and password given
                    //Get the number of users currently in the database, Add one, and save it as a variable to use as a new ID
                    MySqlDataReader reader = DataChanges.GetMySqlReader(Connection, getAllUsersQuery);
                    while (reader.Read())
                    {
                        numOfUsers++;
                    }
                    Connection.Close();
                    numOfUsers++;
                    DateTime today = DateTime.Now;

                    //Fill User Object with 
                    userId = numOfUsers;
                    userName = Username;
                    password = Password;
                    lastUpdate = today;
                    lastUpdatedBy = userCreating;
                    createdBy = userCreating;
                    createDate = today;
                    active = 1;


                    //DataChanges db = new DataChanges();
                    //Create a Command to insert a new user with the id as the count + 1 of users, the username and password given, the user adding the user.
                    string insertCommand = "INSERT INTO user VALUES (" + userId.ToString() + ", '" + userName + "', '" + password + "', " + active.ToString()
                        + ", '" + createdBy + "', '" + createDate.ToString("yyyy-mm-dd") + "', '" + lastUpdate.ToString("yyyy-mm-dd") + "', '" + lastUpdatedBy + "')";
                    DataChanges.ExecuteMySqlCommand(insertCommand, Connection);
                    //See how many users are in Database
                    //reader = DataChanges.GetMySqlReader(conn, "SELECT * FROM user");
                }
                catch
                {
                    throw new Exception("Failed to Add User");
                }
            }
        }
    }
}
