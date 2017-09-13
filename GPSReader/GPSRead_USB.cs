using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GPSReader.USB.Classes;
using GPSReader.USB.Structures;
using System.Diagnostics;

using System.Runtime.InteropServices;
using GPSReader.GarminUSB.Classes.DLL;
using GPSReader.GarminUSB.Structures;
using System.IO;
using GPSReader.GarminUSB.Classes;
using System.Threading;


/**
 * 2017.7.24 xnn GPSReader
 * 
 * 通过多线程实现GPS数据的接收和显示
 * 一个线程负责接收GPS数据，更新数据到主线程的UI
 * 主线程负责显示
 * 
 * */
namespace GPSReader
{
    public partial class GPSReader_USB : Form
    {
       // DeviceChangeNotifier deviceNotifiter = new DeviceChangeNotifier();
        DeviceInformationStructure device = new DeviceInformationStructure();//USB外设

        delegate void AsynUpdateUI(string s);

      
        public GPSReader_USB()
        {
            InitializeComponent();
        }

        /// <summary>  
        /// 定义委托  
        /// </summary>  
        /// <param name="a"></param>  
        public delegate void ShowGPSData(string gpsData);

        /// <summary>  
        /// GPS数据显示在文本框  
        /// </summary>  
        /// <param name="a"></param>  
        public void show_GPSData(string gpsData)
        {
            this.rtxt_DataRecv.Text += gpsData + "\r\n"; ;

        }

        /// <summary>  
        /// 声明委托 
        /// </summary>  
        ShowGPSData showGPS;  


