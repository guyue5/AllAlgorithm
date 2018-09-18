using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace CreateTopology
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Border2 = new double[4] { 0, 0, 0, 0 };
        }

        ArrayList data = new ArrayList();
        List<Node> AllNode=new List<Node>();
        List<Arc> AllArc = new List<Arc>();
        ReadGen RG;
        Method AllMethod=new Method();
        List<Polygon> AllPoly = new List<Polygon>();
        double[] Border2;

        private void 打开数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        public void DrawPicture( ArrayList DrawData)//画图
        {
            Bitmap BT = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.pictureBox1.Image = BT;
            Graphics g = Graphics.FromImage(this.pictureBox1.Image);
            Pen p = new Pen(Color.Black, 1);
            double[] border = new double[] { double.MaxValue, double.MinValue, double.MaxValue, double.MinValue };//地图四至,左右下上
            #region//求解地图四至和放大或缩小倍数
            foreach (ArrayList data1 in DrawData)
            {
                foreach (double[] data2 in data1)
                {
                    if (data2[0]< border[0])
                        border[0] = data2[0];
                    else if (data2[0]> border[1])
                        border[1] = data2[0];
                    if (data2[1] < border[2])
                        border[2] = data2[1];
                    else if (data2[1]> border[3])
                        border[3] = data2[1];
                }
            }
            double times=0d;
            if (border[1] - border[0] < this.pictureBox1.Width | border[3] - border[2] < this.pictureBox1.Height)//数据放大
            {
                double x1 = border[0], x2 = border[1], y1 = border[2], y2 = border[3];
                if (((y2 - y1) / this.pictureBox1.Height) > ((x2 - x1) / this.pictureBox1.Width))
                    times = (float)(y2 - y1) / (this.pictureBox1.Height-20);
                else
                    times = (float)(x2 - x1) / (this.pictureBox1.Width-20);
            }
            else//数据缩小
            {
                double x1 = border[0], x2 = border[1], y1 = border[2], y2 = border[3];
                if (((y2 - y1) / this.pictureBox1.Height) > ((x2 - x1) / this.pictureBox1.Width))
                    times = (float)(y2 - y1) / (this.pictureBox1.Height - 20);
                else
                    times = (float)(x2 - x1) / (this.pictureBox1.Width - 20);
            }

            #endregion

            foreach (ArrayList data1 in DrawData)
            {

                PointF[] pt = new PointF[data1.Count];
                int i = 0;

                foreach (double[] data2 in data1)
                {
                    pt[i] = ShiftPoint(border, new PointF((float)data2[0],(float)data2[1]),times);
                    i++;
                }
                if (data1.Count == 1)
                {
                    g.DrawEllipse(p, pt[0].X, pt[0].Y, 1.5f, 1.5f);
                    continue;
                }
                g.DrawLines(p, pt);

            }
            Border2[0] = border[0]; Border2[1] = border[1];
            Border2[2] = border[2]; Border2[3] = border[3];
        }

        public PointF world2screen(double[] border, PointF pt,double times)//转化到屏幕坐标，1代表数据放大，2代表数据缩小
        {
            double x1=0d,x2=0d,y1=0d,y2=0d;
            x1 = border[0]; x2 = border[1]; y1 = border[2]; y2 = border[3];
            return new PointF((float)((pt.X - x1) / times), (float)(this.pictureBox1.Height - (pt.Y - y1) /times));
        }

        public PointF ShiftPoint(double[] border, PointF pt,double times)//将地图平移至图像中心
        {
            PointF CenterPoint = world2screen(border, new PointF((float)(border[0] / 2.0 + border[1] / 2.0), 
                (float)(border[2] / 2.0 + border[3] / 2.0)),times);
            double x1 = 0d, x2 = 0d, y1 = 0d, y2 = 0d;
            x1 = border[0]; x2 = border[1]; y1 = border[2]; y2 = border[3];
            return new PointF((float)((pt.X - x1) / times + pictureBox1.Width / 2.0 - CenterPoint.X),
                   (float)(this.pictureBox1.Height - (pt.Y - y1) / times + this.pictureBox1.Height / 2.0 - CenterPoint.Y));
          
        }

        private void 查看弧段表ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 查看结点表ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 查看多边形表ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                RG = new ReadGen(openFileDialog1.FileName);
                data.Clear();
                AllNode.Clear();
                AllArc.Clear();
                AllPoly.Clear();

                AllMethod.CalNode(AllNode, RG.AllData);//计算Node结点
                DrawPicture(RG.AllData);//画图
                AllMethod.CalArc(RG.AllData, AllNode, AllArc);//计算Arc弧段
                AllMethod.CreatePoly(AllNode, AllArc, AllPoly);//生成多边形
                AllMethod.IsLand(AllPoly, AllArc);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NodeForm NodeWindow = new NodeForm(AllNode, this.pictureBox1, Border2);
            NodeWindow.Show();
            NodeWindow.Focus();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ArcForm ArcWindow = new ArcForm(AllArc, this.pictureBox1, Border2);
            ArcWindow.Show();
            ArcWindow.Focus();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            PolyForm PloyWindow = new PolyForm(AllPoly, AllArc, this.pictureBox1, Border2, RG.AllData);
            PloyWindow.Show();
            PloyWindow.Focus();
        }
    }
}
