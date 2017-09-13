using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GPSReader.COM.INIFILE;
using System.IO;

/**
 * 2017.7.18 xnn
 * 通过虚拟串口读取Garmin GPS USB接收机的GPS数据
 * 
 * */
namespace GPSReader
{
    public partial class GPSRead_COM : Form
    {
        SerialPort sp1 = new SerialPort();//串口对象
       
        public GPSRead_COM()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 检查是否有串口，并进行设置
        /// </summary>
        private void setSerialPorts()
        {
            //检查是否含有串口
            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                MessageBox.Show("本机没有串口！", "Error");
                return;
            }

            //添加串口项目
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {//获取有多少个COM口
                //System.Diagnostics.Debug.WriteLine(s);
                this.cb_Serial.Items.Add(s);
            }
            
        }

        /// <summary>
        /// 绑定串口参数
        /// </summary>
        private void bandSerialParameters()
        {    
            //波特率
            List<string> list_baudRate = new List<string>();
            list_baudRate.Add("300");
            list_baudRate.Add("600");
            list_baudRate.Add("1200");
            list_baudRate.Add("2400");
            list_baudRate.Add("4800");
            list_baudRate.Add("9600");
            list_baudRate.Add("19200");
            list_baudRate.Add("38400");
            list_baudRate.Add("115200");
            this.cb_BaudRate.DataSource = list_baudRate;
            //数据位
            List<string> list_dataBits = new List<string>();
            list_dataBits.Add("5");
            list_dataBits.Add("6");
            list_dataBits.Add("7");
            list_dataBits.Add("8");
            this.cb_DataBits.DataSource = list_dataBits;
            //停止位
            List<string> list_stopBits = new List<string>();
            list_stopBits.Add("1");
            list_stopBits.Add("1.5");
            list_stopBits.Add("2");
            this.cb_StopBits.DataSource = list_stopBits;
            //校验位
            List<string> list_parity = new List<string>();
            list_parity.Add("无");//NONE
            list_parity.Add("奇校验");//ODD
            list_parity.Add("偶校验");//EVEN
            this.cb_Parity.DataSource = list_parity;

        }


