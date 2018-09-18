using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllAlgorithm.跳马
{
    class HorseVal
    {
        public List<List<int[]>> routelist = new List<List<int[]>>();
        int ROWs;
        int COLs;
        int[,] flag;
        int[] row;
        int[] col;
        public HorseVal(int rows,int cols)//输入的textbox
        {
            this.ROWs = rows;
            this.COLs = cols;
            flag = new int[rows, cols];
            row = new int[rows * cols];
            col = new int[rows * cols];
            for (int i = 0; i < rows * cols; i++)
            {
                row[i] = col[i] = -1;
            }
        }
        int[,] Move = new int[,] { { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 }, { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 } };

        /// <summary>
        /// 若某条线路满足，则将数组中存储的满足要求的坐标输出
        /// </summary>

        private List<int[]> Print()
        {
            List<int[]> route = new List<int[]>();
            for (int i = 0; i < ROWs * COLs; i++)
            {
                int[] temp = new int[2];
                temp[0] = row[i]; temp[1] = col[i];
                if (temp[0] != -1 & temp[1] != -1)
                    route.Add(temp);
            }
            int[] temp1 = new int[2];
            temp1[0] = row[0]; temp1[1] = col[0];
            route.Add(temp1);
            return route;
        }

        /// <summary>
        /// 跳马函数，递归回溯
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="step"></param>
        /// <returns>该点是否为可走的下一个点</returns>
        public int HorseJump(int x, int y, int step)
        {
            int i, j;
            flag[x, y] = 1;
            row[step] = x;
            col[step] = y;

            //若所记录的步数已到达25步且满足最后一点可返回原点，则输出结果
            /*
            if (step >= ROWs * COLs - 1)
                for (int ii = 0; ii < 8; ii++)
                    if (row[step] + Move[ii, 0] == row[0] && col[step] + Move[ii, 1] == col[0])
                    {
                        List<int[]> route = new List<int[]>();
                        route = Print();
                        routelist.Add(route);
                    }
             * */

           // if (step >= ROWs * COLs - 1)
           // {
                bool panduan = false;
                for (int ii = 0; ii < 8; ii++)
                    if (row[step] + Move[ii, 0] == row[0] && col[step] + Move[ii, 1] == col[0])
                    {
                        panduan = true;
                        break;
                    }
                if (panduan)
                {
                    List<int[]> route = new List<int[]>();
                    route = Print();
                    routelist.Add(route);
                }
          //  }
            for (int jj = 0; jj < 8; jj++)
            {
                if ((i = x + Move[jj, 0]) >= 0 && (i = x + Move[jj, 0]) < ROWs && (j = y + Move[jj, 1]) >= 0 && (j = y + Move[jj, 1]) < COLs && flag[i, j] != 1)
                    if (HorseJump(i, j, step + 1) == 1)
                        return 1;
            }

            //清除原先标记，回溯，并返回0
            flag[x, y] = 0;
            row[step] = -1;
            col[step] = -1;
            return 0;
        }
    }
}
