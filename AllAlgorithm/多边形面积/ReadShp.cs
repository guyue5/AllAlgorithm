using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Collections;

namespace ShapeFile
{
    class ReadShp
    {
        #region 私有属性
        private List<Spoint> listpoint = new List<Spoint>();
        private List<Spolyline> listpolyline = new List<Spolyline>();
        public List<Spolygon> listpolygon = new List<Spolygon>();
        public List<List<List<PointF>>> polylist = new List<List<List<PointF>>>();//原始多边形集合
        public List<List<List<PointF>>> merpolylist = new List<List<List<PointF>>>();//原始多边形集合
      //  public ArrayList polylist = new ArrayList();//原始多边形集合
      //  public ArrayList merpolylist = new ArrayList();//墨卡托下的多边形集合
        private int shapetype;
        private double Xmin;
        private double Xmax;
        private double Ymin;
        private double Ymax;
        #endregion

        # region 读文件
        public void ReadShape(string filename)
        {
            using (BinaryReader br = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                br.ReadBytes(32);//跳过开始的几个字节
                shapetype = br.ReadInt32();//读取文件类型
                Xmin = br.ReadDouble();
                Ymin = br.ReadDouble();
                Xmax = br.ReadDouble();
                Ymax = br.ReadDouble();
                br.ReadBytes(32);//读完文件头
                switch (shapetype)
                {
                    case 1://点
                        listpoint.Clear();//清空
                        while (br.PeekChar() != -1)
                        {
                            br.ReadBytes(12);//每个变长度记录是由固定长度的记录头和接着的变长度记录内容，跳过记录头和shape
                            Spoint point = new Spoint();
                            point.x = br.ReadDouble();
                            point.y = br.ReadDouble();
                            listpoint.Add(point);
                        }
                        break;
                    case 3://线
                        listpolyline.Clear();
                        while (br.PeekChar() != -1)
                        {
                            br.ReadBytes(12);
                            Spolyline polyline = new Spolyline();
                            polyline.Box[0] = br.ReadDouble();
                            polyline.Box[1] = br.ReadDouble();
                            polyline.Box[2] = br.ReadDouble();
                            polyline.Box[3] = br.ReadDouble();
                            polyline.NumParts = br.ReadInt32();
                            polyline.NumPoints = br.ReadInt32();
                            for (int i = 0; i < polyline.NumParts; i++)
                            {
                                polyline.Parts.Add(br.ReadInt32());
                            }
                            for (int j = 0; j < polyline.NumPoints; j++)
                            {
                                Spoint point = new Spoint();
                                point.x = br.ReadDouble();
                                point.y = br.ReadDouble();
                                polyline.Points.Add(point);
                            }
                            listpolyline.Add(polyline);
                        }
                        break;
                    case 5://多边形
                        listpolygon.Clear();
                        while (br.PeekChar() != -1)
                        {
                            br.ReadBytes(12);                           
                            Spolygon polygon = new Spolygon();
                            polygon.Box[0] = br.ReadDouble();
                            polygon.Box[1] = br.ReadDouble();
                            polygon.Box[2] = br.ReadDouble();
                            polygon.Box[3] = br.ReadDouble();
                            polygon.NumParts = br.ReadInt32();
                            polygon.NumPoints = br.ReadInt32();
                            for (int i = 0; i < polygon.NumParts; i++)
                            {
                                polygon.Parts.Add(br.ReadInt32());
                            }
                            for (int j = 0; j < polygon.NumPoints; j++)
                            {
                                Spoint point = new Spoint();
                                point.x = br.ReadDouble();
                                point.y = br.ReadDouble();
                                polygon.Points.Add(point);
                            }
                            listpolygon.Add(polygon);
                        }
                        break;
                }      
            }
        }
        #endregion

