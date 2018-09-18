using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Morton
{
    class QuadTree
    {
        #region 私有变量
        int row;
        int col;
        string[,] quadcode;
        List<string> mortonquad = new List<string>();
        List<string[]> outmortonquad = new List<string[]>();//输出参数
        int[,] data;
        int redeep;//反深度
        int end;
        int round;//第几轮比较
        int count;
        int record;//记录传入的参数值,控制每次调用时的范围
        #endregion

        #region 构造函数
        public QuadTree(int Row, int Col, int[,] Data)
        {
            this.row = Row;
            this.col = Col;
            this.data = Data;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 编码表
        /// </summary>
        /// <returns>Morton码数组</returns>
        public string[,] QuadCode()
        {
            quadcode = new string[row, col];
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    int temp = 2 * int.Parse(Convert.ToString(i, 2)) + int.Parse(Convert.ToString(j, 2));//四进制编码，长度不一致
                    string temp1 = temp.ToString();
                    int total = Convert.ToString(row, 2).Length - 1;//规定总长度
                    if (temp1.Length < total)
                    {
                        for (int k = temp1.Length; k < total; k++)
                        {
                            temp1 = "0" + temp1;//高位补0
                        }
                    }
                    quadcode[i, j] = temp1;
                    mortonquad.Add(temp1);
                }
            return quadcode;
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <returns>输出list，即编码值</returns>
        public List<string[]> Ergodic()
        {
            int i = 0;
            while (i < mortonquad.Count)
            {
                round = 0; redeep = 0; end = 0; count = 0;
                record = i;
                MortonQuad(i);
                i = end + 1;
            }
            return outmortonquad;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// morton码生成，从底向上
        /// </summary>
        /// <param name="index">每一次的检索号</param>
        private void MortonQuad(int index)
        {
            round++;
            //M码的升序排列
            mortonquad.Sort();
            for (int j = index; j < record + Math.Pow(4, round) - 1; j++)//每次检索的开始值与检索范围
            {
                int a = data[RowCol(mortonquad[j])[0], RowCol(mortonquad[j])[1]];//调用反解函数，得出对应属性值
                int b = data[RowCol(mortonquad[j + 1])[0], RowCol(mortonquad[j + 1])[1]];
                if (a == b)
                    count++;
                end = j + 1;//记录最后一个值
            }
            if (count == Math.Pow(4, round) - round)//判断相同值的个数是否和理论值相同
            {
                redeep++;
                if (end < mortonquad.Count - round)
                {
                    //判断块值是否和下面的第一个值相等
                    int a1 = data[RowCol(mortonquad[end])[0], RowCol(mortonquad[end])[1]];
                    int b1 = data[RowCol(mortonquad[end + 1])[0], RowCol(mortonquad[end + 1])[1]];
                    if (a1 == b1)
                        MortonQuad(end + 1);//继续合并
                    else//不等直接输出
                    {
                        string[] tempout = new string[3];
                        tempout[0] = mortonquad[end].Substring(0, mortonquad[0].Length - redeep);//morton码
                        tempout[1] = tempout[0].Length.ToString();//深度
                        tempout[2] = data[RowCol(mortonquad[end])[0], RowCol(mortonquad[end])[1]].ToString();//属行值
                        outmortonquad.Add(tempout);
                    }
                }
                else//如果已经合并到最后，则直接输出
                {
                    string[] tempout = new string[3];
                    tempout[0] = mortonquad[end].Substring(0, mortonquad[0].Length - redeep);//morton码
                    tempout[1] = tempout[0].Length.ToString();//深度
                    tempout[2] = data[RowCol(mortonquad[end])[0], RowCol(mortonquad[end])[1]].ToString();//属行值
                    outmortonquad.Add(tempout);
                }
            }
            else//如果该块区域内值不相同，则直接输出
            {
                if ((index + 4) < mortonquad.Count)
                {
                    for (int k = index; k < index + 4; k++)
                    {
                        string[] tempout = new string[3];
                        tempout[0] = mortonquad[k].Substring(0, mortonquad[0].Length - redeep);
                        tempout[1] = tempout[0].Length.ToString();
                        tempout[2] = data[RowCol(mortonquad[k])[0], RowCol(mortonquad[k])[1]].ToString();
                        outmortonquad.Add(tempout);
                    }
                }
            }
        }

        /// <summary>
        /// 由Morton码反求二进制行列值算法
        /// </summary>
        /// <param name="morcode">四进制编码值</param>
        /// <returns>存储了行列值的数组</returns>
        private int[] RowCol(string morcode)
        {
            int[] rowcol = new int[2];
            string temprow = "";
            string tempcol = "";
            for (int i = 0; i < morcode.Length; i++)
            {
                //注意加法顺序!!!后添加的值加在后面
                if (morcode.Substring(i, 1) == "0" || morcode.Substring(i, 1) == "1")
                    temprow = temprow + "0";
                if (morcode.Substring(i, 1) == "2" || morcode.Substring(i, 1) == "3")
                    temprow = temprow + "1";
                if (morcode.Substring(i, 1) == "0" || morcode.Substring(i, 1) == "2")
                    tempcol = tempcol + "0";
                if (morcode.Substring(i, 1) == "1" || morcode.Substring(i, 1) == "3")
                    tempcol = tempcol + "1";
            }
            //转十进制，位权展开
            for (int i = 0; i < temprow.Length; i++)
            {
                rowcol[0] = rowcol[0] + (int)(Math.Pow(2, temprow.Length - i - 1) * int.Parse(temprow.Substring(i, 1)));
            }
            for (int j = 0; j < tempcol.Length; j++)
            {
                rowcol[1] = rowcol[1] + (int)(Math.Pow(2, tempcol.Length - j - 1) * int.Parse(tempcol.Substring(j, 1)));
            }
            return rowcol;
        }
        #endregion
    }
}

    



