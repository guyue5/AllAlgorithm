using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Morton
{
    public partial class Morton : Form
    {
        public Morton()
        {
            InitializeComponent();
        }

        string filename = "";
        string[,] quadcode = null;//存放四进制编码
        List<string[]> outmortonquad = new List<string[]>();////输出编码
       
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            ofd.InitialDirectory="\\..";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filename = ofd.FileName;
                ReadData rd = new ReadData(ofd.FileName);
                int[,] matrix=new int[rd.row,rd.col];
                matrix =rd.DataRead();
                dataGridView1.RowCount = rd.row;
                dataGridView1.ColumnCount = rd.col;
                //自动调整列宽和行宽
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
               // dataGridView1.Rows[0].HeaderCell.Value = "行";
               // dataGridView1.Columns[0].HeaderCell.Value = "列";
                dataGridView1.TopLeftHeaderCell.Value = "行\\列";
                dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;//自动调整行头
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                for (int i = 0; i < rd.row; i++)
                {
                    for (int j = 0; j < rd.col; j++)
                    {
                        dataGridView1[j, i].Value = matrix[i, j];
                    }
                }
            }
            ofd.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Update();
            ReadData rd = new ReadData(filename);
            int[,] matrix=rd.DataRead();
            QuadTree qt = new QuadTree(rd.row, rd.col, matrix);
            quadcode = qt.QuadCode();
            for (int i = 0; i < rd.row; i++)
            {
                for (int j = 0; j < rd.col; j++)
                {
                    dataGridView1[j, i].Value = quadcode [i, j];
                }
            }
            for (int i = 0; i < rd.row; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = quadcode[0, i];
                dataGridView1.Columns[i].HeaderCell.Value = quadcode[0, i];
            }
            outmortonquad = qt.Ergodic();
            dataGridView2.RowCount = outmortonquad.Count;
            dataGridView2.ColumnCount = 3;
            for (int i = 0; i < outmortonquad.Count; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    dataGridView2[j, i].Value = outmortonquad[i][j];
                }
            }
            dataGridView2.Columns[0].HeaderCell.Value = "M码";
            dataGridView2.Columns[1].HeaderCell.Value = "深度";
            dataGridView2.Columns[2].HeaderCell.Value = "属性值";
        }

    }
}
