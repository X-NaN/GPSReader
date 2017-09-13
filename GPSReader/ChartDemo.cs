using dotnetCHARTING.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPSReader
{
    public partial class ChartDemo : Form
    {

        string DIR = "";

        public ChartDemo()
        {
            InitializeComponent();
        }

        private void ChartDemo_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Date", System.Type.GetType("System.String"));
            dt.Columns.Add("Price", System.Type.GetType("System.String"));
            DataRow dr1 = dt.NewRow();
            dr1["Date"] = "2017-7-21";
            dr1["Price"] = "200";
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["Date"] = "2017-7-22";
            dr2["Price"] = "156";
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["Date"] = "2017-7-23";
            dr3["Price"] = "305";
            dt.Rows.Add(dr3);

            ChartHelper ch = new ChartHelper();

            ch.Title = "2008年各月载客量";
            ch.XTitle = "月份";
            ch.YTitle = "载客量(万人)";
            ch.PicHight = 350;
            ch.PicWidth = 650;
            ch.SeriesName = "合计";//仅对于DataTable类型做数据源时，此属性有效
            ch.PhaysicalImagePath = "ChartImages";//统计图片存放的文件夹名称，缺少对应的文件夹生成不了统计图片
            //ch.FileName = "Statistics";
           
            ch.DataSource = dt;
           // ch.DataSource = GetDataSource();

            //生成柱状图
            //ch.CreateColumn(this.chart1);

            //生成曲线图
            ch.CreateLine(this.chart1);

            //生成饼图
           // ch.CreatePie(this.chart1);           

        }

        /// <summary>
        /// 保存图片到指定位置
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="filename"></param>
        private void saveDAImage(Chart chart, string filename)
        {
            try
            {
                Image _image = chart.GetChartBitmap() as Image;
                //根目录是否存在  
                if (!Directory.Exists(DIR))
                {
                    Directory.CreateDirectory(DIR);
                }
                //上级目录是否存在  
               // string savePath = DIR + "/" + dateDir;
                string savePath = DIR + "/" ;
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                string filePath = savePath + "/" + filename;
                _image.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                //LogHelper.writeLog(LogHelper.PIC_SAVE_LOG + "_" + filename, string.Format("程序运行过程中发生错误,错误信息如下:\n{0}\n发生错误的程序集为:{1}\n发生错误的具体位置为:\n{2}", e.Message, e.Source, e.StackTrace));
            }
        }   





    }
}
