namespace CalculatePolygon
{
    partial class MainForm
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
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shapefile文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.多边形面积ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计算球面上梯形面积ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.地图MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.墨卡托投影反算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件FToolStripMenuItem.Text = "文件(F)";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shapefile文件ToolStripMenuItem});
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            // 
            // shapefile文件ToolStripMenuItem
            // 
            this.shapefile文件ToolStripMenuItem.Name = "shapefile文件ToolStripMenuItem";
            this.shapefile文件ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.shapefile文件ToolStripMenuItem.Text = "shapefile文件";
            this.shapefile文件ToolStripMenuItem.Click += new System.EventHandler(this.shapefile文件ToolStripMenuItem_Click);
            // 
            // 工具TToolStripMenuItem
            // 
            this.工具TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.多边形面积ToolStripMenuItem,
            this.计算球面上梯形面积ToolStripMenuItem});
            this.工具TToolStripMenuItem.Name = "工具TToolStripMenuItem";
            this.工具TToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.工具TToolStripMenuItem.Text = "工具(T)";
            // 
            // 多边形面积ToolStripMenuItem
            // 
            this.多边形面积ToolStripMenuItem.Name = "多边形面积ToolStripMenuItem";
            this.多边形面积ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.多边形面积ToolStripMenuItem.Text = "计算平面上多边形面积";
            this.多边形面积ToolStripMenuItem.Click += new System.EventHandler(this.多边形面积ToolStripMenuItem_Click);
            // 
            // 计算球面上梯形面积ToolStripMenuItem
            // 
            this.计算球面上梯形面积ToolStripMenuItem.Name = "计算球面上梯形面积ToolStripMenuItem";
            this.计算球面上梯形面积ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.计算球面上梯形面积ToolStripMenuItem.Text = "计算球面上梯形面积";
            this.计算球面上梯形面积ToolStripMenuItem.Click += new System.EventHandler(this.计算球面上梯形面积ToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.地图MToolStripMenuItem,
            this.工具TToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(791, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 地图MToolStripMenuItem
            // 
            this.地图MToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.墨卡托投影反算ToolStripMenuItem});
            this.地图MToolStripMenuItem.Name = "地图MToolStripMenuItem";
            this.地图MToolStripMenuItem.Size = new System.Drawing.Size(64, 21);
            this.地图MToolStripMenuItem.Text = "地图(M)";
            // 
            // 墨卡托投影反算ToolStripMenuItem
            // 
            this.墨卡托投影反算ToolStripMenuItem.Name = "墨卡托投影反算ToolStripMenuItem";
            this.墨卡托投影反算ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.墨卡托投影反算ToolStripMenuItem.Text = "墨卡托投影反算";
            this.墨卡托投影反算ToolStripMenuItem.Click += new System.EventHandler(this.墨卡托投影反算ToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(767, 393);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(791, 428);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBox1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "计算多边形面积";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 多边形面积ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 计算球面上梯形面积ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem 地图MToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 墨卡托投影反算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shapefile文件ToolStripMenuItem;

    }
}

