using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WSOncologyCanvas.Utility
{
    public class FileStoreUtility
    {
        private const string BLANK = "";
        private const string ZERO = "0";
        private const string LOG_TEMPLATE = "[DATE_TIME]: [MESSAGE]";
        private const int NEW_LINE = 12;

        public static bool AppendLocalWebLog(string message)
        {
            DAL settings = new DAL();
            try
            {
                string fileName = settings.LOCAL_WEB_LOG_FILE_NAME;

                if (!File.Exists(fileName))
                {
                    if (!FileStoreUtility.MakeFile(fileName))
                    {
                        return false;
                    }
                }

                string output = LOG_TEMPLATE
                    .Replace("[DATE_TIME]",DateTime.Now.ToShortDateString())
                    .Replace("[MESSAGE]", message) + Convert.ToChar(NEW_LINE);

                File.AppendAllText(fileName,output);
            }
            catch (Exception exception)
            {                
                string parameters = message;
                Shields_Error_Logger.LogError(exception, "AppendLocalWebLog(string message)", parameters);
                return false;
            }

            return true;
        }

        public static bool UpdateIndex(string lastId)
        {
            DAL settings = new DAL();
            try
            {
                // get file name from config
                string fileName = settings.INDEX_STORE_FILE_NAME;

                // if there is no file make one
                if(!File.Exists(fileName))
                {
                    if(!FileStoreUtility.MakeFile(fileName))
                    {
                        return false;
                    }                
                }
                // write the last id
                File.WriteAllText(fileName, lastId);
            }
            catch (Exception exception)
            {
                string parameters = lastId;
                Shields_Error_Logger.LogError(exception, "UpdateIndex(string lastId)", parameters);
                return false;
            }
            return true;
        }

        public static string GetIndex()
        {
            DAL settings = new DAL();
            string lastId = BLANK;
            try
            {
                // get file name from config
                string fileName = settings.INDEX_STORE_FILE_NAME;
                if (!File.Exists(fileName))
                {
                    // make file, set to zero
                    FileStoreUtility.MakeFile(fileName);          
                }

                // get the last id that was placed inside
                lastId = File.ReadAllText(fileName);
            }
            catch (Exception exception)
            {
                string parameters = lastId;
                Shields_Error_Logger.LogError(exception, "GetIndex()", parameters);
                return lastId;
            }
            return lastId;
        }

        private static bool MakeFile(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {                            
                    // make new stream
                    FileStream stream = File.Create(fileName);
                    // force close the stream
                    stream.Close();
                    // write to file
                    File.WriteAllText(fileName, ZERO);
                }
            }
            catch (Exception exception)
            {
                string parameters = fileName;
                Shields_Error_Logger.LogError(exception, "MakeFile(string fileName)", parameters);
                return false;
            }

            return true;
        }

    }
}
