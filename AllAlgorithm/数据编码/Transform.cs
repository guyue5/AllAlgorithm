using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace AllAlgorithm.数据编码
{
    class Transform
    {
        public List<PointF[]> Drawname(string filename)//读取并画名字
        {
            List<PointF[]> datalist = new List<PointF[]>();
            int index = 1;
            int num = 0;
            int count = 0;//总行数
            string[] lines = File.ReadAllLines(filename);
            while (count < lines.Length)
            {
                List<PointF> code = new List<PointF>();
                if (lines[count] == "")
                {
                    ++num;
                    ++count;
                    index = 1;
                }
                if (lines[count] == index.ToString())
                {
                    count++;
                    while (lines[count] != "END")
                    {
                        string[] pt = lines[count].Split(',');
                        code.Add(new PointF(float.Parse(pt[0]) * 5 + num * 80, float.Parse(pt[1]) * 5));
                        ++count;
                    }
                    datalist.Add(code.ToArray());
                    ++index;
                }
                else
                {
                    count++;
                    continue;
                }
            }
            return datalist;
        }

        public List<PointF[]> DrawCoor(PictureBox pb)//画坐标系
        {
            List<PointF[]> coordinates = new List<PointF[]>();
            PointF[] horzontal=new PointF[2];
            horzontal[0]=new PointF(0,pb.Height/2);
            horzontal[1]=new PointF(pb.Width,pb.Height/2);
            PointF[] vertical=new PointF[2];
            vertical[0]=new PointF(pb.Width/2,0);
            vertical[1]=new PointF(pb.Width/2,pb.Height);
            coordinates.Add(horzontal);
            coordinates.Add(vertical);
            return coordinates;
        }
    
             public List<PointF[]> Translation(string filename,double tx,double ty)//平移变换
             {
                 List<PointF[]> tranData=Drawname(filename);
                 foreach(PointF[] pf in tranData)
                 {
                     for (int i = 0; i < pf.Length; i++)
                     {
                         pf[i].X = (float)(pf[i].X + tx);
                         pf[i].Y = (float)(pf[i].Y + ty);
                     }
                 }
                 return tranData;
             }

             public List<PointF[]> Symmetry_Tran(string filename, double a, double b, double d, double e)//对称变换
             {
                 List<PointF[]> symData = Drawname(filename);
                 foreach (PointF[] pf in symData)
                 {
                     for (int i = 0; i < pf.Length; i++)
                     {
                         PointF temp = new PointF();
                         temp = pf[i];
                         pf[i].X = (float)(temp.X * a + b * temp.Y);
                         pf[i].Y = (float)(temp.X * d + e * temp.Y);
                     }
                 }
                 return symData;
             }

             public List<PointF[]> Revolve_Tran(string filename, double theta)//旋转变换
             {
                 List<PointF[]> revData = Drawname(filename);
                 foreach (PointF[] pf in revData)
                 {
                     for (int i = 0; i < pf.Length; i++)
                     {
                         PointF temp = new PointF();
                         temp = pf[i];
                         pf[i].X = (float)(temp.X * Math.Cos(theta * Math.PI / 180) - Math.Sin(theta * Math.PI / 180) * temp.Y);
                         pf[i].Y = (float)(temp.X * Math.Sin(theta * Math.PI / 180) + Math.Cos(theta * Math.PI / 180) * temp.Y);
                     }
                 }
                 return revData;
             }

             public List<PointF[]> Proportion_Tran(string filename, double sx, double sy)//比例变换
             {
                 List<PointF[]> proData = Drawname(filename);
                 foreach (PointF[] pf in proData)
                 {
                     for (int i = 0; i < pf.Length; i++)
                     {
                         pf[i].X = (float)(pf[i].X*sx);
                         pf[i].Y = (float)(pf[i].Y*sy);
                     }
                 }
                 return proData;
             }

             public List<PointF[]> Shrug_Tran(string filename)//耸肩变换
             {
                 List<PointF[]> shrData = Drawname(filename);
                 foreach (PointF[] pf in shrData)
                 {
                     for (int i = 0; i < pf.Length; i++)
                     {
                         PointF temp = new PointF();
                         temp = pf[i];
                         pf[i].Y = temp.Y + (float)(temp.X * Math.Tan(15 * Math.PI / 180));
                     }
                 }
                 return shrData;
             }

             public List<PointF[]> Obligue_Tran(string filename)//左斜变换
             {
                 List<PointF[]> oblData = Drawname(filename);
                 foreach (PointF[] pf in oblData)
                 {
                     for (int i = 0; i < pf.Length; i++)
                     {
                         PointF temp = new PointF();
                         temp = pf[i];
                         pf[i].X = temp.X - (float)(temp.Y * Math.Tan(15 * Math.PI / 180));
                     }
                 }
                 return oblData;
             }
    }
}
