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
    public partial class TaskCreate : Form
    {
        public TaskCreate()
        {
            InitializeComponent();
        }

        private void dgvDevice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDevice_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.ColumnIndex > -1 && e.RowIndex > -1)  //点击的是鼠标右键，并且不是表头
            {
                //右键选中单元格
                this.dgvDevice.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                this.contextMenuStrip1.Show(MousePosition.X, MousePosition.Y); //MousePosition.X, MousePosition.Y 是为了让菜单在所选行的位置显示

            }

        }

        private void btn_AddDevice_Click(object sender, EventArgs e)
        {
            new AddDevice().ShowDialog();
        }

    }
}