        #region 显示图像
        public Bitmap DrawShp(Rectangle rct)
        {
            Bitmap shp = new Bitmap(rct.Width, rct.Height);
            Graphics g = Graphics.FromImage(shp);
            g.TranslateTransform(0, rct.Height);
            g.ScaleTransform(1, -1);
            Pen pen = new Pen(Color.Black, 1);
            double per = Math.Min(rct.Height / (Ymax - Ymin), rct.Width / (Xmax - Xmin));//缩放比例

            switch (shapetype)
            {
                case 1://点
                    foreach (Spoint sp in listpoint)
                    {
                        //按比例缩放绘点，以小圆的形式绘点
                        PointF p = new PointF();

                        p.X = (float)((sp.x - Xmin) * per);
                        p.Y = (float)((sp.y - Ymin) * per);
                        g.DrawEllipse(pen, p.X, p.Y, 2, 2);
                    }
                    break;
                case 3://线
                    foreach (Spolyline sl in listpolyline)
                    {
                        List<PointF> p = new List<PointF>();
                        for (int i = 0; i < sl.NumParts - 1; i++)
                        {
                            int start = sl.Parts[i];//部分点的起始点的索引
                            int end = sl.Parts[i + 1];//部分点的第二个点在总的点数组中的索引
                            for (; start < end; start++)
                                p.Add(new PointF((float)((sl.Points[start].x - Xmin) * per), (float)((sl.Points[start].y - Ymin) * per)));
                            g.DrawLines(pen, p.ToArray());
                            p.Clear();//清空
                        }
                        for (int j = sl.Parts[sl.NumParts - 1]; j < sl.NumPoints; j++)
                            p.Add(new PointF((float)((sl.Points[j].x - Xmin) * per), (float)((sl.Points[j].y - Ymin) * per)));
                        g.DrawLines(pen, p.ToArray());
                        p.Clear();
                    }
                    break;
                case 5://多边形
                    foreach (Spolygon sg in listpolygon)
                    {
                        List<List<PointF>> pointslist = new List<List<PointF>>();//原始图像
                        List<PointF> p = new List<PointF>();
                        for (int i = 0; i < sg.NumParts - 1; i++)
                        {
                            List<PointF> temp = new List<PointF>();//岛的存放
                            int start = sg.Parts[i];//部分点的起始点的索引
                            int end = sg.Parts[i + 1];//部分点的第二个点在总的点数组中的索引
                            for (; start < end; start++)
                            {
                                PointF k=new PointF();
                                k.X=(float)sg.Points[start].x;
                                k.Y=(float)sg.Points[start].y;
                                p.Add(new PointF((float)((sg.Points[start].x - Xmin) * per), (float)((sg.Points[start].y - Ymin) * per)));
                                temp.Add(new PointF((float)((sg.Points[start].x - Xmin) * per), (float)((sg.Points[start].y - Ymin) * per)));
                            }
                            pointslist.Add(temp);//循环完后，相当于存放了所有的岛
                            g.DrawPolygon(pen, p.ToArray());
                            p.Clear();
                        }
                        List<PointF> temp1 = new List<PointF>();//存放的是非岛的部分的所有点
                        for (int j = sg.Parts[sg.NumParts - 1]; j < sg.NumPoints; j++)
                        {
                            PointF k = new PointF();
                            k.X = (float)sg.Points[j].x;
                            k.Y = (float)sg.Points[j].y;
                            p.Add(new PointF((float)((sg.Points[j].x - Xmin) * per), (float)((sg.Points[j].y - Ymin) * per)));
                            temp1.Add(new PointF((float)((sg.Points[j].x - Xmin) * per), (float)((sg.Points[j].y - Ymin) * per)));
                        }
                        pointslist.Add(temp1);//存放了第一个多边形的所有的
                        g.DrawPolygon(pen, p.ToArray());//绘制多边形
                        p.Clear();
                        polylist.Add(pointslist);
                    }
                    break;
            }
            return shp;
        }
        #endregion

