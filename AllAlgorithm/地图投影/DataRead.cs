using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MapProjection
{
    class DataRead
    {
        public   List<PointF[]> code = new List<PointF[]>();//将所有要画的线全部存储在了里面
        private  int index;

        public DataRead(string filename)
        {
            string[] lines = File.ReadAllLines(filename);//读取txt，存放在lines中 
            index = 1;//线号
            int count = 0;//总行数，控制循环
            while (count < lines.Length)
            {
                if (lines[count] == index.ToString())
                {
                    int linenum = 0;
                    PointF[] temp = new PointF[lines.Length];//临时存放
                    count++;
                    while (lines[count].ToUpper() != "END")//判断某一条线
                    {
                        string[] pt = lines[count].Split(',');
                        temp[linenum] = new PointF(float.Parse(pt[0]), float.Parse(pt[1]));
                        ++count;
                        ++linenum;//代表一条线中的点，每次新的线时更新为0
                    }
                    PointF[] temp1 = new PointF[linenum];
                    //此处的存储注意不能删！！！
                    for (int ii = 0; ii < linenum; ii++)
                        temp1[ii] = temp[ii];
                    ++index;   
                    code.Add(temp1);          
                }
                else
                {
                    count++;
                    continue;
                }
            }  
        }
    }
}
