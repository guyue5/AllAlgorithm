using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllAlgorithm.数据编码
{
    public partial class DisplayName : Form
    {
        public DisplayName()
        {
            InitializeComponent();
        }

        private void DisplayName_Load(object sender, EventArgs e)
        {
            this.comboBox1.Items.Add("伍");
            this.comboBox1.Items.Add("丝");
            this.comboBox1.Items.Add("琪");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.TranslateTransform(0, this.pictureBox1.Height);
            g.ScaleTransform(1, -1);
            ViewName vn = new ViewName();
            List<PointF[]> grid = vn.DrawGrid(this.pictureBox1);
            Pen p = new Pen(Brushes.Red);//实例化画笔
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;//设置线型(虚线)
            List<PointF[]> code = new List<PointF[]>();
            if (comboBox1.SelectedIndex == 0)
            {
                this.pictureBox1.Refresh();
                code.Clear();
                code = vn.DrawName(@"..\wu.txt");
                foreach (PointF[] pf in grid)
                {
                    g.DrawLines(p, pf);
                }
                foreach (PointF[] p0 in code)
                {
                    g.DrawLines(new Pen(Color.Gray, 10), p0);
                }
            }

            else if (comboBox1.SelectedIndex == 1)
            {
                code.Clear();
                code = vn.DrawName(@"..\si.txt");
                this.pictureBox1.Refresh();
                foreach (PointF[] pf in grid)
                {
                    g.DrawLines(p, pf);
                }
                foreach (PointF[] p1 in code)
                {
                    g.DrawLines(new Pen(Color.Gray, 10), p1);
                }
            }
            else
            {
                code.Clear();
                code = vn.DrawName(@"..\qi.txt");
                this.pictureBox1.Refresh();
                foreach (PointF[] pf in grid)
                {
                    g.DrawLines(p, pf);
                }
                foreach (PointF[] p2 in code)
                {
                    g.DrawLines(new Pen(Color.Gray, 10), p2);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