        /// <summary>
        /// 根据加载文件，预先设置串口参数：波特率，数据位，停止位，校验位
        /// </summary>
        private void presetSerialParameters()
        {
            Profile.LoadProfile();//加载所有
            switch (Profile.G_BAUDRATE)
            {
                case "300":
                    cb_BaudRate.SelectedIndex = 0;
                    break;
                case "600":
                    cb_BaudRate.SelectedIndex = 1;
                    break;
                case "1200":
                    cb_BaudRate.SelectedIndex = 2;
                    break;
                case "2400":
                    cb_BaudRate.SelectedIndex = 3;
                    break;
                case "4800":
                    cb_BaudRate.SelectedIndex = 4;
                    break;
                case "9600":
                    cb_BaudRate.SelectedIndex = 5;
                    break;
                case "19200":
                    cb_BaudRate.SelectedIndex = 6;
                    break;
                case "38400":
                    cb_BaudRate.SelectedIndex = 7;
                    break;
                case "115200":
                    cb_BaudRate.SelectedIndex = 8;
                    break;
                default:
                    {
                        MessageBox.Show("波特率预置参数错误。");
                        return;
                    }
            }

            //预置数据位
            switch (Profile.G_DATABITS)
            {
                case "5":
                    cb_DataBits.SelectedIndex = 0;
                    break;
                case "6":
                    cb_DataBits.SelectedIndex = 1;
                    break;
                case "7":
                    cb_DataBits.SelectedIndex = 2;
                    break;
                case "8":
                    cb_DataBits.SelectedIndex = 3;
                    break;
                default:
                    {
                        MessageBox.Show("数据位预置参数错误。");
                        return;
                    }

            }
            //预置停止位
            switch (Profile.G_STOP)
            {
                case "1":
                    cb_StopBits.SelectedIndex = 0;
                    break;
                case "1.5":
                    cb_StopBits.SelectedIndex = 1;
                    break;
                case "2":
                    cb_StopBits.SelectedIndex = 2;
                    break;
                default:
                    {
                        MessageBox.Show("停止位预置参数错误。");
                        return;
                    }
                    
            }

            //预置校验位
            switch (Profile.G_PARITY)
            {
                case "NONE":
                    cb_Parity.SelectedIndex = 0;
                    break;
                case "ODD":
                    cb_Parity.SelectedIndex = 1;
                    break;
                case "EVEN":
                    cb_Parity.SelectedIndex = 2;
                    break;
                default:
                    {
                        MessageBox.Show("校验位预置参数错误。");
                        return;
                    }
            }


        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GPSRead_COM_Load(object sender, EventArgs e)
        {
            setSerialPorts();
            bandSerialParameters();
            presetSerialParameters();

            //串口设置默认选择项
            this.cb_Serial.SelectedIndex = 2;         //COM3是NMEA数据 2017.7.18 xnn
            this.cb_BaudRate.SelectedIndex = 5;
            this.cb_DataBits.SelectedIndex = 3;
            this.cb_StopBits.SelectedIndex = 0;
            this.cb_Parity.SelectedIndex = 0;
           
            this.rb_RecvStr.Checked = true;

            Control.CheckForIllegalCrossThreadCalls = false;    //这个类中我们不检查跨线程的调用是否合法(因为.net 2.0以后加强了安全机制,，不允许在winform中直接跨线程访问控件的属性)
           // sp1.ReceivedBytesThreshold = 1;
             sp1.DataReceived += new SerialDataReceivedEventHandler(sp1_DataReceived);
            

        }


        void sp1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (sp1.IsOpen)     //此处可能没有必要判断是否打开串口，但为了严谨性，我还是加上了
            {
                //输出当前时间
                DateTime dt = DateTime.Now;
             
               // rtxt_DataRecv.Text += dt.GetDateTimeFormats('f')[0].ToString() + "\r\n";
                rtxt_DataRecv.Text += dt.ToLocalTime().ToString() + "\r\n";
                rtxt_DataRecv.SelectAll();
                rtxt_DataRecv.SelectionColor = Color.Blue;         //改变字体的颜色

                byte[] byteRead = new byte[sp1.BytesToRead];    //BytesToRead:sp1接收的字符个数

                try
                {
                    int len = sp1.BytesToRead;//要读入字节长度
                    Byte[] readBuffer = new Byte[len];
                    sp1.Read(readBuffer, 0, len); //将数据读入缓存
                    sp1.DiscardInBuffer();                      //清空SerialPort控件的Buffer 

                    //处理readBuffer中的数据，自定义处理过程              
                    if (rb_Recv16.Checked)//16进制显示
                    {
                        string strRcv = null;
                        //int decNum = 0;//存储十进制
                        for (int i = 0; i < readBuffer.Length; i++) //窗体显示
                        {

                            strRcv += readBuffer[i].ToString("X2");  
                        }
                        rtxt_DataRecv.Text += strRcv + "\r\n";
                        //保存GPS数据
                        saveData(strRcv + "\r\n");
                        //打印时间+GPS数据
                        saveData(dt.ToLocalTime().ToString() + "\r\n" + strRcv + "\r\n");

                    }
                    else if (rb_RecvStr.Checked) //这是用以显示字符串
                    {                     
                        string strRcv = null;
                        for (int i = 0; i < readBuffer.Length; i++)
                        {
                            strRcv += ((char)Convert.ToInt32(readBuffer[i]));
                        }
                        rtxt_DataRecv.Text += strRcv + "\r\n";             //显示信息
                        //保存GPS数据
                        saveData(strRcv + "\r\n");
                        //打印时间+GPS数据
                        saveData(dt.ToLocalTime().ToString() + "\r\n"+strRcv + "\r\n");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "出错提示");
                }               
              
               
            }
            else
            {
                MessageBox.Show("请打开某个串口", "错误提示");
            }
        }


