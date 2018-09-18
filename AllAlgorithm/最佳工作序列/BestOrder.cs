using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AllAlgorithm.最佳工作序列
{
    class BestOrder
    {
        public static int[,] data;
        public static int[] task;

        public void Readfile(string filename)
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                string s = sr.ReadLine();
                data = new int[4, 4];

                for (int i = 0; i < 4; i++)
                {
                    //一行一行读取
                    string line = sr.ReadLine();
                    string[] p = line.Split(',');
                    for (int j = 0; j < 4; j++)
                    {
                        data[i, j] = int.Parse(p[j]);
                    }
                }
                task = new int[4];
                for (int i = 0; i < 4; i++)
                    task[i] = data[i, 0];
            }
        }

        public static int max;
        public static int[] print = new int[4];
        public static int[] order = new int[4];

        /// <summary>
        /// 排列组合
        /// </summary>
        /// <param name="list">需要排列的数组</param>
        /// <param name="start">数组中的起始编号</param>
        /// <param name="end">数组中的结尾编号</param>
        public static void Rank(int[] list, int start, int end)
        {
            int j;
            int temp = 0;
            if (start == end)
            {
                int i = 0;
                int date = 0;
                for (int k = 0; k <= end; k++)
                {
                    if ((date = date + data[list[k] - 1, 1]) <= data[list[k] - 1, 2])
                    {
                        temp = temp + data[list[k] - 1, 3];
                        order[i] = list[k];
                        ++i;
                    }

                }

                if (temp > max)
                {
                    max = temp;
                    for (int m = 0; m < 4; m++)
                        print[m] = order[m];
                }
            }
            else
            {
                for (j = start; j <= end; j++)
                {
                    Swap(ref list[start], ref list[j]);
                    Rank(list, start + 1, end);
                    Swap(ref list[start], ref list[j]);//数组一定要复原
                }
            }
        }

        /// <summary>
        /// 交换
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        private static void Swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }
    }
}
