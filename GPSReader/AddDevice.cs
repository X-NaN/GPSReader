using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPSReader
{
    public partial class AddDevice : Form
    {
        public AddDevice()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SelectFile_Click(object sender, EventArgs e)
        {
            /*
            DialogResult dr = folderBrowserDialog1.ShowDialog();//是调用文件浏览器控件；
            ////是判断文件浏览器控件是否返回ok，即用户是否确定选择。如果确定选择，则弹出用户在文件浏览器中选择的路径：         
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                this.txt_NMEAFile.Text = folderBrowserDialog1.SelectedPath;
            }   
            MessageBox.Show(folderBrowserDialog1.SelectedPath);
             * */


            string Pdfpath = "";
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "word Files(*.doc)|*.doc|All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                Pdfpath = op.FileName;
            }
            else
            {
                Pdfpath = "";
            }
            this.txt_NMEAFile.Text = Pdfpath;
            textBox1.Text = Pdfpath;




        }
    }
}
