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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void 跳马ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HorseVault hv = new HorseVault();
            hv.Show();
        }

        private void 最佳工作序列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            最佳工作序列.Sequence se = new 最佳工作序列.Sequence();
            se.Show();
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            数据编码.DisplayName dn = new 数据编码.DisplayName();
            dn.Show();
        }

        private void 坐标变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            数据编码.TransformCoor tc = new 数据编码.TransformCoor();
            tc.Show();
        }

        private void 多边形面积ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalculatePolygon.MainForm mf = new CalculatePolygon.MainForm();
            mf.Show();
        }

        private void 四叉树ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Morton.Morton mor = new Morton.Morton();
            mor.Show();
        }

        private void 地图投影ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapProjection.Form1 form = new MapProjection.Form1();
            form.Show();
        }

        private void 左转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTopology.MainForm mf = new CreateTopology.MainForm();
            mf.Show();
        }

        private void 道格拉斯ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Douglas.MainForm mf = new Douglas.MainForm();
            mf.Show();
        }
    }
}
