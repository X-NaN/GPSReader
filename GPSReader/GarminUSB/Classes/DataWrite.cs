using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/**
 * 2017.7.24 xnn GPS数据写入类
 * 
 * 本类从GarminUSB的接口中读取数据，
 * 并将数据存入txt文件中
 * 
 * 多线程实现，主线程UI显示
 * 
 * 
 * */
namespace GPSReader.GarminUSB.Classes
{
    public class DataWrite
    {
        public delegate void UpdateUI(string gpsData);//声明一个更新主线程的委托
        public UpdateUI UpdateUIDelegate;

        public delegate void AccomplishTask();//声明一个在完成任务时通知主线程的委托
        public AccomplishTask TaskCallBack;

        public void writeData()
        {
            try
            {
                string strRcv = null;

                //!!!静态类里面的静态方法，要通过类调用，竟然忘记了！！！太笨
               // GarminUSB.Classes.DLL.GarminUSB.setGarminUSBReady();

                for (; ; )
                {
                    //输出当前时间
                    DateTime dt = DateTime.Now;

                    IntPtr ptrRet = GarminUSB.Classes.DLL.GarminUSB.getGPSData();//获得返回数据所在地址
                    strRcv = Marshal.PtrToStringAnsi(ptrRet);//通过地址查找接收到的GPS数据

                    //一下两种方式都可以，但是方法二可能会出现乱码，偶现  2017.7.21 xnn
                    // 方法一  按字符判断
                    char[] temp = strRcv.ToCharArray();
                    strRcv = null;
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (!temp[i].Equals('\r') && !temp[i].Equals('\n'))
                        {
                            strRcv += temp[i];
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (strRcv.ToCharArray()[0] != '$')
                    {
                        //程序异常，输出乱码
                        UpdateUIDelegate("error");
                        return;
                    }
                    else
                    {
                        //保存GPS数据
                        saveData(strRcv);
                        //打印时间+GPS数据
                        // saveData(dt.ToLocalTime().ToString() + "\r\n" + strRcv );     
                        UpdateUIDelegate(strRcv);
                    }
                    

                }
            }
            catch (Exception ex)
            {
                //写错误日志
                Log.WriteLog.Write("DataWrite", "writeData", ex.Message);
            }
           
        }


        /// <summary>
        /// 写文件
        /// 保存GPS数据至txt文件
        /// </summary>
        /// <param name="data"></param>
        private void saveData(string data)
        {

            try
            {
                string filePath = "D:\\项目\\GPS自动化测试系统\\GPSReader\\GPSReader\\GPSReader\\Data\\USB";
                string filename = DateTime.Now.ToString("yyyyMMddHH");//// 年月日时分
                string file = filePath + "\\" + filename + ".txt";
                if (!File.Exists(file))
                {
                    FileStream fs = new FileStream(file, FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs);
                    //设置在txt文件末尾追加数据，默认是在文件开头
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    //写入数据
                    sw.WriteLine(data);
                    sw.Close();
                    fs.Close();
                }
                else
                {
                    FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    //设置在txt文件末尾追加数据，默认是在文件开头
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(data);
                    sw.Close();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Error:" + ex.Message, "Error");
                Log.WriteLog.Write("DataWrite", "saveData", ex.Message);
                return;
            }

        }



    }
}
