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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public static  string filename { get; set; }
        private int flag;//定义窗口是否变化
        public static int calflag { get; set; }  //判断执行计算哪种类型面积的操作
        Bitmap shp;
       // public ArrayList polylist = new ArrayList();//原始多边形集合
       // public ArrayList merpolylist = new ArrayList();//墨卡托下的多边形集合

        public List<List<List<PointF>>> polylist = new List<List<List<PointF>>>();//原始多边形集合
        public List<List<List<PointF>>> merpolylist = new List<List<List<PointF>>>();//原始多边形集合
        private void shapefile文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            flag = 1;
            OpenFileDialog ofd = new OpenFileDialog();//打开文件夹
            ofd.Filter="shapefile(*.shp)|*.shp|所有文件(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filename = ofd.FileName;
                ShapeFile.ReadShp rs = new ShapeFile.ReadShp();
                rs.ReadShape(filename);
                Bitmap shp = rs.DrawShp(pictureBox1.DisplayRectangle);
                this.pictureBox1.Image = shp;
                polylist = rs.polylist;//有值
                merpolylist = rs.merpolylist;//无值
            }
            ofd.Dispose();   
        }

        //更改picturebox的大小，使其与主窗口变化一致
        private void MainForm_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            this.pictureBox1.ClientSize = control.Size;
            if (flag == 1)
            {
                pictureBox1.Invalidate();
                ShapeFile.ReadShp rs = new ShapeFile.ReadShp();
                rs.ReadShape(filename);
                //Bitmap shp = rs.DrawShp(pictureBox1.DisplayRectangle);
                shp = rs.DrawShp(pictureBox1.DisplayRectangle);
                this.pictureBox1.Image = shp;
            }
        }


        private void 多边形面积ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calflag = 1;
          //  Area polygon = new Area();
            Area polygon = new Area(this.pictureBox1, shp, polylist);
            polygon.Show();
        }

        private void 墨卡托投影反算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            ShapeFile.ReadShp rs = new ShapeFile.ReadShp();
            rs.ReadShape(filename);
            Bitmap mi = rs.MercatorInverse(pictureBox1.DisplayRectangle);
            merpolylist = rs.merpolylist;
            this.pictureBox1.Image = mi;
        }

        private void 计算球面上梯形面积ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calflag = 2;
            //Area polygon = new Area();
            Area polygon = new Area(this.pictureBox1, shp, merpolylist);
            polygon.Show();
        }

    }
}
