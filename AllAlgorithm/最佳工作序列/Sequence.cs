using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AllAlgorithm.最佳工作序列
{
    public partial class Sequence : Form
    {
        public Sequence()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.richTextBox1.Text = File.ReadAllText(ofd.FileName, System.Text.Encoding.Default);
                最佳工作序列.BestOrder bo = new BestOrder();
                bo.Readfile(ofd.FileName);
                最佳工作序列.BestOrder.Rank(最佳工作序列.BestOrder.task, 0, 3);
                for (int i = 0; i < 4; i++)
                {
                    this.textBox1.AppendText(最佳工作序列.BestOrder.print[i].ToString());
                }
                this.textBox2.Text = 最佳工作序列.BestOrder.max.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
