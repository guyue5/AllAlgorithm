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
    public partial class PolyForm : Form
    {
        List<Arc> AllArc;
        PictureBox pictureBox1;
        Bitmap Map;
        double[] border;
        List<Polygon> AllPoly;
        Method AllMethod = new Method();
        ArrayList AllData;

        public PolyForm(List<Polygon> AllPoly,List<Arc> AllArc, PictureBox Pic, double[] border,ArrayList AllData)
        {
            InitializeComponent();
            this.AllArc = new List<Arc>();
            this.AllArc = AllArc;
            this.AllPoly = new List<Polygon>();
            this.AllPoly = AllPoly;
            this.pictureBox1 = Pic;
            this.AllData = AllData;
            Map = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.Map = (Bitmap)this.pictureBox1.Image.Clone();
            this.border = border;
        }

        private void PolyForm_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn[] columns = new DataGridViewTextBoxColumn[4];
            columns[0] = new DataGridViewTextBoxColumn();
            columns[1] = new DataGridViewTextBoxColumn();
            columns[2] = new DataGridViewTextBoxColumn();
            columns[3] = new DataGridViewTextBoxColumn();
            columns[0].HeaderText = "ID号";
            columns[1].HeaderText = "外多边形弧段";
            columns[2].HeaderText = "内多边形弧段";
            columns[3].HeaderText = "多边形总面积";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowDrop = false;
            this.dataGridView1.ReadOnly = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSize = false;
            this.dataGridView1.AllowUserToOrderColumns = false;
            this.dataGridView1.Columns.AddRange(columns);
            this.dataGridView1.Rows.Add(AllArc.Count);
            for (int i = 0; i < AllPoly.Count; i++)
            {
                string S1 = ""; string S2 = "";
                for (int m = 0; m < AllPoly[i].ArcPart.Count;m++ )
                {
                    if (m < AllPoly[i].ArcPart.Count - 1)
                        S1 = S1 + AllPoly[i].ArcPart[m].ToString() + ",";
                    else
                        S1 = S1 + AllPoly[i].ArcPart[m].ToString();
                }
                
                foreach(ArrayList TmpAl in AllPoly[i].ArcPart2)
                {
                    for (int m = 0; m < TmpAl.Count; m++)
                    {
                        if (m < TmpAl.Count - 1)
                            S2 = S2 + TmpAl[m].ToString() + " ";
                        else
                            S2 = S2  + TmpAl[m].ToString()+";";
                    }
                }

                this.dataGridView1[0, i].Value = AllPoly[i].ID;//ID
                this.dataGridView1[1, i].Value = S1;//边界
                this.dataGridView1[2, i].Value = S2;//岛
                this.dataGridView1[3,i].Value=AllPoly[i].Area;
                if(AllPoly[i].ArcPartArea<0)
                    this.dataGridView1[3, i].Value = AllPoly[i].ArcPartArea;
            }
        }

        private void PolyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.pictureBox1.Image = Map;
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.dataGridView1.ClearSelection();
                    this.dataGridView1.Rows[e.RowIndex].Selected = true;
                    int ID = e.RowIndex;
                    this.pictureBox1.Image = (Bitmap)Map.Clone();
                    //画外边界
                    bool flag = true;
                    List<point> DrawData = AllMethod.ReturnPts(AllArc, AllPoly[ID].ArcPart);
                    if(AllPoly[ID].ArcPartArea<0)//面积为负的多边形画法
                    {
                        //填充整个画布
                        Graphics g = Graphics.FromImage(this.pictureBox1.Image);
                        SolidBrush brush;
                        brush = new SolidBrush(Color.Red);
                        Point[] pt = new Point[4] { new Point(0, 0), new Point(this.pictureBox1.Width, 0),
                                                new Point(this.pictureBox1.Width,this.pictureBox1.Height),new Point(0,this.pictureBox1.Height),};
                        g.FillPolygon(brush, pt);
                        g.Dispose();
                        //再挖空
                        DrawPicture(DrawData, false);
                        //把原来补上
                        DrawPicture(AllData);
                        return;
                    }

                    DrawPicture(DrawData, flag);
                    //掏空岛
                    flag = false;
                    foreach(ArrayList TmpAL in AllPoly[ID].ArcPart2)
                    {
                      DrawData.Clear();
                      DrawData = AllMethod.ReturnPts(AllArc, TmpAL);
                      DrawPicture(DrawData, flag);
                    }
                }
            }
        }

        public void DrawPicture(List<point> DrawData,bool Flag)//画图
        {
            Random ran = new Random();
            //this.pictureBox1.Image = (Bitmap)Map.Clone();
            Graphics g = Graphics.FromImage(this.pictureBox1.Image);
            SolidBrush brush;
            if (Flag)
                brush = new SolidBrush(Color.FromArgb(ran.Next(1, 255), ran.Next(1, 255), ran.Next(1, 255)));
            else
                brush = new SolidBrush(this.pictureBox1.BackColor);


            double times = 0d;
            if (border[1] - border[0] < this.pictureBox1.Width | border[3] - border[2] < this.pictureBox1.Height)//数据放大
            {
                double x1 = border[0], x2 = border[1], y1 = border[2], y2 = border[3];
                if (((y2 - y1) / this.pictureBox1.Height) > ((x2 - x1) / this.pictureBox1.Width))
                    times = (float)(y2 - y1) / (this.pictureBox1.Height - 20);
                else
                    times = (float)(x2 - x1) / (this.pictureBox1.Width - 20);
            }
            else//数据缩小
            {
                double x1 = border[0], x2 = border[1], y1 = border[2], y2 = border[3];
                if (((y2 - y1) / this.pictureBox1.Height) > ((x2 - x1) / this.pictureBox1.Width))
                    times = (float)(y2 - y1) / (this.pictureBox1.Height - 20);
                else
                    times = (float)(x2 - x1) / (this.pictureBox1.Width - 20);
            }

            PointF[] pt = new PointF[DrawData.Count];
            int i = 0;

            foreach (point data2 in DrawData)
            {
                pt[i] = ShiftPoint(new PointF((float)data2.X, (float)data2.Y), times);
                i++;
            }
            if (DrawData.Count == 1)
            {
                return;
            }
            g.FillPolygon(brush, pt);

        }

        public PointF world2screen(PointF pt, double times)//转化到屏幕坐标，1代表数据放大，2代表数据缩小
        {
            double x1 = 0d, x2 = 0d, y1 = 0d, y2 = 0d;
            x1 = border[0]; x2 = border[1]; y1 = border[2]; y2 = border[3];
            return new PointF((float)((pt.X - x1) / times), (float)(this.pictureBox1.Height - (pt.Y - y1) / times));
        }

        public PointF ShiftPoint(PointF pt, double times)//将地图平移至图像中心
        {
            PointF CenterPoint = world2screen(new PointF((float)(border[0] / 2.0 + border[1] / 2.0),
                (float)(border[2] / 2.0 + border[3] / 2.0)), times);
            double x1 = 0d, x2 = 0d, y1 = 0d, y2 = 0d;
            x1 = border[0]; x2 = border[1]; y1 = border[2]; y2 = border[3];
            return new PointF((float)((pt.X - x1) / times + pictureBox1.Width / 2.0 - CenterPoint.X),
                   (float)(this.pictureBox1.Height - (pt.Y - y1) / times + this.pictureBox1.Height / 2.0 - CenterPoint.Y));

        }





        public void DrawPicture(ArrayList DrawData)//画图
        {
            Graphics g = Graphics.FromImage(this.pictureBox1.Image);
            Pen p = new Pen(Color.Black, 1);
            double[] border = new double[] { double.MaxValue, double.MinValue, double.MaxValue, double.MinValue };//地图四至,左右下上
            #region//求解地图四至和放大或缩小倍数
            foreach (ArrayList data1 in DrawData)
            {
                foreach (double[] data2 in data1)
                {
                    if (data2[0] < border[0])
                        border[0] = data2[0];
                    else if (data2[0] > border[1])
                        border[1] = data2[0];
                    if (data2[1] < border[2])
                        border[2] = data2[1];
                    else if (data2[1] > border[3])
                        border[3] = data2[1];
                }
            }
            double times = 0d;
            if (border[1] - border[0] < this.pictureBox1.Width | border[3] - border[2] < this.pictureBox1.Height)//数据放大
            {
                double x1 = border[0], x2 = border[1], y1 = border[2], y2 = border[3];
                if (((y2 - y1) / this.pictureBox1.Height) > ((x2 - x1) / this.pictureBox1.Width))
                    times = (float)(y2 - y1) / (this.pictureBox1.Height - 20);
                else
                    times = (float)(x2 - x1) / (this.pictureBox1.Width - 20);
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
                    pt[i] = ShiftPoint(new PointF((float)data2[0], (float)data2[1]), times);
                    i++;
                }
                if (data1.Count == 1)
                {
                    g.DrawEllipse(p, pt[0].X, pt[0].Y, 1.5f, 1.5f);
                    continue;
                }
                g.DrawLines(p, pt);

            }
        }

    }
}
