using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace gis_no6_compress
{
    class ReadGen //Gen读取类
    {
        public ArrayList AllData = new ArrayList();//所有数据
        public int num = 0;
        public ReadGen(string filename)
        {
            try
            {
                //读取txt中的数据
                using (StreamReader sr = new StreamReader(filename))
                {
                    string data = "";
                    do
                    {
                        data = sr.ReadLine();
                        if ((data == "") | (data == "END"))
                            break;
                        string line = sr.ReadLine();//读取数据点
                        ArrayList Line = new ArrayList();//一个对象数据

                        while ((line != null) & (line.ToUpper() != "END"))//读取一个对象
                        {
                            num++;
                            string[] tmp = line.Split(new char[] { ',' });
                            //利用Split函数将字符串按照逗号分开为x,y坐标
                            PointF now_data = new PointF(0, 0);
                            now_data.X = (float)Convert.ToDouble(tmp[0]);
                            now_data.Y = (float)Convert.ToDouble(tmp[1]);
                            
                            Line.Add(lanbote(now_data.Y,now_data.X));
                            line = sr.ReadLine();
                        }
                        AllData.Add(Line);
                    } while ((data != null) & (data.ToUpper() != "END"));

                }
            }
            catch (Exception ex)
            {    //异常处理
                MessageBox.Show(ex.Message);
                return;
            }
        }

        public PointF lanbote(float B, float L ) //北京54坐标系下的兰勃特坐标正算(B,L)->(X,Y)
        {
            PointF pt = new PointF(0, 0);
            B = (float)(B / 180.0 * Math.PI);
            L = (float)(L / 180.0 * Math.PI);
            float X=0f;
            float Y=0f;
            double B0 = 0d;//原点纬度
            double L0 = 105 / 180.0 * Math.PI;//中央经线
            double B1 = 20 / 180.0 * Math.PI;//第一标准纬线
            double B2 = 40 / 180.0 * Math.PI;//第二标准纬线
            double a = 6378245f;//长半轴
            double b = 6356863.01877f;//短半轴
            double e = Math.Sqrt((a * a - b * b) / (a * a));

            double m_B = Math.Cos(B) / Math.Sqrt(1 - e * e * Math.Sin(B) * Math.Sin(B));
            double t_B = Math.Tan(Math.PI / 4.0 - B / 2.0) / Math.Pow((1 - e * Math.Sin(B)) / (1 + e * Math.Sin(B)), e / 2.0);
            double t_B0 = Math.Tan(Math.PI / 4.0 - B0 / 2.0) / Math.Pow((1 - e * Math.Sin(B0)) / (1 + e * Math.Sin(B0)), e / 2.0);
            double m_B1 = Math.Cos(B1) / Math.Sqrt(1 - e * e * Math.Sin(B1) * Math.Sin(B1));
            double m_B2 = Math.Cos(B2) / Math.Sqrt(1 - e * e * Math.Sin(B2) * Math.Sin(B2));
            double t_B1 = (Math.Tan(Math.PI / 4.0 - B1 / 2.0) / Math.Pow((1 - e * Math.Sin(B1)) / (1 + e * Math.Sin(B1)), e / 2.0));
            double t_B2 = (Math.Tan(Math.PI / 4.0 - B2 / 2.0) / Math.Pow((1 - e * Math.Sin(B2)) / (1 + e * Math.Sin(B2)), e / 2.0));
            double n = Math.Log(m_B1 / m_B2) / Math.Log(t_B1 / t_B2);
            double F = m_B1 / (n * Math.Pow(t_B1, n));
            double r_B = 6378245 * F * Math.Pow(t_B, n);
            double angle = n * (L - L0);
            double r_B0 = 6378245 * F * Math.Pow(t_B0, n);
            X = (float)(r_B0 - r_B * Math.Cos(angle)); //北为正
            Y = (float)(r_B * Math.Sin(angle));//东为正
            pt.X=X;
            pt.Y=Y;
            return pt;
        }

    }
}
