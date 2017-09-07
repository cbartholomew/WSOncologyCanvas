using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WSOncologyCanvas.Model;
using WSOncologyCanvas.Utility;

namespace WSOncologyCanvas.Controller
{
    public class CanvasController
    {
        private const string BLANK = "";

        /// <summary>
        /// PutReference()
        /// Static method which is responsible 
        /// for manually updating the patient list in Canvas Oncology App     
        /// </summary>
        public static void doReferenceUpdate()
        {
            API settings = new API();

            // make new canvas object
            Canvas c = new Canvas();

            // set the rows for the object based on the data set
            CanvasUtility.SetCanvasRows(c);

            // set canvas url
            CanvasUtility.SetCanvasURL(c,
                Canvas.RequestType.ReferencePut);

            // don't process request if there are no rows avaliable
            if (c.Rows.Count == 0)
            {
                return;
            }

            // get the last identity
            Identity lastIdentity = IdentityController.GetLastIdentity();

            // update the text file
            FileStoreUtility.UpdateIndex(lastIdentity.lastId.ToString());

            // make xml
            XMLController.BuildXML(c);

            // get payload
            string XMLPayload = File.ReadAllText(settings.XML_FILE_NAME);

            // make request to update fields
            XMLController.doRequest(c, XMLPayload);
        }

        /// <summary>
        /// GetSubmissions()
        /// Returns all form submissions from Canvas based on form name
        /// (can add optional begin_date and end_date, if needed via URL)
        /// Not in use currently. 
        /// </summary>
        public static void doSubmissionGet()
        { 
            // submission form name
            string formName = "HIPAA%20COMPLIANT%20AUTHORIZATION%20FORM";

            // make new canvas object
            Canvas c = new Canvas();
            
            // set the canvas url
            CanvasUtility.SetCanvasURL(c, 
                Canvas.RequestType.SubmissionGet);

            // update url w/ form name
            c.URL = c.URL.Replace("[FORM_NAME]", formName);

            // make the request
            XMLController.doRequest(c,BLANK);
        }


    }
}
