using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using WSOncologyCanvas.Model;
using WSOncologyCanvas.Utility;
using System.Net;
using System.IO;

namespace WSOncologyCanvas.Controller
{
    class XMLController
    {
        public const string XML_LIST = "List";
        public const string XML_LIST_NAME = "Name";
        public const string XML_LIST_ACTION = "Action";
        public const string XML_COLUMNS = "Columns";
        public const string XML_ROWS = "Rows";
        public const string XML_COLUMN = "c";
        public const string XML_VALUE = "v";
        public const string XML_ROW = "r";
        public const string XML_CANVAS = "CanvasResult";
        public const string XML_COL_IDA = "IDA";
        public const string XML_COL_IDENT_ID = "IDENT_ID";
        public const string XML_COL_FIRST_NAME = "First_Name";
        public const string XML_COL_LAST_NAME = "Last_Name";
        public const string XML_COL_BIRTH_DTTM = "Birth_DtTm";
        public const string XML_COL_SS_NUMBER = "SS_Number";
        public const string XML_COL_USER_GROUP = "User_Group";

        private const string BLANK = "";
        private const string METHOD = "POST";
        private const string CONTENT_TYPE = "application/x-www-form-urlencoded; charset=ISO-8859-1";

        public static void doRequest(Canvas canvas, string Payload)
        {
            string webException = BLANK;
            string programException = BLANK;
            string responseString = BLANK;
            // get all xml text
            string XMLDocument = BLANK;
            string requestString = BLANK;
            // set settings up
            API settings = new API();

            if (!String.IsNullOrEmpty(Payload))
            {
                // XML Doc
                XMLDocument = Payload;
                //XMLDoc is the XML data string being submitted. 
                requestString = XMLDocument;
            }
            // set request parameters
            string URL = canvas.URL;
            string uName = canvas.Username;
            string pWord = canvas.Password;

            try
            {
                HttpWebRequest request = null;
                WebResponse response = null;
                // Create a request using a URL that can receive a post. 
                request = (HttpWebRequest)WebRequest.Create(URL);
                // Our credentials for the website.     
                request.Credentials = new NetworkCredential(uName, pWord);
                // Set the Method property of the request to POST.   
                request.Method = METHOD;
                // Set content type
                request.ContentType = CONTENT_TYPE;
                // Wrap the request stream with a text-based writer  
                StreamWriter sw = new StreamWriter(request.GetRequestStream());
                // Write the xml as text into the stream  
                sw.WriteLine(requestString);
                sw.Close();
                // Send the data to the webserver and get the response.  
                response = request.GetResponse();
                if (request != null)
                {
                    // Close the request object 
                    request.GetRequestStream().Close();
                }
                string responseFromServer = string.Empty;
                if (response != null)
                {
                    // start stream leader
                    StreamReader incomingStreamReader =
                        new StreamReader(response.GetResponseStream());
                    // Put the response in a string
                    responseFromServer = incomingStreamReader.ReadToEnd();
                    // close out the stream & response reader
                    incomingStreamReader.Close();
                    response.GetResponseStream().Close();
                    // get the web exception
                    webException = ((HttpWebResponse)response).StatusDescription.ToString();
                    // write xml output 
                    File.WriteAllText(settings.XML_RESPONSE, responseFromServer);                    
                    // write the web exception
                    FileStoreUtility.AppendLocalWebLog(webException);
                }
                // look up response string
                responseString = responseFromServer.ToString();
            }
            catch (WebException webEx)
            {
                webException = webEx.Message.ToString();
            }
            catch (Exception ex)
            {
                programException = ex.Message.ToString();
            }
        }

        public static void BuildXML(Canvas canvas)
        {
            API apiSettings = new API();

            using (XmlWriter writer = XmlWriter.Create(apiSettings.XML_FILE_NAME))
            {
                // begin document write
                writer.WriteStartDocument();

                // <List>
                writer.WriteStartElement(XML_LIST);

                // name/action list attributes
                BuildListElementAttributes(writer, canvas);

                // <Columns></Columns>
                BuildColumnsHeaderElement(writer);

                // <Rows></Rows>
                BuildRowsAndBodyElement(writer, canvas);

                // </List>
                writer.WriteEndElement();

                // end document write
                writer.WriteEndDocument();
            }
        }

        private static void BuildListElementAttributes(XmlWriter writer,
                                            Canvas canvas)
        {
            // <List  Name="" Action = ""
            writer.WriteAttributeString(XML_LIST_ACTION, canvas.Action);
            // <List Name=""
            writer.WriteAttributeString(XML_LIST_NAME, canvas.Name);

        }

        private static void BuildColumnsHeaderElement(XmlWriter writer)
        {
            // provide a list of headers first
            List<string> columnHeaders = new List<string>() { 
                XML_COL_IDA,
                XML_COL_IDENT_ID,
                XML_COL_FIRST_NAME,
                XML_COL_LAST_NAME,
                XML_COL_BIRTH_DTTM,
                XML_COL_SS_NUMBER,
                XML_COL_USER_GROUP
            };

            // <Columns>
            writer.WriteStartElement(XML_COLUMNS);
            foreach (string header in columnHeaders)
            {
                // <c>Header</c>
                writer.WriteElementString(XML_COLUMN, header);
            }
            // </Columns>
            writer.WriteEndElement();
        }

        private static void BuildRowsAndBodyElement(XmlWriter writer,
                                                Canvas canvas)
        {
            // <Rows>
            writer.WriteStartElement(XML_ROWS);
            foreach (Canvas.Row row in canvas.Rows)
            {
                // <r>
                writer.WriteStartElement(XML_ROW);
                foreach (Canvas.Column value in row.columns)
                {
                    // columns should be processed in order
                    switch (value.c)
                    {
                        case XML_COL_IDA:
                            // IDA
                            BuildColumnValueElement(writer, value.v[XML_COL_IDA]);
                            break;
                        case XML_COL_IDENT_ID:
                            // IDENT_ID
                            BuildColumnValueElement(writer, value.v[XML_COL_IDENT_ID]);
                            break;
                        case XML_COL_FIRST_NAME:
                            // FIRST_NAME
                            BuildColumnValueElement(writer, value.v[XML_COL_FIRST_NAME]);
                            break;
                        case XML_COL_LAST_NAME:
                            // LAST_NAME
                            BuildColumnValueElement(writer, value.v[XML_COL_LAST_NAME]);
                            break;
                        case XML_COL_BIRTH_DTTM:
                            // BIRTH_DTTM
                            BuildColumnValueElement(writer, value.v[XML_COL_BIRTH_DTTM]);
                            break;
                        case XML_COL_SS_NUMBER:
                            // SS_NUMBER
                            BuildColumnValueElement(writer, value.v[XML_COL_SS_NUMBER]);
                            break;
                        case XML_COL_USER_GROUP:
                            // User_Group
                            BuildColumnValueElement(writer, value.v[XML_COL_USER_GROUP]);
                            break;
                        default:
                            break;
                    }
                }
                // </r>
                writer.WriteEndElement();
            }
            // </rows>
            writer.WriteEndElement();
        }

        private static void BuildColumnValueElement(XmlWriter writer,
                                                string value)
        {
            // <v>Value</v>
            writer.WriteElementString(XML_VALUE, value);
        }
    }
}
