using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using WSOncologyCanvas.Model;
using WSOncologyCanvas.Utility;
using WSOncologyCanvas.Controller;

namespace ServiceOncologyCanvas
{
    public class Oncology
    {
        public const string BLANK = "";
        public const int ZERO = 0;
        public const int SUBMISSION = 1;
        public bool isRunning { get; set; }

        public Oncology()
        {
            // start running
            this.isRunning = true;
        }

        public void Start()
        {
            CanvasController controller = new CanvasController();
            
            while (isRunning)
            {
                CanvasController.doReferenceUpdate();

                Thread.Sleep(1);
            } 
        }

        public void Stop()
        {
            this.isRunning = false;
        }

    }
}
