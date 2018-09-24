using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Software2Project_DakodaWillden_V1.Classes
{
    public class City
    {
        //Class Properties
        public int cityId { get; set; }
        public string cityName { get; set; }
        public int countryId { get; set; }
        public DateTime createDate { get; set; }
        public string createdBy { get; set; }
        public DateTime lastUpdate { get; set; }
        public string lastUpdateBy { get; set; }

        public Country countryObject;

        // Constructors
        /// <summary>
        /// City Constructor using City Name
        /// </summary>
        /// <param name="CityName"></param>
        /// <param name="CountryName"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="conn"></param>
        public City(string CityName, string CountryName, string CreatedBy, MySqlConnection conn)
        {
            //Search Database for City that matches.
            //Check to see if Country with given name exists. If not, update the database to add the country
            using (conn)
            {
                countryObject = new Country(CountryName, CreatedBy, conn);
                int cityCount = 0;
                bool cityExists = false;
                int existingCityId = 0;

                bool databaseEmpty = true;
                int newMaxCityId = 0;

                //Get the max value of id instead of counting all of the accounts.
                string myQuery = "SELECT MAX(cityId) FROM city";
                MySqlDataReader reader1 = DataChanges.GetMySqlReader(conn, myQuery);
                //Use this to see if the country exists, and to count the number of countries
                while (reader1.Read())
                {
                    newMaxCityId = (int)reader1["Max(cityId)"];
                    databaseEmpty = false;
                }
                conn.Close();

                myQuery = "SELECT * FROM city";
                MySqlDataReader reader = DataChanges.GetMySqlReader(conn, myQuery);
                //Use this to see if the city exists, and to count the number of cities
                while (reader.Read())
                {
                    cityCount++;
                    string checkCity = reader["city"].ToString();
                    int checkCountry = (int)reader["countryId"];

                    if (checkCity == CityName)
                    {
                        if(countryObject.countryId == checkCountry)
                        {
                            cityExists = true;
                            var cityId = reader["cityId"];
                            existingCityId = (int)cityId;
                        }
                    }
                }
                conn.Close();
                if (cityExists == true)
                {
                    setDatabaseCityInfo(existingCityId, CreatedBy, conn);
                }
                else
                {
                    //Increment the number of countries by one to use as the id
                    cityCount++;
                    if (databaseEmpty == true)
                    {
                        cityId = 1;
                    }
                    else
                    {
                        newMaxCityId++;
                        cityId = newMaxCityId;
                    }
                    //Set values of Class
                    cityName = CityName;
                    //Create a country object to check if a country is already created, and if it's not, create it, and use the id of the existing or new Country
                   
                    countryId = countryObject.countryId;
                    createDate = DateTime.Now;
                    createdBy = CreatedBy;
                    lastUpdate = DateTime.Now;
                    lastUpdateBy = CreatedBy;

                    //add city to database if it doesn't exist
                    //DataChanges db = new DataChanges();
                    myQuery = "INSERT INTO city VALUES(" + cityId.ToString() + ", '" + cityName + "', "
                        + countryId.ToString() + ", '" + createDate.ToString("yyyy-MM-dd") + "', '" + createdBy 
                        + "', '" + lastUpdate.ToString("yyyy-MM-dd") + "', '" + lastUpdateBy + "')";
                    DataChanges.ExecuteMySqlCommand(myQuery, conn);
                }
            }
            //If there isn't one that matches, add it with the information given
        }

        /// <summary>
        /// Searches a city information and throws an error if something goes wrong
        /// </summary>
        /// <param name="CityId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="conn"></param>
        public City(int CityId, string CreatedBy, MySqlConnection conn)
        {
            //Find the City by CityID, and populate the class information with the information given.
            try
            {
                setDatabaseCityInfo(CityId, CreatedBy, conn);
            }
            //Throw an error if ID isn't found
            catch(Exception innerException)
            {
                throw new Exception("Something went wrong setting city object from Database info", innerException);
            }
        }

        //Class Methods
        /// <summary>
        /// Finds a city by the city ID, and fills an object with the information from the database
        /// </summary>
        /// <param name="CityId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="conn"></param>
        private void setDatabaseCityInfo(int CityId, string CreatedBy, MySqlConnection conn)
        {
            using (conn)
            {
                string myExecuteQuery = "SELECT * FROM city WHERE cityId = " + CityId.ToString();
                MySqlDataReader reader = DataChanges.GetMySqlReader(conn, myExecuteQuery);
                while (reader.Read())
                {
                    cityId = (int)reader["cityId"];
                    cityName = reader["city"].ToString();
                    countryId = (int)reader["countryId"];
                    createDate = (DateTime)reader["createDate"];
                    createdBy = reader["createdBy"].ToString();
                    lastUpdate = (DateTime)reader["lastUpdate"];
                    lastUpdateBy = reader["lastUpdateBy"].ToString();
                }
                conn.Close();

                if(cityId != CityId)
                {
                    throw new Exception("City ID wasn't found");
                }
                countryObject = new Country(countryId, conn);
            }
        }
    }
}
