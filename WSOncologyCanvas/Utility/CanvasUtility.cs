using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSOncologyCanvas.Model;
using WSOncologyCanvas.Controller;

namespace WSOncologyCanvas.Utility
{
    public class CanvasUtility
    {
        public const string BLANK = "";  
        
        public static void SetCanvasURL(Canvas canvas, Canvas.RequestType requestType)
        { 
            // make api settings
            API settings = new API();

            switch (requestType)
            {
                case Canvas.RequestType.ReferencePut:
                    // get base URL and replace w/ username and password from settings
                    canvas.URL = settings.REFERENCE_DATA_URL
                        .Replace("[USERNAME]", settings.USERNAME)
                        .Replace("[PASSWORD]", settings.PASSWORD);
                    break;
                case Canvas.RequestType.SubmissionGet:
                    // get base URL and replace w/ username and password from settings
                    canvas.URL = settings.SUBMISSION_DATA_URL
                        .Replace("[USERNAME]", settings.USERNAME)
                        .Replace("[PASSWORD]", settings.PASSWORD);
                    break;
                case Canvas.RequestType.ImageGet:
                    // get base URL and replace w/ username and password from settings
                    canvas.URL = settings.IMAGE_GET_URL
                        .Replace("[USERNAME]", settings.USERNAME)
                        .Replace("[PASSWORD]", settings.PASSWORD);
                    break;
            }                 
        }

        public static void SetCanvasRows(Canvas canvas)
        {
            // get the last index
            int lastIndex = Convert.ToInt32(FileStoreUtility.GetIndex());

            // get the identities from last index
            List<Identity> identities = IdentityController.GetIdentitiesFromLastIndex(lastIndex);

            foreach (Identity ident in identities)
            {
                // new column list
                List<Canvas.Column> columns = new List<Canvas.Column>();

                // add all the necessary columns - should be in order of canvas
                columns.Add(MakeColumn(XMLController.XML_COL_IDA, ident.ida));
                columns.Add(MakeColumn(XMLController.XML_COL_IDENT_ID,ident.identId.ToString()));
                columns.Add(MakeColumn(XMLController.XML_COL_FIRST_NAME,ident.firstName));
                columns.Add(MakeColumn(XMLController.XML_COL_LAST_NAME,ident.lastName));
                columns.Add(MakeColumn(XMLController.XML_COL_BIRTH_DTTM,ident.dateOfBirth));
                columns.Add(MakeColumn(XMLController.XML_COL_SS_NUMBER, ident.socialSecurityNumber));
                // set the user group from canvas object
                columns.Add(MakeColumn(XMLController.XML_COL_USER_GROUP, canvas.UserGroup));

                // make row
                canvas.Rows.Add(MakeRow(columns));
            }

        }

        private static Canvas.Row MakeRow(List<Canvas.Column> columns)
        {
            return new Canvas.Row(columns);        
        }

        private static Canvas.Column MakeColumn(string c, string v)
        {
            if (String.IsNullOrEmpty(v))
            {
                v = BLANK;
            }
            else
            {
                // trim the spaces
                v = v.ToString().Trim();
            }

            return new Canvas.Column(c, v);        
        }
    }
}
