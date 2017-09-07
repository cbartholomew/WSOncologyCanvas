using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WSOncologyCanvas.Controller;
using WSOncologyCanvas.Utility;
namespace WSOncologyCanvas.Model
{
    public class Identity : Patient
    {
        public int identId { get; set; }
        public string ida { get; set; }
        public DateTime createDateTime { get; set; }        
        // last ident
        public int lastId { get; set; }

        public Identity()
        { 
                    
        }

        public Identity(IDataRecord record, bool isLastIdCheck = false)
        {
            if (!isLastIdCheck)
            {
                this.ida                  = DALUtility.GetData<string>(record, IdentityController.IDA); 
                this.identId              = DALUtility.GetData<int>(record, IdentityController.IDENT_ID);
                this.createDateTime       = DALUtility.GetData<DateTime>(record, IdentityController.CREATE_DTTM);
                this.firstName            = DALUtility.GetData<string>(record, PatientController.FIRST_NAME);
                this.lastName             = DALUtility.GetData<string>(record, PatientController.LAST_NAME);
                this.dateOfBirth          = DALUtility.GetData<string>(record, PatientController.BIRTH_DTTM);
                this.socialSecurityNumber = DALUtility.GetData<string>(record, PatientController.SOCIAL_SECURITY_NUMBER);

                // handle the patient date of birth
                PatientUtility.handleDateOfBirth(this);
            }
            else
            {
                this.lastId = DALUtility.GetData<int>(record, IdentityController.LAST_ID);
            }
        }
        
    }
}
