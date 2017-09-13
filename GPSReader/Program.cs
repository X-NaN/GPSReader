using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPSReader
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new GPSRead_COM());
            //Application.Run(new GPSReader_USB());
            Application.Run(new ChartDemo());

            //Application.Run(new TaskCreate());
            //Application.Run(new AddDevice());
        }
    }
}