        #region 求解平面下的面积
        public List<double> CalArea()
        {
            List<double> area = new List<double>();
            CalculatePolygon.求解面积.CalculateArea ca = new CalculatePolygon.求解面积.CalculateArea();
            double Area;//计算面积
            switch (shapetype)
            {
                case 5://多边形
                    foreach (Spolygon sg in listpolygon)
                    {
                        Area = 0;
                        for (int i = 0; i < sg.NumParts - 1; i++)
                        {
                            int start = sg.Parts[i];//部分点的起始点的索引
                            int end = sg.Parts[i + 1];//部分点的第二个点在总的点数组中的索引
                            for (; start < end; start++)
                            {
                                if (start < end - 1)
                                    Area = Area + ca.CalPolygon(sg.Points[start], sg.Points[start + 1]);
                            }
                        }
                        for (int j = sg.Parts[sg.NumParts - 1]; j < sg.NumPoints; j++)
                        {
                            if (j < sg.NumPoints - 1)
                                Area = Area + ca.CalPolygon(sg.Points[j], sg.Points[j + 1]);
                        }
                        //可能存在负值，取绝对值
                        //area.Add(Area);//添加面积数据
                        area.Add(Math.Abs(Area));
                    }
                    break;
            }
            return area;
        }
        #endregion

        #region 墨卡托投影反算
        public Bitmap MercatorInverse(Rectangle rct)
        {
            CalculatePolygon.投影转换.MercatorInverse mer = new CalculatePolygon.投影转换.MercatorInverse();
            Bitmap mi = new Bitmap(rct.Width, rct.Height);
            Graphics g = Graphics.FromImage(mi);
            g.TranslateTransform(0, rct.Height);
            g.ScaleTransform(1, -1);
            Pen pen = new Pen(Color.Black, 1);
            #region 重新求取边界
            Spoint min = new Spoint();
            PointF min1 = new PointF();
            Spoint max = new Spoint();
            PointF max1 = new PointF();
            min.x = Xmin; min.y = Ymin; max.x = Xmax; max.y= Ymax;
            min1 = mer.WGS84ToMecator(min);
            max1 = mer.WGS84ToMecator(max);
            double ymax, ymin, xmax, xmin;
            ymax = max1.Y; ymin = min1.Y; xmax = max1.X; xmin = min1.X;
            double per = Math.Min(rct.Height / (ymax - ymin), rct.Width / (xmax - xmin));
            #endregion

            switch (shapetype)
            {
                case 1://点
                    foreach (Spoint sp in listpoint)
                    {
                        //按比例缩放绘点，以小圆的形式绘点
                        PointF p = new PointF();
                        p = mer.WGS84ToMecator(sp);
                        p.X = (float)((p.X - xmin) * per);
                        p.Y = (float)((p.Y - ymin) * per);
                        g.DrawEllipse(pen, p.X, p.Y, 2, 2);
                    }
                    break;
                case 3://线
                    foreach (Spolyline sl in listpolyline)
                    {
                        List<PointF> p = new List<PointF>();
                        PointF pl = new PointF();
                        for (int i = 0; i < sl.NumParts - 1; i++)
                        {
                            int start = sl.Parts[i];//部分点的起始点的索引
                            int end = sl.Parts[i + 1];//部分点的第二个点在总的点数组中的索引
                            for (; start < end; start++)
                            {
                                pl = mer.WGS84ToMecator(sl.Points[start]);
                                pl.X = (float)((pl.X- xmin) * per);
                                pl.Y = (float)((pl.Y - ymin) * per);
                                p.Add(pl);
                            }
                            g.DrawLines(pen, p.ToArray());
                            p.Clear();//清空
                        }
                        for (int j = sl.Parts[sl.NumParts - 1]; j < sl.NumPoints; j++)
                        {
                            pl = mer.WGS84ToMecator(sl.Points[j]);
                            pl.X = (float)((pl.X - xmin) * per);
                            pl.Y = (float)((pl.Y - ymin) * per);
                            p.Add(pl);
                        }
                        g.DrawLines(pen, p.ToArray());
                        p.Clear();
                    }
                    break;
                case 5://多边形
                    foreach (Spolygon sg in listpolygon)
                    {
                        List<List<PointF>> merpointslist = new List<List<PointF>>();//墨卡托下的图像
                        PointF p2 = new PointF();
                        List<PointF> p = new List<PointF>();
                        for (int i = 0; i < sg.NumParts - 1; i++)
                        {
                            List<PointF> temp = new List<PointF>();//岛的存放
                            int start = sg.Parts[i];//部分点的起始点的索引
                            int end = sg.Parts[i + 1];//部分点的第二个点在总的点数组中的索引
                            for (; start < end; start++)
                            {
                                p2 = mer.WGS84ToMecator(sg.Points[start]);
                                p2.X = (float)((p2.X - xmin) * per);
                                p2.Y = (float)((p2.Y - ymin) * per);
                                p.Add(p2);
                                temp.Add(p2);
                            }
                            g.DrawPolygon(pen, p.ToArray());
                            merpointslist.Add(temp);//循环完后，相当于存放了所有的岛
                            p.Clear();
                        }
                        List<PointF> temp1 = new List<PointF>();//存放的是非岛的部分的所有点
                        for (int j = sg.Parts[sg.NumParts - 1]; j < sg.NumPoints; j++)
                        {
                            p2 = mer.WGS84ToMecator(sg.Points[j]);
                            p2.X = (float)((p2.X - xmin) * per);
                            p2.Y = (float)((p2.Y- ymin) * per);
                            p.Add(p2);
                            temp1.Add(p2);
                        }
                        merpointslist.Add(temp1);//存放了第一个多边形的所有的
                        g.DrawPolygon(pen, p.ToArray());//绘制多边形
                        p.Clear();
                        merpolylist.Add(merpointslist);
                    }
                    break;
            }
            return mi;
        }
        #endregion

