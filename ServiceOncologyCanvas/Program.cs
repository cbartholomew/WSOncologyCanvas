using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServiceOncologyCanvas
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            OncologyCanvas canvas = new OncologyCanvas();
            #if DEBUG
                canvas.onDebugStart();
            #else

                        ServiceBase[] ServicesToRun;
                        ServicesToRun = new ServiceBase[] 
                        { 
                            new OncologyCanvas() 
                        };
                        ServiceBase.Run(ServicesToRun);
            #endif
        }
    }
}
