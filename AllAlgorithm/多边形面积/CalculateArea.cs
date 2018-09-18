using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CalculatePolygon.求解面积
{
    class CalculateArea
    {
        //解平面多边形面积
        public double CalPolygon(ShapeFile.Spoint p1, ShapeFile.Spoint p2)
        {
            double A;//多边形面积
            A = p1.x * p2.y - p2.x * p1.y;
            return A / 2;
        }

        //解椭球体表面的梯形
        public double CalTrapezoid(PointF p1, PointF p2)
        {
            double T;//面积
            double a = 6378137;
            double b = 6356752.3142;
            double e2 = (a * a - b * b) / (b * b);//第一偏心率的平方
            double K = 2 * a * a * (1 - e2) * (p2.X - p1.X);// λ为经度
            double A = 1 + 1.0 / 2.0 * e2 + 3.0 / 8.0 * e2 * e2 + 5.0 / 16.0 * Math.Pow(e2, 3);
            double B = 1.0 / 6.0 * e2 + 3.0 / 16.0 * e2 * e2 + 3.0 / 16.0 * Math.Pow(e2, 3);
            double C = 3.0 / 80.0 * e2 * e2 + 1.0 / 16.0 * Math.Pow(e2, 3);
            double D = 1.0 / 112.0 * Math.Pow(e2, 3);
            double dy = p2.Y - p1.Y;//φ为纬度
            double ym = (p1.Y + p2.Y) / 2.0;
            T = K * (A * Math.Sin(dy / 2.0) * Math.Cos(ym) - B * Math.Sin(3.0 * dy / 2.0) * Math.Cos(3 * ym) +
                C * Math.Sin(5.0 * dy / 2.0) * Math.Cos(5 * ym) - D * Math.Sin(7.0 * dy / 2.0) * Math.Cos(7.0 * ym));
            return T;
        }
    }
}
