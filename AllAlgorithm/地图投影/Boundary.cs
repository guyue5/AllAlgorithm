using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MapProjection
{
    class Boundary
    {
        private List<PointF[]> coordinate = new List<PointF[]>();
        public float xmin, ymin, xmax, ymax;

        public Boundary(List<PointF[]> coordinate_st)
        {
            this.coordinate = coordinate_st;//存储坐标点
        }

        /// <summary>
        /// 寻找边界点
        /// </summary>
        /// <returns>
        /// 将四个点存在RectangleF中
        /// </returns>
        public RectangleF FindBoundary()
        {
            xmin = coordinate[0][0].X;
            ymin = coordinate[0][0].Y;
            xmax = coordinate[0][0].X;
            ymax = coordinate[0][0].Y;
            foreach (PointF[] pf in coordinate)
            {
                foreach (PointF p in pf)
                {
                    if (p.X < xmin) xmin = p.X;
                    if (p.X > xmax) xmax = p.X;
                    if (p.Y < ymin) ymin = p.Y;
                    if (p.Y > ymax) ymax = p.Y;
                }
            }
            RectangleF rect = new RectangleF(xmin, ymin, xmax, ymax);
            return rect;
        }
    }
}
