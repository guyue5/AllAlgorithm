using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapProjection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<PointF[]> coordinate_st = new List<PointF[]>();//存放原始坐标
       // private List<PointF[]> graticule_st = new List<PointF[]>();//存放原始经纬网
        private string filename = "";
        private static int Flag;
        public static int flag { get { return Flag; } set { Flag = value; } }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.White, this.ClientRectangle);//将当前界面设置为白色
            this.pictureBox1.BackColor = Color.White;
        }

        private void 大圆轨迹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            Draw draw = new Draw(coordinate_st, pictureBox1.DisplayRectangle);
            Bitmap map = draw.DrawGraticule();
            Bitmap nmap = draw.DrawPro(map);
            this.pictureBox1.Image = draw.DrawOrbit(nmap);
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = 1;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Generate(*.gen)|*.gen";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filename = ofd.FileName;
                DataRead dr = new DataRead(filename);
                coordinate_st = dr.code;
                Draw draw = new Draw(coordinate_st, pictureBox1.DisplayRectangle);
                Bitmap map = draw.DrawGraticule();
                this.pictureBox1.Image = draw.DrawPro(map);
            }
            ofd.Dispose();
        }

    
 
        private void 北京54转兰伯特ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = 2;
            pictureBox1.Refresh();
            Draw draw = new Draw(coordinate_st, pictureBox1.DisplayRectangle);
            Bitmap map = draw.DrawGraticule();
            this.pictureBox1.Image = draw.DrawPro(map);
        }

        private void wGS84转墨卡托投影ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = 4;
            pictureBox1.Refresh();
            Draw draw = new Draw(coordinate_st, pictureBox1.DisplayRectangle);
            Bitmap map = draw.DrawGraticule();
            this.pictureBox1.Image = draw.DrawPro(map);
        }

        private void 北京54转墨卡托ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = 3;
            pictureBox1.Refresh();
            Draw draw = new Draw(coordinate_st, pictureBox1.DisplayRectangle);
            Bitmap map = draw.DrawGraticule();
            this.pictureBox1.Image = draw.DrawPro(map);
        }


    }
}
