using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Software2Project_DakodaWillden_V1.Classes
{
    public class Country
    {
        //Properties for Class
        public int countryId { get; set; }
        public string country { get; set; }
        public DateTime createDate { get; set; }
        public string createdBy { get; set; }
        public DateTime lastUpdate { get; set; }
        public string lastUpdatedBy { get; set; }

        //Constructors
        /// <summary>
        /// Constructor based on Country Name
        /// </summary>
        /// <param name="CountryName"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="conn"></param>
        public Country(string CountryName, string CreatedBy, MySqlConnection conn)
        {
            //Check to see if Country with given name exists. If not, update the database to add the country
            using (conn)
            {
                int countryCount = 0;
                bool countryExists = false;
                int existingCountryId = 0;
                bool databaseEmpty = true;
                int newMaxCountryId = 0;

                //Get the max value of id instead of counting all of the accounts.
                string myQuery = "SELECT MAX(countryId) FROM country";
                MySqlDataReader reader1 = DataChanges.GetMySqlReader(conn, myQuery);
                //Use this to see if the country exists, and to count the number of countries
                while (reader1.Read())
                {
                    newMaxCountryId = (int)reader1["Max(countryId)"];
                    databaseEmpty = false;
                }
                conn.Close();



                myQuery = "SELECT * FROM country";
                MySqlDataReader reader = DataChanges.GetMySqlReader(conn, myQuery);
                //Use this to see if the country exists, and to count the number of countries
                while (reader.Read())
                {
                    countryCount++;
                    string checkCountry = reader["country"].ToString();

                    if (checkCountry == CountryName)
                    {
                        countryExists = true;
                        var countryId = reader["countryId"];
                        existingCountryId = (int)countryId;
                    }
                }
                conn.Close();
                if(countryExists == true)
                {
                    getDatabaseCountryInfo(existingCountryId, conn);
                }
                else
                {
                    //Increment the number of countries by one to use as the id
                    countryCount++;
                    //Set values of Class
                    if (databaseEmpty == true)
                    {
                        countryId = 1;
                    }
                    else
                    {
                        newMaxCountryId++;
                        countryId = newMaxCountryId; 
                    }
                    country = CountryName;
                    createDate = DateTime.Now;
                    createdBy = CreatedBy;
                    lastUpdate = DateTime.Now;
                    lastUpdatedBy = CreatedBy;

                    //add country to database if it doesn't exist
                    //DataChanges db = new DataChanges();
                    myQuery = "INSERT INTO country VALUES(" + countryId.ToString() + ", '" + country + "', '"
                        + createDate.ToString("yyyy-MM-dd") + "', '" + createdBy + "', '" 
                        + lastUpdate.ToString("yyyy-MM-dd") + "', '" + lastUpdatedBy + "')";
                    DataChanges.ExecuteMySqlCommand(myQuery, conn);
                }

            }
        }

        /// <summary>
        /// Constructor based on Country ID
        /// </summary>
        /// <param name="CountryId"></param>
        /// <param name="conn"></param>
        public Country(int CountryId, MySqlConnection conn)
        {
            //Get the Database Info
            getDatabaseCountryInfo(CountryId, conn);
        }

        //Class Methods
        /// <summary>
        /// Sets the Object's properties with the Database information based on Country ID with the given connection
        /// </summary>
        /// <param name="CountryId"></param>
        /// <param name="conn"></param>
        private void getDatabaseCountryInfo(int CountryId, MySqlConnection conn)
        {
            //Search Database to find country by ID, then populate the information with the returned information.
            using (conn)
            {
                string myExecuteQuery = "SELECT * FROM country WHERE countryId = " + CountryId.ToString();
                MySqlDataReader reader = DataChanges.GetMySqlReader(conn, myExecuteQuery);
                while (reader.Read())
                {
                    countryId = (int)reader["countryId"];
                    country = reader["country"].ToString();
                    createDate = (DateTime)reader["createDate"];
                    createdBy = reader["createdBy"].ToString();
                    lastUpdate = (DateTime)reader["lastUpdate"];
                    lastUpdatedBy = reader["lastUpdateBy"].ToString();
                }
            }
        }
    }
}
