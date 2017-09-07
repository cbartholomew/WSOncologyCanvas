using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Configuration;

namespace WSOncologyCanvas
{
    class Shields_Error_Logger
    {
        public static void LogError(Exception exc,string methodName,string parameters = "",string username = "")
        {
            String hostName = Dns.GetHostName();
            int APP_ID = 13;
            DAL settings = new DAL();

            using (SqlConnection myConnection = new SqlConnection(settings.CONNECTION_STRING_ERROR_LOGGER))
            {
                SqlCommand myCommand = new SqlCommand("INSERT_ERROR", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                // db will generate auto new id
                myCommand.Parameters.AddWithValue("@APPLICATION_ID", APP_ID);
                myCommand.Parameters.AddWithValue("@HOST", hostName);
                myCommand.Parameters.AddWithValue("@MESSAGE", exc.Message);
                myCommand.Parameters.AddWithValue("@STACKTRACE", exc.StackTrace);
                myCommand.Parameters.AddWithValue("@METHODNAME", methodName);
                myCommand.Parameters.AddWithValue("@PARAMETERS", parameters);
                myCommand.Parameters.AddWithValue("@USER", username);
                myCommand.Parameters.AddWithValue("@DATE", DateTime.Now);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public static void WriteErrorToEventViewer(Exception exc)
        {
            EventLog eventLog = new EventLog("");
            eventLog.Source = "WSAcceleratorApps";
            eventLog.WriteEntry("Error: " + exc.Message + " Stack Trace: " + exc.StackTrace, EventLogEntryType.Error);
        }

        public static void WriteMessageToEventViewer(string message)
        {
            EventLog eventLog = new EventLog("");
            eventLog.Source = "WSAcceleratorApps";
            eventLog.WriteEntry("Message: " + message, EventLogEntryType.Information);
        }
    }
}


/*


			try
            {
                Convert.ToDateTime(@"dsdsds");
            }
            catch (Exception exc)
            {
                string parameters = "test param 1" + "|" + "test param 2";
                Shields_Error_Logger.LogError(exc, "Main(string param1,string param2)", parameters);
               
            }

			
*/