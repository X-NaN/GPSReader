namespace GPSReader
{
    partial class GPSReader_USB
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_SartRecv = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtxt_DataRecv = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsReceiveOrNot = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btn_StopRecv = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_SartRecv
            // 
            this.btn_SartRecv.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SartRecv.Location = new System.Drawing.Point(153, 20);
            this.btn_SartRecv.Name = "btn_SartRecv";
            this.btn_SartRecv.Size = new System.Drawing.Size(75, 23);
            this.btn_SartRecv.TabIndex = 10;
            this.btn_SartRecv.Text = "开始接收";
            this.btn_SartRecv.UseVisualStyleBackColor = true;
            this.btn_SartRecv.Click += new System.EventHandler(this.btn_StartRecv_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtxt_DataRecv);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(12, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(605, 310);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "接收的GPS数据";
            // 
            // rtxt_DataRecv
            // 
            this.rtxt_DataRecv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxt_DataRecv.Location = new System.Drawing.Point(3, 17);
            this.rtxt_DataRecv.Name = "rtxt_DataRecv";
            this.rtxt_DataRecv.ReadOnly = true;
            this.rtxt_DataRecv.Size = new System.Drawing.Size(599, 290);
            this.rtxt_DataRecv.TabIndex = 0;
            this.rtxt_DataRecv.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsReceiveOrNot,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 397);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(629, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsReceiveOrNot
            // 
            this.tsReceiveOrNot.Name = "tsReceiveOrNot";
            this.tsReceiveOrNot.Size = new System.Drawing.Size(44, 17);
            this.tsReceiveOrNot.Text = "状态：";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Visible = false;
            // 
            // btn_StopRecv
            // 
            this.btn_StopRecv.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_StopRecv.Location = new System.Drawing.Point(370, 20);
            this.btn_StopRecv.Name = "btn_StopRecv";
            this.btn_StopRecv.Size = new System.Drawing.Size(75, 23);
            this.btn_StopRecv.TabIndex = 15;
            this.btn_StopRecv.Text = "停止接收";
            this.btn_StopRecv.UseVisualStyleBackColor = true;
            this.btn_StopRecv.Click += new System.EventHandler(this.btn_StopRecv_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_SartRecv);
            this.groupBox4.Controls.Add(this.btn_StopRecv);
            this.groupBox4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(12, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(599, 58);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "控制开关";
            // 
            // GPSReader_USB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 419);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GPSReader_USB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "获取GPS数据";
            this.Load += new System.EventHandler(this.GPSReader_Load);
            this.groupBox2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_SartRecv;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtxt_DataRecv;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsReceiveOrNot;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btn_StopRecv;
        private System.Windows.Forms.GroupBox groupBox4;


    }
}

