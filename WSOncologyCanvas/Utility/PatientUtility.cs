using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSOncologyCanvas.Model;

namespace WSOncologyCanvas.Utility
{
    public class PatientUtility
    {
        public static void handleDateOfBirth(Patient patient)
        {
            try
            {
                patient.dateOfBirth = Convert.ToDateTime(patient.dateOfBirth)
                            .ToShortDateString();
        
            }
            catch (Exception exception)
            {
                string parameters = patient.dateOfBirth;
                Shields_Error_Logger.LogError(exception, 
                    "handleDateOfBirth(Patient patient)", parameters);
            }
      
        }

    }
}
