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
    public partial class TransformCoor : Form
    {
        string filename = "";
        public TransformCoor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void Drawcoor()
        {
            Graphics g = this.pictureBox1.CreateGraphics();
            Transform tf = new Transform();
            coordinates = tf.DrawCoor(this.pictureBox1);
            foreach (PointF[] pf in coordinates)
                g.DrawLines(Pens.Red, pf);
        }

        List<PointF[]> coordinates = new List<PointF[]>();
        private void TransformCoor_Paint(object sender, PaintEventArgs e)
        {
            Drawcoor();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Graphics g = this.pictureBox1.CreateGraphics();
            g.TranslateTransform(this.pictureBox1.Width / 2, this.pictureBox1.Height / 2);
            g.ScaleTransform(1, -1);
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "文本文件|*.txt|所有文件|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filename = ofd.FileName;
                Transform tf=new Transform();
                List<PointF[]> namelist = tf.Drawname(ofd.FileName);
                foreach (PointF[] onename in namelist)
                    g.DrawLines(new Pen(Brushes.Black,5),onename);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Transform tf=new Transform();
            Graphics g = this.pictureBox1.CreateGraphics();
            g.TranslateTransform(this.pictureBox1.Width / 2, this.pictureBox1.Height / 2);
            g.ScaleTransform(1, -1);
            if (radioButton1.Checked)
            {
                this.pictureBox1.Refresh();
                Drawcoor();
                List<PointF[]> oblData = tf.Obligue_Tran(filename);
                foreach (PointF[] pf in oblData)
                    g.DrawLines(new Pen(Brushes.LightSeaGreen, 5), pf);
            }
            if (radioButton2.Checked)
            {
                this.pictureBox1.Refresh();
                Drawcoor();
                List<PointF[]> shrData = tf.Shrug_Tran(filename);
                foreach (PointF[] pf in shrData)
                    g.DrawLines(new Pen(Brushes.LightSeaGreen, 5), pf);
            }
            if (radioButton3.Checked)
            {
                this.pictureBox1.Refresh();
                Drawcoor();
                List<PointF[]> tranData = tf.Translation(filename, double.Parse(textBox1.Text), double.Parse(textBox2.Text));
                foreach (PointF[] pf in tranData)
                    g.DrawLines(new Pen(Brushes.LightSeaGreen, 5), pf);
            }
            if (radioButton4.Checked)
            {
                this.pictureBox1.Refresh();
                Drawcoor();
                List<PointF[]> rovData = tf.Revolve_Tran(filename, double.Parse(textBox3.Text));
                foreach (PointF[] pf in rovData)
                    g.DrawLines(new Pen(Brushes.LightSeaGreen, 5), pf);
            }
            if (radioButton5.Checked)
            {
                this.pictureBox1.Refresh();
                Drawcoor();
                List<PointF[]> proData = tf.Proportion_Tran(filename, double.Parse(textBox4.Text), double.Parse(textBox5.Text));
                foreach (PointF[] pf in proData)
                    g.DrawLines(new Pen(Brushes.LightSeaGreen, 5), pf);
            }

            if (radioButton6.Checked)
            {
                this.pictureBox1.Refresh();
                Drawcoor();
                List<PointF[]> symData = tf.Symmetry_Tran(filename, double.Parse(textBox6.Text), double.Parse(textBox7.Text),
                    double.Parse(textBox8.Text), double.Parse(textBox9.Text));
                foreach (PointF[] pf in symData)
                    g.DrawLines(new Pen(Brushes.LightSeaGreen, 5), pf);
            }
        }

    }
}
