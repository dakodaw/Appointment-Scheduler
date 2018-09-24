using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Software2Project_DakodaWillden_V1.Classes
{
    public class Address
    {
        public int addressId { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public int cityId { get; set; }
        public string postalCode { get; set; }
        public string phone { get; set; }
        public DateTime createDate { get; set; }
        public string createdBy { get; set; }
        public DateTime lastUpdate { get; set; }
        public string lastUpdateBy { get; set; }

        public string loggedInUser { get; set; }
        public City cityObject;

        // Constructors
        /// <summary>
        /// Address Constructor Using Address Information.
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="Address2"></param>
        /// <param name="City"></param>
        /// <param name="CountryName"></param>
        /// <param name="PostalCode"></param>
        /// <param name="Phone"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="conn"></param>
        public Address(string Address, string Address2, string City, String CountryName, string PostalCode, string Phone, string CreatedBy, MySqlConnection conn)
        {
            //Check Database to see if the address matches exactly with any records. 
            //If It does, populate information with information from Database

            //If it's not, use the given information to add a new record to the database
            //Get City ID by searching for matches for City
            //Use current date/time for creade date and last update
            //Throw error if Insert failed
            cityObject = new City(City, CountryName, CreatedBy, conn);
            int addressCount = 0;
            bool addressExists = false;
            int existingAddressId = 0;



            string myQuery = "SELECT * FROM address";
            MySqlDataReader reader = DataChanges.GetMySqlReader(conn, myQuery);
            //Use this to see if the country exists, and to count the number of countries
            while (reader.Read())
            {
                addressCount++;
                string checkAddress = reader["address"].ToString() + reader["address2"].ToString() + reader["cityId"].ToString();
                string givenAddress = Address + Address2 + cityObject.cityId.ToString();
                if (givenAddress == checkAddress)
                {
                    //Set existingAddressId to be able to Call function that fills class from reader by id 
                        //after connection is closed
                    addressExists = true;
                    existingAddressId = (int)reader["addressId"];
                }
                
            }
            conn.Close();
            if (addressExists == true)
            {
                setDatabaseAddressInfo(existingAddressId, conn);
            }
            else
            {
                //Increment the number of addresses by one to use as the id
                addressCount++;
                //Set values of Class
                addressId = addressCount;
                address = Address;
                address2 = Address2;
                //Create a country object to check if a country is already created, and if it's not, create it, and use the id of the existing or new Country
                cityId = cityObject.cityId;
                postalCode = PostalCode;
                phone = Phone;
                createDate = DateTime.Now;
                createdBy = CreatedBy;
                lastUpdate = DateTime.Now;
                lastUpdateBy = CreatedBy;

                //add country to database if it doesn't exist
                //DataChanges db = new DataChanges();
                myQuery = "INSERT INTO address VALUES(" + addressId.ToString() + ", '" + address + "', '"
                    + address2 + "', " + cityId + ", '"  + postalCode + "', '" + phone + "', '" 
                    + createDate.ToString("yyyy-MM-dd") + "', '" + createdBy + "', '" 
                    + lastUpdate.ToString("yyyy-MM-dd") + "', '" + lastUpdateBy + "')";
                DataChanges.ExecuteMySqlCommand(myQuery, conn);
            }
        }

        /// <summary>
        /// Constructor when there is the ID known, just to fill an object
        /// </summary>
        /// <param name="AddressId"></param>
        /// <param name="Address1"></param>
        /// <param name="Address2"></param>
        /// <param name="CityId"></param>
        /// <param name="PostalCode"></param>
        /// <param name="Phone"></param>
        /// <param name="CreateDate"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="LastUpdate"></param>
        /// <param name="LastUpdatedBy"></param>
        public Address(int AddressId, string Address1, string Address2, int CityId, string PostalCode, string Phone, string CreatedBy, string LastUpdatedBy )
        {
            addressId = AddressId;
            address = Address1;
            address2 = Address2;
            cityId = CityId;
            postalCode = PostalCode;
            phone = Phone;
            createdBy = CreatedBy;
            lastUpdateBy = LastUpdatedBy;
        }

        /// <summary>
        /// Address Constructor using Address Id
        /// </summary>
        /// <param name="AddressId"></param>
        /// <param name="conn"></param>
        public Address(int AddressId, MySqlConnection conn)
        {
            //Search Database for address using the given ID, and populate the address with the information returned.
            //Throw an error if not found
            setDatabaseAddressInfo(AddressId, conn);
        }

        //Class Methods
        /// <summary>
        /// Fills the class with Address information from database using addressId
        /// </summary>
        /// <param name="existingAddressId"></param>
        /// <param name="conn"></param>
        private void setDatabaseAddressInfo(int existingAddressId, MySqlConnection conn)
        {
            using (conn)
            {
                string myQuery = "SELECT * FROM address WHERE addressId = " + existingAddressId.ToString();
                MySqlDataReader reader = DataChanges.GetMySqlReader(conn, myQuery);
                while (reader.Read())
                {
                    addressId = (int)reader["addressId"];
                    address = reader["address"].ToString();
                    address2 = reader["address2"].ToString();
                    cityId = (int)reader["cityId"];
                    postalCode = reader["postalCode"].ToString();
                    phone = reader["phone"].ToString();
                    //createDate = (DateTime)reader["createDate"];
                    createdBy = reader["createdBy"].ToString();
                    //lastUpdate = (DateTime)reader["lastUpdate"];
                    lastUpdateBy = reader["lastUpdateBy"].ToString();
                }
                conn.Close();

                if (addressId != existingAddressId)
                {
                    throw new Exception("Address ID wasn't found");
                }
                cityObject = new City(cityId, loggedInUser, conn);
            }
        }
    }
}
