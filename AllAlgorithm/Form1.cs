using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace gis_no6_compress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.trackBar1.Value = 50;
            this.trackBar1.Visible = false;
            this.label1.Visible = false;
            this.label2.Visible = false;
            this.button1.Visible = false;
        }

        ArrayList PtData = new ArrayList();
        public int compress = 50;//压缩比例
        float[] border = new float[] { float.MaxValue, float.MinValue, float.MaxValue, float.MinValue };//地图四至

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int value = this.trackBar1.Value;
            Point pt = new Point(0, 0);
            pt.X = this.trackBar1.Location.X + this.trackBar1.Width - 2;
            float y = (float)(this.trackBar1.Location.Y + this.trackBar1.Height - 13 - (this.trackBar1.Height - 26) * (value - 10) / 90.0 - 10);
            pt.Y = (int)(y + 0.5);
            this.label1.Location = pt;
            this.label1.Text = value.ToString() + "%";
            compress = value;
        }

        private void 打开数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog1.FileName; //获取文件名
                this.trackBar1.Visible = true;
                this.label1.Visible = true;
                this.label2.Visible = true;
                this.button1.Visible = true;
                this.label1.Text = "50%";
                this.label1.Location = new Point(this.trackBar1.Location.X + this.trackBar1.Width - 2, (int)(this.trackBar1.Location.Y + this.trackBar1.Height - 13 - (this.trackBar1.Height - 26) * (50 - 10) / 90.0 - 10));
            }
            ReadGen GenData = new ReadGen(file);
            PtData = GenData.AllData;
            
            #region
            foreach (ArrayList data1 in PtData)//求解地图四至
            {
                foreach(PointF data2 in data1)
                {
                    PointF pt =data2;
                    if (data2.X < border[0])
                        border[0] = data2.X;
                    else if (data2.X > border[1])
                        border[1] = data2.X;
                    if (data2.Y < border[2])
                        border[2] = data2.Y;
                    else if (data2.Y > border[3])
                        border[3] = data2.Y;
                }
            }
            #endregion
            DrawPicture(border, PtData);
        }

        public PointF world2screen(float[] border, PointF pt)//兰勃特x,y坐标转化到屏幕坐标，经过缩放
        {
            float times = 0;//缩放倍数
            float x1 = border[0], x2 = border[1], y1 = border[2], y2 = border[3];
            if (((y2 - y1) / this.pictureBox1.Width) > ((x2 - x1) / this.pictureBox1.Height))
                times = (y2 - y1) / this.pictureBox1.Width;
            else
                times = (x2 - x1) / this.pictureBox1.Height;
            return new PointF((pt.Y - y1) / times, this.pictureBox1.Height - (pt.X - x1) / times);
        }

        public void DrawPicture(float[]border,ArrayList DrawData)//画图
        {
            Bitmap BT = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.pictureBox1.Image = BT;
            Graphics g = Graphics.FromImage(this.pictureBox1.Image);
            Pen p = new Pen(Color.Black, 1);

            foreach (ArrayList data1 in DrawData)//求解地图四至
            {
                
                PointF [] pt=new PointF [data1.Count];
                int i=0;
                foreach (PointF data2 in data1)
                {
                    pt[i]=world2screen(border,data2);
                    i++;
                }
                if (data1.Count==1)
                {
                    g.DrawEllipse(p, pt[0].X, pt[0].Y, 1.5f, 1.5f);
                    continue;
                }
                g.DrawLines(p, pt);

            }
            //this.pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e) //进行压缩
        {
            CompressShapeFile CSF = new CompressShapeFile(PtData, compress);
            DrawPicture(border, CSF.PtData);
            double Per = 0d;
            long  l1 = 0;
            long l2=0;
            foreach (ArrayList data1 in CSF.PtData)
                l2 += data1.Count;
            foreach (ArrayList data1 in PtData)
                l1 += data1.Count;
            Per = l2 * 1.0 / (l1 * 1.0);
            Per*=100;
            MessageBox.Show("实际压缩比例为：" + Per + "%");
        }

    }
}