        #region 求墨卡托投影下的面积
        public List<double> CalTrapezoid()
        {
            List<double> area = new List<double>();
            CalculatePolygon.求解面积.CalculateArea ca = new CalculatePolygon.求解面积.CalculateArea();
            //一定要转换为墨卡托！！！
            CalculatePolygon.投影转换.MercatorInverse mer = new CalculatePolygon.投影转换.MercatorInverse();
            double T;//计算面积
            switch (shapetype)
            {
                case 5://多边形
                    foreach (Spolygon sg in listpolygon)
                    {
                        T = 0;
                        for (int i = 0; i < sg.NumParts - 1; i++)
                        {
                            int start = sg.Parts[i];//部分点的起始点的索引
                            int end = sg.Parts[i + 1];//部分点的第二个点在总的点数组中的索引
                            for (; start < end; start++)
                            {
                                PointF p1 = new PointF();
                                PointF p2 = new PointF();
                                if (start < end - 1)
                                {
                                    p1 = mer.WGS84ToMecator(sg.Points[start]);
                                    p2 = mer.WGS84ToMecator(sg.Points[start+1]);
                                    T = T + ca.CalTrapezoid(p1, p2);
                                }
                            }
                        }
                        for (int j = sg.Parts[sg.NumParts - 1]; j < sg.NumPoints; j++)
                        {
                            PointF p1 = new PointF();
                            PointF p2 = new PointF();
                            if (j < sg.NumPoints - 1)
                            {
                                p1 = mer.WGS84ToMecator(sg.Points[j]);
                                p2 = mer.WGS84ToMecator(sg.Points[j + 1]);
                                T = T + ca.CalTrapezoid(p1, p2);
                            }
                        }
                        area.Add(Math.Abs(T));
                    }
                    break;
            }
            return area;
        }
        #endregion 
    }
}
