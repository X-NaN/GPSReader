using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotnetCHARTING.WinForms;//

/**
 * 2017.7.24 xnn ChartHelper
 * 
 * 生成统计图：柱形图、曲线图、饼图
 * 
 * */
namespace GPSReader
{
    class ChartHelper
    {
        #region 属性
        private string _phaysicalimagepath;//图片存放路径  
        private string _title; //图片标题  
        private string _xtitle;//图片x坐标名称  
        private string _ytitle;//图片y坐标名称  
        private string _seriesname;//图例名称  
        private int _picwidth;//图片宽度  
        private int _pichight;//图片高度  
        private DataTable _dt;//图片数据源  

        private string _filename;//

        /// <summary>
        /// 图片存放路径
        /// </summary>
        public string PhaysicalImagePath
        {
             set { _phaysicalimagepath = value; }  
            get{return _phaysicalimagepath;}  
        }

        /// <summary>
        /// 图片标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        /// <summary>
        /// 图片x坐标名称
        /// </summary>
        public string XTitle
        {
            set { _xtitle = value; }
            get { return _xtitle; }
        }

        /// <summary>
        /// 图片y坐标的名称
        /// </summary>
        public string YTitle
        {
            set { _ytitle = value; }
            get { return _ytitle; }
        }

        /// <summary>
        /// 图例名称
        /// </summary>
        public string SeriesName
        {
            set { _seriesname = value; }
            get { return _seriesname; }
        }

        /// <summary>
        /// 图片宽度
        /// </summary>
        public int PicWidth
        {
            set { _picwidth = value; }
            get { return _picwidth; }
        }

        /// <summary>
        /// 图片高度
        /// </summary>
        public int PicHight
        {
            set { _pichight = value; }
            get { return _pichight; }
        }

        /// <summary>
        /// 图片数据源
        /// </summary>
        public DataTable DataSource  
        {
            set { _dt = value; }
            get { return _dt; }
        }


        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }

        #endregion

        #region 构造函数
       
         public ChartHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

         public ChartHelper(string PhaysicalImagePath, string Title, string XTitle, string YTitle, string SeriesName)
        {
            _phaysicalimagepath=PhaysicalImagePath;
            _title=Title;
            _xtitle=XTitle;
            _ytitle=YTitle;
            _seriesname=SeriesName;  
        }
        #endregion


        #region 输出柱形图

        /// <summary>
        /// 生成柱形图
        /// </summary>
         /// <param name="chart">bin目录下添加dotnetCharting.Winform.dll，且解决方案中添加该引用</param>
         public void CreateColumn(Chart  chart)
         {
             chart.Title = this._title;
             chart.XAxis.Label.Text = this._xtitle;
             chart.YAxis.Label.Text = this._ytitle;
             chart.TempDirectory = this._phaysicalimagepath;
             chart.FileManager.FileName = "xxg";//
             chart.Width = this._picwidth;
             chart.Height = this._pichight;


             //一个chart可以绘制多个ChartArea，每个ChartArea都可以绘制多条Series。
             //ChartArea就是就是绘图区域，可以有多个ChartArea叠加在一起，series是画在ChartAarea上的
             chart.Type = ChartType.Combo;
             chart.Series.Type = SeriesType.Cylinder;

             chart.Series.Name = this._seriesname;
             chart.Series.Data = this._dt;
             chart.SeriesCollection.Add();
             chart.DefaultSeries.DefaultElement.ShowValue = true;
             chart.ShadingEffect = true;
             chart.Use3D = false;
             chart.Series.DefaultElement.ShowValue = true;  

         }

        #endregion

        #region 输出饼图

         /// <summary>
         /// 生成饼图
         /// </summary>
         /// <param name="chart"></param>
         public void CreatePie(Chart chart)
         {
             chart.Title = this._title;
             chart.XAxis.Label.Text = this._xtitle;
             chart.YAxis.Label.Text = this._ytitle;
             chart.TempDirectory = this._phaysicalimagepath;
             chart.FileManager.FileName = "xxg";//
             chart.Width = this._picwidth;
             chart.Height = this._pichight;

             chart.Type = ChartType.Pie;
             chart.Series.Type = SeriesType.Cylinder;//数据线，它可以是曲线、点、柱形、条形、饼图

             chart.ShadingEffect = true;
             chart.Use3D = false;
             chart.DefaultSeries.DefaultElement.Transparency = 20;
             chart.DefaultSeries.DefaultElement.ShowValue = true;
             chart.PieLabelMode = PieLabelMode.Outside;
             chart.SeriesCollection.Add(getArrayData());
             chart.Series.DefaultElement.ShowValue = true;

         }

         private SeriesCollection getArrayData()
         {
             SeriesCollection SC = new SeriesCollection();
             DataTable dt = this._dt;

             for (int i = 0; i < dt.Rows.Count; i++)
             {
                 Series s = new Series();
                 s.Name = dt.Rows[i][0].ToString();

                 Element e = new Element();

                 // 每元素的名称  
                 e.Name = dt.Rows[i][0].ToString();

                 // 每元素的大小数值  
                 e.YValue = Convert.ToInt32(dt.Rows[i][1].ToString());

                 s.Elements.Add(e);
                 SC.Add(s);
             };

             return SC;
         }  


        #endregion

        #region 输出曲线图

        /// <summary>
        /// 生成曲线图
        /// </summary>
        /// <param name="chart"></param>
         public void CreateLine(Chart chart)
         {
             chart.Title = this._title;
             chart.XAxis.Label.Text = this._xtitle;
             chart.YAxis.Label.Text = this._ytitle;
             chart.TempDirectory = this._phaysicalimagepath;
             chart.Width = this._picwidth;
             chart.Height = this._pichight;
             chart.Type = ChartType.Combo;
             chart.Series.Type = SeriesType.Line;
             chart.Series.Name = this._seriesname;
             chart.Series.Data = this._dt;
             chart.SeriesCollection.Add();
             chart.DefaultSeries.DefaultElement.ShowValue = true;
             chart.ShadingEffect = true;
             chart.Use3D = false;
             chart.Series.DefaultElement.ShowValue = true;  
         }

        #endregion


        #region 调用说明及范例
        //        在要显示统计图的页面代码直接调用，方法类似如下：
        //
        //        ChartHelper ch=new ChartHelper();   
        //        ch.Title ="2008年各月消费情况统计";
        //        ch.XTitle ="月份";
        //        ch.YTitle ="金额(万元)";
        //        ch.PicHight =300;
        //        ch.PicWidth =600;
        //        ch.SeriesName ="具体详情";
        //        ch.PhaysicalImagePath ="ChartImages";
        //绑定数据源
        //        ch.DataSource =this.GetDataSource();
       // 绘图
        //        ch.CreateColumn(this.Chart1);    
        #endregion


    }
}
