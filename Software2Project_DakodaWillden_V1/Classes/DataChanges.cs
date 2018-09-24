using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;


namespace Software2Project_DakodaWillden_V1.Classes
{
    public class DataChanges
    {
        //Class Variables
        public string filePath;

        /// <summary>
        /// Opens a connection, gets a Data Reader, and returns the reader. Connection needs to be closed after reader is used
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="SqlQuery"></param>
        /// <returns></returns>
        public static MySqlDataReader GetMySqlReader(MySqlConnection conn, string SqlQuery)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = SqlQuery;
            if (conn.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
                //conn.Open();
            }
            MySqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// Executes an Insert, Update or Delete Query
        /// </summary>
        /// <param name="myExecuteQuery"></param>
        /// <param name="myConnection"></param>
        public static void ExecuteMySqlCommand(string myExecuteQuery, MySqlConnection myConnection)
        {
            try
            {
                MySqlCommand myCommand = new MySqlCommand(myExecuteQuery, myConnection);
                if (myConnection.State == ConnectionState.Closed)
                {
                    myCommand.Connection.Open();
                }
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            catch(Exception e)
            {
                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
                throw new Exception("There was a problem with executing query " + myExecuteQuery + "/n/n" + e.InnerException.ToString());
                //return;
            }
        }

        /// <summary>
        /// Writes a List of strings to a log file. If the file doesn't exist, it creates it
        /// </summary>
        /// <param name="listOfLogs"></param>
        /// <returns></returns>
        public static bool WriteToLogsFile(List<string> listOfLogs)
        {
            string beginningFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            filePath = Path.Combine(beginningFilePath, "SchedulingLogs.txt");
            try
            {
                //Check if the file already exists
                if (File.Exists(filePath))
                {
                    //If it does, cycle through the list of logs given, and add them to the end of the file
                    using(StreamWriter sw = File.AppendText(filePath))
                    {
                        foreach(var log in listOfLogs)
                            sw.WriteLine(log);
                    }
                }
                //Create a new file if not
                else
                {
                    using(StreamWriter sw = new StreamWriter(filePath))
                    {
                        //Then add an intro message to the log, and cycle through the new logs, and add them to the end of the file
                        string introMessage = "International Consulting Company Software Use Logs: ";
                        sw.WriteLine(introMessage);
                        foreach(var log in listOfLogs)
                            sw.WriteLine(log);
                    }
                }
                return true;
            }
            catch
            {
                return false;   
            }
        }
    }
}
