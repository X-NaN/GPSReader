using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSReader.Log
{
    class WriteLog
    {
        private static string logInfo = "{0} / {1} / {2} / {3}\r\n";
        public static void Write(string winName, string funcName, string exInfo)
        {
            try
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string str = string.Format(logInfo, time, winName, funcName, exInfo);
                string dirPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                //StartupPath
                string filePath = dirPath + "log.log";
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }
                FileStream fs = new FileStream(filePath, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(str);
                sw.Close();
                fs.Close();
            }
            catch (System.Exception ex)
            {

            }

        }
    }
}
