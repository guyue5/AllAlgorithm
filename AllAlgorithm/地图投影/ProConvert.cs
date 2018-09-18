using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MapProjection
{
    class ProConvert
    {
        private List<PointF[]> coordinate = new List<PointF[]>();

        public ProConvert(List<PointF[]> coordinate_st)
        {
            this.coordinate = coordinate_st;
        }

        //北京54转兰伯特
        public PointF KrasovskyToLambert(PointF pf)
        {
            //基本参数
            double a = 6378245;//长轴
            double b = 6356863.01877;//短轴
            int B0 = 0;//原点纬线
            int L0 = 105;//中央经线
            int B1 = 20;//标准纬线1
            int B2 = 40;//标准纬线2
            double e = Math.Sqrt((a * a - b * b) / (a * a));//注意e是第一偏心率，不是math.e
            //转化为弧度制
            double PI=Math.PI;
            double b0=B0*PI/180;
            double l0=L0*PI/180;
            double b1=B1*PI/180;
            double b2=B2*PI/180;
            double mB1 = Math.Cos(b1) / Math.Sqrt(1 - Math.Pow(e, 2) * Math.Pow(Math.Sin(b1), 2));
            double mB2 = Math.Cos(b2)/ Math.Sqrt(1 - Math.Pow(e, 2) * Math.Pow(Math.Sin(b2), 2));
            double tB1 = Math.Tan(PI / 4 - b1 / 2) / Math.Pow((1 - e * Math.Sin(b1)) / (1 + e * Math.Sin(b1)), e / 2);
            double tB2 = Math.Tan(PI / 4 - b2 / 2) / Math.Pow((1 - e * Math.Sin(b2)) / (1 + e * Math.Sin(b2)), e / 2);
            double n=Math.Log(mB1/mB2)/Math.Log(tB1/tB2);
            double F = mB1 / (n * Math.Pow(tB1, n));  //F是定值！！！     
            double r0 = a * F;//t=1
     
            //注意经纬的的顺序
            PointF newpf = new PointF();
            double L = pf.X * PI / 180;
            double B = pf.Y * PI / 180;
            double t = Math.Tan(PI / 4 - B / 2) / Math.Pow((1 - e * Math.Sin(B)) / (1 + e * Math.Sin(B)), e / 2);
            double r = a * F * Math.Pow(t, n);
            double theta = n * (L - L0 * PI / 180);
            newpf.Y = Convert.ToSingle(r0 - r * Math.Cos(theta));//纵坐标对应纬度
            newpf.X = Convert.ToSingle(r * Math.Sin(theta));//横坐标对应经度
            return newpf;
        }

        //北京54转墨卡托
        public PointF krasovskyToMercator(PointF pf)
        {
            //基本参数
            double a = 6378245;//长轴
            double b = 6356863.01877;//短轴
            int B0 = 30;//标准纬度
            int L0 = 0;//原点经度
            int B00 = 0;//原点纬度
            double e=Math.Sqrt((a * a - b * b) / (a * a));//第一偏心率
            double e2 =(Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(b, 2);//第二偏心率的平方
            //转换为弧度
            double PI = Math.PI;
            double b0 = B0 * PI / 180;
            double l0 = L0 * PI / 180;

            double K = a * a * Math.Cos(b0) / b / Math.Sqrt(1 + e2 * Math.Pow(Math.Cos(b0), 2));
            double B=pf.Y*PI/180;
            double L=pf.X*PI/180;
            pf.Y = Convert.ToSingle(K * Math.Log(Math.Tan(PI / 4 + B / 2) * Math.Pow((1 - e * Math.Sin(B)) / (1 + e * Math.Sin(B)), e / 2)));
            pf.X=Convert.ToSingle(K*(L-l0));
            return pf;
        }

        //WGS84转墨卡托
        public PointF WGS84ToMercator(PointF pf)
        {
            //基本参数
            double a = 6378137;//长轴
            double b = 6356752.3142;//短轴
            int B0 = 30;//标准纬度
            int L0 = 0;//原点经度
            int B00 = 0;//原点纬度
            double e = Math.Sqrt((a * a - b * b) / (a * a));//第一偏心率
            double e2 = (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(b, 2);//第二偏心率的平方
            //转换为弧度
            double PI = Math.PI;
            double b0 = B0 * PI / 180;
            double l0 = L0 * PI / 180;

            double K = a * a * Math.Cos(b0) / b / Math.Sqrt(1 + e2 * Math.Pow(Math.Cos(b0), 2));
            double B = pf.Y * PI / 180;
            double L = pf.X * PI / 180;
            pf.Y = Convert.ToSingle(K * Math.Log(Math.Tan(PI / 4 + B / 2) * Math.Pow((1 - e * Math.Sin(B)) / (1 + e * Math.Sin(B)), e / 2)));
            pf.X = Convert.ToSingle(K * (L - l0));
            return pf;
        }
    }
}
