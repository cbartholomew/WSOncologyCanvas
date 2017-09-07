using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WSOncologyCanvas.Controller;
using WSOncologyCanvas.Model;
using WSOncologyCanvas.Utility;

namespace WSOncologyCanvas
{
    public static class Program
    {
        public const string BLANK = "";
        public const int ZERO = 0;
        public const int SUBMISSION = 1;
        static void Main(string[] args)
        {
            if (args.Length == ZERO)
            {
                CanvasController.doReferenceUpdate();
            }
            else
            {
                // running different argument
                string argType = args[ZERO];

                // based on argument, run different request
                switch (Convert.ToInt32(argType))
                {
                    case SUBMISSION:
                        CanvasController.doSubmissionGet();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
