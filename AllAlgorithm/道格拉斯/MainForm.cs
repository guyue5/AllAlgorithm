using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Douglas
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.textBox1.Text = Convert.ToString(50);
        }

        ArrayList PtData = new ArrayList();
        public int compress = 50;//压缩比例
        float[] border = new float[] { float.MaxValue, float.MinValue, float.MaxValue, float.MinValue };//地图四至

        private void 打开数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog1.FileName; //获取文件名
            }
            Readdata GenData = new Readdata(file);
            PtData = GenData.AllData;
            
            foreach (ArrayList data1 in PtData)
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

            foreach (ArrayList data1 in DrawData)//求解地图
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
                  //  g.DrawEllipse(p, pt[0].X, pt[0].Y, 1.5f, 1.5f);
                    continue;
                }
                g.DrawLines(p, pt);

            }
        }

        private void button1_Click(object sender, EventArgs e) //进行压缩
        {
            compress = Convert.ToInt32(this.textBox1.Text);
            CompressShapeFile CSF = new CompressShapeFile(PtData, compress);
            DrawPicture(border, CSF.PtData);
         //   double Per = 0d;
         //   long  l1 = 0;
          //  long l2=0;
            //foreach (ArrayList data1 in CSF.PtData)
              //  l2 += data1.Count;
           // foreach (ArrayList data1 in PtData)
               // l1 += data1.Count;
           // Per = l2 * 1.0 / (l1 * 1.0);
           // Per*=100;
          //  MessageBox.Show("实际压缩率为：" + Per + "%");
        }

    }
}
