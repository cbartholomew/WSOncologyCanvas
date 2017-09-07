using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSOncologyCanvas.Utility;

namespace WSOncologyCanvas.Model
{
    public class Canvas
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string URL { get; set; }
        public string Action { get; set; }
        public string UserGroup { get; set; }
        public List<Row> Rows { get; set; }

        public enum RequestType
        {
            ReferencePut,
            SubmissionGet,
            ImageGet
        }


        public Canvas()
        {
            API settings = new API();
            this.Rows   = new List<Row>();
            this.Action = settings.ACTION;
            this.Name   = settings.NAME;
            this.Username = settings.USERNAME;
            this.Password = settings.PASSWORD;
            this.UserGroup = settings.USERGROUP;            
        }

        public class Column
        {
            public Dictionary<string, string> v { get; set; }
            public string c { get; set; }
            public Column()
            {

            }

            public Column(string key, string v)
            {
                this.v = new Dictionary<string, string>();
                this.c = key;                
                this.v[key] = v;        
            }
        }

        public class Row
        {
            public List<Column> columns { get; set; }

            public Row()
            {
                this.columns = new List<Column>();
            }

            public Row(List<Column> columns)
            {
                this.columns = new List<Column>();
                this.columns = columns;
            }
        }
    }
}
