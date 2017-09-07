using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSOncologyCanvas.Controller;
using WSOncologyCanvas.Utility;

namespace WSOncologyCanvas.Model
{
    public class Patient
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        public string socialSecurityNumber { get; set; }
        
        public Patient()
        { 
                    
        }

        public Patient(IDataRecord record)
        {
            this.firstName = DALUtility.GetData<string>(record, PatientController.FIRST_NAME);
            this.lastName = DALUtility.GetData<string>(record, PatientController.LAST_NAME);
            this.dateOfBirth = DALUtility.GetData<string>(record, PatientController.BIRTH_DTTM);
            this.socialSecurityNumber = DALUtility.GetData<string>(record, PatientController.SOCIAL_SECURITY_NUMBER);        
        }
    }
}
