namespace MapProjection
{
    partial class Form1
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.坐标系ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.北京54转兰伯特ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.北京54转墨卡托ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wGS84转墨卡托投影ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.大圆轨迹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(724, 393);
            this.splitContainer1.SplitterDistance = 613;
            this.splitContainer1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(613, 393);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
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
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 地图MToolStripMenuItem
            // 
            this.地图MToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.坐标系ToolStripMenuItem,
            this.大圆轨迹ToolStripMenuItem});
            this.地图MToolStripMenuItem.Name = "地图MToolStripMenuItem";
            this.地图MToolStripMenuItem.Size = new System.Drawing.Size(64, 21);
            this.地图MToolStripMenuItem.Text = "地图(M)";
            // 
            // 坐标系ToolStripMenuItem
            // 
            this.坐标系ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.北京54转兰伯特ToolStripMenuItem,
            this.北京54转墨卡托ToolStripMenuItem,
            this.wGS84转墨卡托投影ToolStripMenuItem});
            this.坐标系ToolStripMenuItem.Name = "坐标系ToolStripMenuItem";
            this.坐标系ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.坐标系ToolStripMenuItem.Text = "投影转换";
            // 
            // 北京54转兰伯特ToolStripMenuItem
            // 
            this.北京54转兰伯特ToolStripMenuItem.Name = "北京54转兰伯特ToolStripMenuItem";
            this.北京54转兰伯特ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.北京54转兰伯特ToolStripMenuItem.Text = "北京54坐标系转兰勃特投影";
            this.北京54转兰伯特ToolStripMenuItem.Click += new System.EventHandler(this.北京54转兰伯特ToolStripMenuItem_Click);
            // 
            // 北京54转墨卡托ToolStripMenuItem
            // 
            this.北京54转墨卡托ToolStripMenuItem.Name = "北京54转墨卡托ToolStripMenuItem";
            this.北京54转墨卡托ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.北京54转墨卡托ToolStripMenuItem.Text = "北京54坐标系转墨卡托投影";
            this.北京54转墨卡托ToolStripMenuItem.Click += new System.EventHandler(this.北京54转墨卡托ToolStripMenuItem_Click);
            // 
            // wGS84转墨卡托投影ToolStripMenuItem
            // 
            this.wGS84转墨卡托投影ToolStripMenuItem.Name = "wGS84转墨卡托投影ToolStripMenuItem";
            this.wGS84转墨卡托投影ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.wGS84转墨卡托投影ToolStripMenuItem.Text = "WGS84坐标系转墨卡托投影";
            this.wGS84转墨卡托投影ToolStripMenuItem.Click += new System.EventHandler(this.wGS84转墨卡托投影ToolStripMenuItem_Click);
            // 
            // 大圆轨迹ToolStripMenuItem
            // 
            this.大圆轨迹ToolStripMenuItem.Name = "大圆轨迹ToolStripMenuItem";
            this.大圆轨迹ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.大圆轨迹ToolStripMenuItem.Text = "大圆轨迹";
            this.大圆轨迹ToolStripMenuItem.Click += new System.EventHandler(this.大圆轨迹ToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.地图MToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(724, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(724, 418);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "地图投影";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 地图MToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 坐标系ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 北京54转兰伯特ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 北京54转墨卡托ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wGS84转墨卡托投影ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 大圆轨迹ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}

