using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WSOncologyCanvas.Controller;
using WSOncologyCanvas.Model;
using WSOncologyCanvas.Utility;
using System.Threading;

namespace ServiceOncologyCanvas
{
    public partial class OncologyCanvas : ServiceBase
    {
        Oncology canvasHandler = new Oncology();

        public OncologyCanvas()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {    
            // To run under "Debug" mode - comment the line below
            ThreadPool.QueueUserWorkItem(new WaitCallback(startService), canvasHandler);

            // To run under "Debug" mode - un-comment the line below.
            // canvasHandler.Start();
        }

        private void startService(object state)
        {
            Oncology canvasHandler = (Oncology)state;
            canvasHandler.Start();
        }

        protected override void OnStop()
        {
            canvasHandler.Stop();
        }

        public void onDebugStart()
        {
            OnStart(null);
        }
       
    }
}