        private void GPSReader_Load(object sender, EventArgs e)
        {
            try
            {
                #region 检测USB设备
                showGPS = new ShowGPSData(show_GPSData);//初始化委托  

                device.TargetVendorId=0x46d;
                device.TargetProductId=0xc077;

                device.TargetVendorId = 0x413c;
                device.TargetProductId = 0x2107;

               // device.TargetVendorId = 0x091E;
                //device.TargetProductId = 0xc077;

                bool DevFound=DeviceDiscovery.FindTargetDevice(ref device);
                if (DevFound)
                {
                    MessageBox.Show("检测到USB外设！", "测试测试", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                #endregion              

                
            }
            catch (Exception ex)
            {
                Log.WriteLog.Write("GPSReader_USB", "GPSReader_Load", ex.Message);
               
            }
        }
         

        /// <summary>
        /// 开始接收GPS数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_StartRecv_Click(object sender, EventArgs e)
        {
          
            try
            { 
                this.tsReceiveOrNot.ForeColor = Color.Red;
                int flag = GarminUSB.Classes.DLL.GarminUSB.setGarminUSBReady();
                if (flag == 0)
                {
                   
                    this.tsReceiveOrNot.Text = "状态：GarminUSB接收机未插入！";//状态栏信息
                    MessageBox.Show("请插入GarminUSB接收机！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                

                this.btn_SartRecv.Enabled = false;

                string strRcv=null ;
                
                this.tsReceiveOrNot.Text = "状态：GPS数据接收中。。。。。。";//状态栏信息
              
               // rtxt_DataRecv.SelectAll();
               // rtxt_DataRecv.SelectionColor = Color.Blue;         //改变字体的颜色

                #region 注释掉

                /* 
                //!!!静态类里面的静态方法，要通过类调用，竟然忘记了！！！太笨
                GarminUSB.Classes.DLL.GarminUSB.setGarminUSBReady();

                for (; ; )
                {

                    this.tsReceiveOrNot.Text = "状态：GPS数据中。。。。。。";
                    //在控件上用程序加了东西要refresh才看的到  2017.7.21 xnn
                    this.Refresh();

                    //输出当前时间
                    DateTime dt = DateTime.Now;

                    // rtxt_DataRecv.Text += dt.GetDateTimeFormats('f')[0].ToString() + "\r\n";
                    rtxt_DataRecv.Text += dt.ToLocalTime().ToString() + "\r\n";
                    rtxt_DataRecv.SelectAll();
                    rtxt_DataRecv.SelectionColor = Color.Blue;         //改变字体的颜色


                    IntPtr ptrRet = GarminUSB.Classes.DLL.GarminUSB.getGPSData();//获得返回数据所在地址
                    strRcv = Marshal.PtrToStringAnsi(ptrRet);//通过地址查找接收到的GPS数据

                    //一下两种方式都可以，但是方法二可能会出现乱码，偶现  2017.7.21 xnn
                    /*方法一  按字符判断
                    char[] temp = strRcv.ToCharArray();
                    strRcv=null;
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (!temp[i].Equals('\r')  && !temp[i].Equals('\n') )
                        {
                            strRcv += temp[i];
                        }
                        else
                        {
                            break;
                        }
                    }
                     */
                /*       
                      //方法二   截取字符串处理方式
                      int index = strRcv.LastIndexOf('\r');
                      strRcv = strRcv.Substring(0, index);                  

                     // rtxt_DataRecv.Text += strRcv + "\r\n";             //第一种，直接显示信息在文本框

                      this.Invoke(showGPS, strRcv);  //第二种，通过委托，将GPS数据显示在文本框中 2017.7.21 xnn

                      //保存GPS数据
                      saveData(strRcv + "\r\n");
                      //打印时间+GPS数据
                     // saveData(dt.ToLocalTime().ToString() + "\r\n" + strRcv );                   
                  }
                  */

                #endregion 

                DataWrite writeGPSData = new DataWrite();
                writeGPSData.UpdateUIDelegate += UpdataUIStatus;//绑定更新任务状态的委托


                Thread thread = new Thread(new ThreadStart(writeGPSData.writeData));
                thread.IsBackground = true;//后台
                thread.Start();

               

            }
            catch (Exception ex)
            {
                Log.WriteLog.Write("GPSReader_USB", "btn_StartRecv_Click", ex.Message);
               // MessageBox.Show(ex.Message, "出错提示btn_Switch_Click");
                return;
            }
        }


        /// <summary>
        /// 将GPS数据实时显示在文本框中
        /// </summary>
        /// <param name="gps"></param>
        private void UpdataUIStatus(string gps)
        {
            try
            {
                if (gps.Equals("error"))
                {
                    //程序异常，输出乱码                    
                    this.tsReceiveOrNot.ForeColor = Color.Red;
                    this.tsReceiveOrNot.Text = "状态：GarminUSB接收机被移出，数据接收中止！";//状态栏信息
                    MessageBox.Show("GarminUSB接收机被移出！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);                   
                    return;
                }                


                if (this.rtxt_DataRecv.InvokeRequired)
                {
                    //输出当前时间
                    DateTime dt = DateTime.Now;
                    string datetime = dt.ToLocalTime().ToString() + "\r\n";

                    this.Invoke(new AsynUpdateUI(delegate(string gpsData)
                    {
                        //this.rtxt_DataRecv.Text += gpsData + "\r\n"; 
                        this.rtxt_DataRecv.Text += datetime + gpsData + "\r\n";

                    }), gps);

                    //this.Invoke(testclass.mainThread, new object[] { gps });  
                }
                else
                {
                    //gps与gpsData表示相同的数据
                    this.rtxt_DataRecv.Text += gps + "\r\n";

                }
            }
            catch (Exception ex)
            {
                Log.WriteLog.Write("GPSReader_USB", "UpdataUIStatus", ex.Message);               
                return;
            }
            
        }

        /// <summary>
        /// 完成任务时需要调用
        /// </summary>
        private void Accomplish()
        {
            //还可以进行其他的一些完任务完成之后的逻辑处理
            MessageBox.Show("任务完成");
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
                MessageBox.Show("Error:" + ex.Message, "Error");
                return;
            }

        }



        /// <summary>
        /// 停止接收GPS数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_StopRecv_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0); 
        }     


    }
}
