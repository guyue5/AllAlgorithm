using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace CalculatePolygon
{
    public partial class Area : Form
    {
        PictureBox picturebox = new PictureBox();
        Bitmap map;
        public List<List<List<PointF>>> viewpoly = new List<List<List<PointF>>>();//显示多边形集合
        public Area(PictureBox Pb, Bitmap Map, List<List<List<PointF>>> Viewpoly)
        {
            InitializeComponent();
            this.picturebox = Pb;
            this.map = new Bitmap(Pb.Width, Pb.Height);
            this.map = (Bitmap)Pb.Image.Clone();
            this.viewpoly = Viewpoly;//显示多边形
        }

        private void Area_Load(object sender, EventArgs e)
        {
            List<double> area = new List<double>();
            ShapeFile.ReadShp rs = new ShapeFile.ReadShp();
            rs.ReadShape(MainForm.filename);
            if (MainForm.calflag == 1)
            {
                area = rs.CalArea();
            }
            else if (MainForm.calflag == 2)
            {
                area = rs.CalTrapezoid();
            }
            #region 设置datagridview的相关属性
            dataGridView1.RowCount = area.Count;//行数
            dataGridView1.ColumnCount = 2;//列数
            dataGridView1.Columns[0].Width = 100;//第一列宽
            dataGridView1.Columns[1].Width = 400;//第二列宽
            dataGridView1.Columns[0].Name = "序号";
            dataGridView1.Columns[1].Name = "面积";
            #endregion
            for (int i = 0; i < area.Count; i++)
            {
                this.dataGridView1[0, i].Value = (i+1).ToString();
                this.dataGridView1[1, i].Value = area[i];
            } 
        }

        private void dataGridView1_CellMousedown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.dataGridView1.ClearSelection();
                    this.dataGridView1.Rows[e.RowIndex].Selected = true;
                    int ID = Convert.ToInt32(this.dataGridView1[0, e.RowIndex].Value);
                    picturebox.Image = (Bitmap)map.Clone();
                    foreach (List<PointF> points in viewpoly[ID - 1])
                    {
                        DrawShp(points);
                    }
                }
            }
        }

        public void DrawShp(List<PointF> points)
        {
            //Bitmap shp = new Bitmap(picturebox.Width, picturebox.Height);
         //   Graphics g = Graphics.FromImage(shp);
            Graphics g = Graphics.FromImage(picturebox.Image);
            Pen p = new Pen(Color.Red, 3);
            g.TranslateTransform(0, picturebox.Height);
            g.ScaleTransform(1, -1);
           // g.DrawLines(p, points.ToArray());
            g.FillPolygon(Brushes.Orange, points.ToArray());
       //     return shp;
        }
    }
}
