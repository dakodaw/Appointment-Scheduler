using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Software2Project_DakodaWillden_V1.Classes
{
    public class Customer
    {
        //Class Properties
        public int customerID { get; set; }
        public string customerName { get; set; }
        public int addressID { get; set; }
        public int active { get; set; }
        public DateTime createDate { get; set; }
        public string createdBy { get; set; }
        public DateTime lastUpdate { get; set; }
        public string lastUpdateBy { get; set; }
        public string loggedInUser { get; set; }

        public Address addressObject;

        //Constructors
        /// <summary>
        /// Constructor for Adding a new Customer, or loading customer object if matching customer exists
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Address1"></param>
        /// <param name="Address2"></param>
        /// <param name="City"></param>
        /// <param name="CountryName"></param>
        /// <param name="PostalCode"></param>
        /// <param name="Phone"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="conn"></param>
        public Customer(string Name, string Address1, string Address2, string City, string CountryName, string PostalCode, string Phone, string CreatedBy, MySqlConnection conn)
        {
            //Check Database for number of customers, add one, and use as customer ID
            using (conn)
            {
                addressObject = new Address(Address1, Address2, City, CountryName, PostalCode, Phone, CreatedBy, conn);
                bool customerExists = false;
                int existingCustomerId = 0;

                bool databaseEmpty = true;
                int newMaxCityId = 0;

                //Get the max value of id instead of counting all of the accounts.
                string myQuery = "SELECT MAX(customerId) FROM customer";
                MySqlDataReader reader1 = DataChanges.GetMySqlReader(conn, myQuery);
                //Use this to see if the country exists, and to count the number of countries
                while (reader1.Read())
                {
                    newMaxCityId = (int)reader1["Max(customerId)"];
                    databaseEmpty = false;
                }
                conn.Close();


                //Check to see if there are any matches to the customer
                string mySqlQuery = "SELECT * FROM customer JOIN address ON customer.addressId = address.addressId";
                MySqlDataReader reader = DataChanges.GetMySqlReader(conn, mySqlQuery);
                while (reader.Read())
                {
                    string TempName = reader["customerName"].ToString();
                    if(TempName == Name)
                    {
                        string tempAddress = reader["address"].ToString() + reader["address2"].ToString();
                        string tempPhone = reader["phone"].ToString();
                        if (tempAddress == Address1 + Address2 && tempPhone == Phone)
                        {
                            customerExists = true;
                            existingCustomerId = (int)reader["customerId"];
                        }
                    }
                }
                conn.Close();
                if(customerExists == true)
                {
                    setDatabaseCustomerInfo(existingCustomerId, conn);
                }
                else
                {
                    newMaxCityId++;
                    if(databaseEmpty == true)
                    {
                        customerID = 1;
                    }
                    else
                    {
                        customerID = newMaxCityId;
                    }
                    //Set name = the name given
                    //use address constructor to set phone, and address along with postal Code, etc in an object
                    //get address ID from address object to add address ID
                    //Set active,
                    //Use current date/time to set create and last update dates
                    //Use the given logged in user name to set createdBy, and lastupdated by
                    customerName = Name;
                    addressID = addressObject.addressId;
                    active = 1;
                    createDate = DateTime.Now;
                    CreatedBy = loggedInUser;
                    lastUpdate = DateTime.Now;
                    lastUpdateBy = loggedInUser;

                    //add customer to database if it doesn't exist
                    //DataChanges db = new DataChanges();
                    myQuery = "INSERT INTO customer VALUES(" + customerID.ToString() + ", '" + customerName + "', "
                        + addressID.ToString() + ", " + active.ToString() +  ", '" + createDate.ToString("yyyy-MM-dd") + "', '" + createdBy
                        + "', '" + lastUpdate.ToString("yyyy-MM-dd") + "', '" + lastUpdateBy + "')";
                    DataChanges.ExecuteMySqlCommand(myQuery, conn);
                }
            }
        }

        /// <summary>
        /// Constructor for Customer with known CustomerId, or alerting user if CustomerId isn't found
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="conn"></param>
        public Customer(int CustomerId, MySqlConnection conn)
        {
            //Find the City by CityID, and populate the class information with the information given.
            try
            {
                setDatabaseCustomerInfo(CustomerId, conn);
            }
            //Throw an error if ID isn't found
            catch (Exception innerException)
            {
                throw new Exception("Something went wrong setting city object from Database info\n", innerException);
            }
        }

        /// <summary>
        /// Constructor to update information for a customer given a customerId
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="CustomerName"></param>
        /// <param name="AddressId"></param>
        /// <param name="Active"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="LastUpdateBy"></param>
        public Customer(int CustomerId, string CustomerName, int AddressId, bool Active, string CreatedBy, string LastUpdateBy)
        {
            customerID = CustomerId;
            customerName = CustomerName;
            addressID = AddressId;
            if(Active == true)
            {
                active = 1;
            }
            else
            {
                active = 0;
            }
            createdBy = CreatedBy;
            lastUpdateBy = LastUpdateBy;
        }

        // Class Methods
        /// <summary>
        /// Sets the Customer Object with Database info
        /// </summary>
        /// <param name="existingCustomerId"></param>
        /// <param name="conn"></param>
        private void setDatabaseCustomerInfo(int existingCustomerId, MySqlConnection conn)
        {
            using (conn)
            {
                string myExecuteQuery = "SELECT * FROM customer WHERE customerId = " + existingCustomerId.ToString();
                MySqlDataReader reader = DataChanges.GetMySqlReader(conn, myExecuteQuery);
                while (reader.Read())
                {
                    customerID = (int)reader["customerId"];
                    customerName = reader["customerName"].ToString();
                    addressID = (int)reader["addressId"];
                    bool activeCheck = (bool)reader["active"];
                    if(activeCheck == true)
                    {
                        active = 1;
                    }
                    else
                    {
                        active = 0;
                    }
                    createdBy = reader["createdBy"].ToString();
                    lastUpdateBy = reader["lastUpdateBy"].ToString();
                }
                conn.Close();

                if (customerID != existingCustomerId)
                {
                    throw new Exception("Customer ID wasn't found");
                }
                addressObject = new Address(addressID, conn);
            }
        }
    }
}
