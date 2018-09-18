using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace AllAlgorithm.数据编码
{
    class ViewName
    {

        private List<PointF[]> code = new List<PointF[]>();//存坐标
        private List<PointF[]> grid = new List<PointF[]>();//存网格
        int rowInterval;
        int colInterval;
 
        public List<PointF[]> DrawGrid(PictureBox pb)
        {
            int col = 16; // 网格的列数
            int row = 16; // 网格的行数
            int drawRow = 0;
            int drawCol = 0;
            rowInterval = pb.Height / row;
            colInterval = pb.Width / col;
            // 画水平线
            for (int i = 0; i <= row; i++)
            {
                PointF[] temp = new PointF[2];
                temp[0] = new PointF(0, drawCol);
                temp[1] = new PointF(pb.Width, drawCol);
                grid.Add(temp);
                drawCol += rowInterval;
            }
            // 画竖直线
            for (int j = 0; j <= col; j++)
            {
                PointF[] temp = new PointF[2];
                temp[0] = new PointF(drawRow, 0);
                temp[1] = new PointF(drawRow, pb.Height);
                grid.Add(temp);
                drawRow += colInterval;
            }
            return grid;
        }

        //读取txt，存放在line中
        public List<PointF[]> DrawName(string filename)
        {
            string[] lines = File.ReadAllLines(filename);//读取txt，存放在lines中 
            int count = 0;//总行数，控制循环
            while (count < lines.Length - 1)
            {
                List<PointF> temp = new List<PointF>();
                while (lines[count].ToUpper() != "END")
                {
                    if (lines[count].Contains(","))
                    {
                        string[] pt = lines[count].Split(',');
                        temp.Add(new PointF(float.Parse(pt[0])*colInterval, float.Parse(pt[1])*rowInterval));
                    }
                    count++;
                }
                code.Add(temp.ToArray());
                count++;
            }
            return code;
        }
    }
}
