using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSOncologyCanvas.Utility
{
    public class DALUtility
    {
        public static T GetData<T>(IDataRecord reader, string columnName, bool ignoreException = true)
        {
            try
            {
                return (T)Convert.ChangeType(((reader[columnName] == DBNull.Value) ? default(T) : reader[columnName]), typeof(T));
            }
            catch (Exception e)
            {
                if (ignoreException)
                {
                    return default(T);
                }
                Shields_Error_Logger.LogError(e, "getData(IDataRecord reader, string columnName)", reader + "|" + columnName);
                throw e;
            }
        }

        public static string BuildConnectionString()
        {
            // set settings
            DAL settings = new DAL();

            // get base connection string
            string connection_string = settings.CONNECTION_STRING;

            // build custom connectionstring
            connection_string = connection_string.Replace("[SERVER]", settings.ONCOLOGY_SERVER);
            connection_string = connection_string.Replace("[DATABASE]", settings.ONCOLOGY_CATALOG);
            connection_string = connection_string.Replace("[USERNAME]", settings.ONCOLOGY_USER);
            connection_string = connection_string.Replace("[PASSWORD]", settings.ONCOLOGY_PWD);
            return connection_string;
        }
    }
}