        /// <summary>
        /// 串口开关
        /// 打开 或 关闭串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Switch_Click(object sender, EventArgs e)
        {
            if (!sp1.IsOpen)//打开串口
            {
                try
                {
                    //设置串口号
                    string serialName = cb_Serial.SelectedItem.ToString();
                    sp1.PortName = serialName;

                    //设置各“串口设置”
                    string strBaudRate = cb_BaudRate.Text;
                    string strDataBits = cb_DataBits.Text;
                    string strStopBits = cb_StopBits.Text;
                    Int32 iBaudRate = Convert.ToInt32(strBaudRate);
                    Int32 iDataBits = Convert.ToInt32(strDataBits);

                    sp1.BaudRate = iBaudRate;       //波特率
                    sp1.DataBits = iDataBits;       //数据位
                    switch (cb_StopBits.Text)            //停止位
                    {
                        case "1":
                            sp1.StopBits = StopBits.One;
                            break;
                        case "1.5":
                            sp1.StopBits = StopBits.OnePointFive;
                            break;
                        case "2":
                            sp1.StopBits = StopBits.Two;
                            break;
                        default:
                            MessageBox.Show("Error：参数不正确!", "Error");
                            break;
                    }
                    switch (cb_Parity.Text)             //校验位
                    {
                        case "无":
                            sp1.Parity = Parity.None;
                            break;
                        case "奇校验":
                            sp1.Parity = Parity.Odd;
                            break;
                        case "偶校验":
                            sp1.Parity = Parity.Even;
                            break;
                        default:
                            MessageBox.Show("Error：参数不正确!", "Error");
                            break;
                    }

                    if (sp1.IsOpen == true)//如果打开状态，则先关闭一下
                    {
                        sp1.Close();
                    }
                    //状态栏设置
                    tsSpNum.Text = "串口号：" + sp1.PortName + "|";
                    tsBaudRate.Text = "波特率：" + sp1.BaudRate + "|";
                    tsDataBits.Text = "数据位：" + sp1.DataBits + "|";
                    tsStopBits.Text = "停止位：" + sp1.StopBits + "|";
                    tsParity.Text = "校验位：" + sp1.Parity + "|";

                    //设置必要控件不可用
                    cb_Serial.Enabled = false;
                    cb_BaudRate.Enabled = false;
                    cb_DataBits.Enabled = false;
                    cb_StopBits.Enabled = false;
                    cb_Parity.Enabled = false;

                    sp1.Open();     //打开串口
                    btn_Switch.Text = "关闭串口";

                    //创建文件，并打开文件流
                   // sw = createFile();


                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Error");
                   // tmSend.Enabled = false;
                    return;
                }
            }
            else//关闭串口
            {
                //状态栏设置
                tsSpNum.Text = "串口号：未指定|";
                tsBaudRate.Text = "波特率：未指定|";
                tsDataBits.Text = "数据位：未指定|";
                tsStopBits.Text = "停止位：未指定|";
                tsParity.Text = "校验位：未指定|";
                //恢复控件功能
                //设置必要控件不可用
                cb_Serial.Enabled = true;
                cb_BaudRate.Enabled = true;
                cb_DataBits.Enabled = true;
                cb_StopBits.Enabled = true;
                cb_Parity.Enabled = true;

                sp1.Close();                    //关闭串口
                btn_Switch.Text = "打开串口";
                //tmSend.Enabled = false;         //关闭计时器
               
            }
        }

        /// <summary>
        /// NO USE 2017.7.18 xnn
        /// 创建txt文件，并生成一个StreamWriter
        /// </summary>
        /// <returns></returns>
    /*    private StreamWriter createFile()
        {
            string filePath = "D:\\项目\\GPS自动化测试系统\\GPSReader\\GPSReader\\GPSReader\\Data";
            string filename = DateTime.Now.ToString("yyyyMMddHH");//// 年月日时分
            string file = filePath + "\\" + filename + ".txt";
          
            try
            {
                if (!File.Exists(file))
                {
                    fs = new FileStream(file, FileMode.Append);
                    sw = new StreamWriter(fs);

                }
                else
                {
                    fs = new FileStream(file, FileMode.Open, FileAccess.Write);
                    sw = new StreamWriter(fs);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建文件错误，Error:" + ex.Message, "Error");
                
            }                      

            return sw;
        }
        */


        /// <summary>
        /// 写文件
        /// 保存GPS数据至txt文件
        /// </summary>
        /// <param name="data"></param>
        private void saveData(string data)
        {

            try
            {
                string filePath = "D:\\项目\\GPS自动化测试系统\\GPSReader\\GPSReader\\GPSReader\\Data";
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

       


    }
}
