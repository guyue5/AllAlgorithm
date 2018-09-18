using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MapProjection
{
    class Graticule
    {
        private List<PointF[]> coordinate = new List<PointF[]>();//存放经纬网坐标点
        public Graticule(List<PointF[]> coordinate_st)
        {
            this.coordinate = coordinate_st;
        }

        /// <summary>
        /// 将经度线和纬度线存储
        /// </summary>
        /// <returns>
        /// 返回值为存放经纬度的泛型数组
        /// </returns>
        public List<PointF[]> StoreGraticule()
        {
            List<PointF[]> grid = new List<PointF[]>();//存储经纬网坐标点
            Boundary bd = new Boundary(coordinate);
            RectangleF box = bd.FindBoundary();
            int col = (int)Math.Ceiling((box.Width - box.X) / 5);
            int row = (int)Math.Ceiling((box.Height - box.Y) / 5);
            //经度
            for (int i = 0; i <= col; i++)
            {
                PointF[] point1 = new PointF[row + 1];
                for (int j = 0; j <= row; j++)
                {
                    point1[j].X = (int)Math.Floor(box.X) + i * 5;
                    point1[j].Y = (int)Math.Floor(box.Y) + j * 5;
                }
                grid.Add(point1);
            }
            //纬度
            for (int i = 0; i <= row; i++)
            {
                PointF[] point2 = new PointF[col + 1];
                for (int j = 0; j <= col; j++)
                {
                    point2[j].X = (int)Math.Floor(box.X) + j * 5;
                    point2[j].Y = (int)Math.Floor(box.Y) + i * 5;
                }
                grid.Add(point2);
            }
            return grid;
        }
    }
}
