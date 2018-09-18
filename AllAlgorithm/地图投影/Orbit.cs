using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MapProjection
{
    class Orbit
    {
        public PointF OrbitMer(double lat1,double lon1,double lat2,double lon2)
        {
            //注意经纬度的转换！！！
            double Bx = Math.Cos(lat2 * Math.PI / 180) * Math.Cos(lon2 * Math.PI / 180 - lon1 * Math.PI / 180);
            double By = Math.Cos(lat2 * Math.PI / 180) * Math.Sin(lon2 * Math.PI / 180 - lon1 * Math.PI / 180);
            double latm = (Math.Atan2(Math.Sin(lat1 * Math.PI / 180) + Math.Sin(lat2 * Math.PI / 180), Math.Sqrt(Math.Pow((Math.Cos(lat1 * Math.PI / 180) + Bx), 2) + By * By))) * 180 / Math.PI;
            double lonm = lon1 + (Math.Atan2(By, Math.Cos(lat1 * Math.PI / 180) + Bx)) * 180 / Math.PI;
            PointF pt = new PointF();
            pt.X = (float)lonm;
            pt.Y = (float)latm;
            return pt;
        }

        public PointF[] OrbitPoint(int circlenum)
        {
            //迭代求中间点
            int pointnum =(int) Math.Pow(2, circlenum) + 1;//总点数
            PointF[] orbitpoint = new PointF[pointnum];
            orbitpoint[0]=new PointF(2.2F,48.52F);
            orbitpoint[pointnum - 1] = new PointF(116.4F, 39.8F);
            for (int i = 0; i < circlenum ; i++)
            {
                int newnum = (int)Math.Pow(2, i);//每次循环产生的新点个数
                int index = pointnum / (newnum * 2);//中间点的索引
                int gainstep = index;//与0点的索引间距
                int newindex = pointnum / newnum;//新点之间的间距
                for (int j = 0; j < newnum; j++)
                {
                    orbitpoint[index] = OrbitMer(orbitpoint[index - gainstep].Y, orbitpoint[index - gainstep].X, orbitpoint[index + gainstep].Y, orbitpoint[index + gainstep].X);
                    index = index + newindex;//寻找下一个点
                }
            }
            return orbitpoint;
        }
      
    }
}
