using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CalculatePolygon.投影转换
{
    class MercatorInverse
    {
        private int B0;//标准纬度
        private int L0;//原点经度

        //墨卡托投影反算公式
        public PointF WGS84ToMecator(ShapeFile.Spoint p)
        {
            B0 = 0;
            L0 = 0;
            PointF pf=new PointF();
            //注意弧度和度之间的转换
            double a = 6378137;//长半轴
            double b = 6356752.3142;//短半轴
            double e = Math.Sqrt((a * a - b * b) / (a * a));    //第一偏心率
            double e2 = (a * a - b * b) / (b * b);//第二偏心率的平方
            double K = a * a * Math.Cos(B0 * Math.PI / 180) / b / Math.Sqrt(1 + e2 * Math.Pow(Math.Cos(B0 * Math.PI / 180), 2));
            pf.Y=1;//迭代初值
            float temp=0;//存储pf.Y的上一个值
            while (Math.Abs(pf.Y - temp) > Math.Pow(10, -6))//判断是否收敛
            {
                temp = pf.Y;
                pf.Y = (float)(Math.PI / 2 - 2 * Math.Atan(Math.Exp(-p.y / K) * 
                    Math.Exp(e / 2 * Math.Log((1 - e * Math.Sin(pf.Y)) / (1 + e * Math.Sin(pf.Y))))));//B，弧度值
            }
            pf.X = (float)(p.x / K + L0);
            return pf;
        }
    }
}
