using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShapeFile
{
    /// <summary>
    /// 点
    /// </summary>
    public class Spoint
    {
        public double x;//x坐标
        public double y;//y坐标
    }

    /// <summary>
    /// 线
    /// </summary>
    public class Spolyline
    {
        public double[] Box = new double[4];//边界盒
        public int NumParts;//部分点的数目
        public int NumPoints;//总数目
        public List<int> Parts = new List<int>();//部分点中的点在总点数中的索引，为每条PolyLine存储它在点数列中的第一个点的索引。
        public List<Spoint> Points = new List<Spoint>();//存储所有的点
    }

    /// <summary>
    /// 面，存储结构与线一致
    /// </summary>
    public class Spolygon : Spolyline
    {
    }

}
