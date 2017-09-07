using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSOncologyCanvas.Model;
using WSOncologyCanvas.Utility;

namespace WSOncologyCanvas.Controller
{
    public class IdentityController
    {
        public const string IDA         = "IDA";
        public const string IDENT_ID    = "IDENT_ID"; 
        public const string LAST_ID     = "LAST_ID";
        public const string CREATE_DTTM = "CREATE_DTTM";
        private const int ZERO = 0;

        public static List<Identity> GetAllIdentities()
        {
            // init dal settings
            DAL settings = new DAL();
            // make new list of identities 
            List<Identity> identities = new List<Identity>();
            using (SqlConnection connection = new SqlConnection(DALUtility.BuildConnectionString()))
            {
                // open connection
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = settings.GET_ALL_PATIENTS;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LASTID", DBNull.Value);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            identities.Add(new Identity(reader));
                        }
                    }
                }
                catch (Exception exception)
                {
                    string parameters = "all patients";
                    Shields_Error_Logger.LogError(exception, "GetAllIdentities(string lastId)", parameters);
                }
            }

            return identities;
        }

        public static List<Identity> GetIdentitiesFromLastIndex(int lastId)
        {
            // init dal settings
            DAL settings = new DAL();
            // make new list of identities 
            List<Identity> identities = new List<Identity>();
            using (SqlConnection connection = new SqlConnection(DALUtility.BuildConnectionString()))
            {
                // open connection
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = settings.GET_ALL_PATIENTS;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LASTID", lastId);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            identities.Add(new Identity(reader));
                        }
                    }
                }
                catch (Exception exception)
                {
                    string parameters = lastId.ToString();
                    Shields_Error_Logger.LogError(exception, "GetIdentitiesFromLastIndex(string lastId)", parameters);
                }
            }
            return identities;
        }

        public static Identity GetLastIdentity()
        {
            // set up dal settings
            DAL settings = new DAL();
            // init new identity object
            Identity identity = new Identity() { lastId = ZERO };

            using (SqlConnection connection = new SqlConnection(DALUtility.BuildConnectionString()))
            {
                // open connection
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = settings.GET_LAST_PT;
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                           identity = new Identity(reader,true);
                        }
                    }
                }
                catch (Exception exception)
                {
                    string parameters = identity.lastId.ToString();
                    Shields_Error_Logger.LogError(exception, "GetAllIdentities(string lastId)", parameters);
                }
            }

            return identity;
        }
    }
}
