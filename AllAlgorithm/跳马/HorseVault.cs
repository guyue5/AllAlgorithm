using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllAlgorithm
{
    public partial class HorseVault : Form
    {
        public HorseVault()
        {
            InitializeComponent();
        }

        List<List<int[]>> route = new List<List<int[]>>();

        private void HorseVault_Load(object sender, EventArgs e)
        {
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
            跳马.HorseVal hv = new 跳马.HorseVal(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
            hv.HorseJump(int.Parse(textBox3.Text), int.Parse(textBox4.Text), 0);
            List<List<int[]>> routelist = hv.routelist.Distinct().ToList();
            this.textBox5.Text = routelist.Count.ToString();
            foreach (List<int[]> route in routelist)
            {
                foreach(int[] rowcol in route)
                {
                    this.richTextBox1.AppendText("("+rowcol[0]+","+rowcol[1]+")"+"->");
                }
                this.richTextBox1.AppendText("over");
                this.richTextBox1.AppendText("\r\n");
            }
        }

    }
}
