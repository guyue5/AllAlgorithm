using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Morton
{
    class ReadData
    {
        string filename;
        int[,] matrix = null;//存储矩阵

        public int row { get; set; }//行数
        public int col { get; set; }//列数

        public ReadData(string Filename)
        {
            this.filename = Filename;
        }

        /// <summary>
        /// 读取Quadtree文件
        /// </summary>
        /// <returns>返回读取的矩阵</returns>
        public int[,] DataRead()
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                string inf = sr.ReadLine();
                string[] info = inf.Split(',');
                row = int.Parse(info[0]);
                col = int.Parse(info[1]);
                matrix = new int[row, col];
                for (int i = 0; i < row; i++)
                {
                    string colinf=sr.ReadLine();
                    string[] colinfo = colinf.Split(',');
                    for (int j = 0; j < col; j++)
                    {
                        matrix[i, j] = int.Parse(colinfo[j]);
                    }
                }
            }
            return matrix;
        }
    }
}
