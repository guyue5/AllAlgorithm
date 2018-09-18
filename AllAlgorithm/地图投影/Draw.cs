using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MapProjection
{
    class Draw
    {
        private List<PointF[]> coordinate = new List<PointF[]>();
        private Rectangle rct;
        private double xmin, ymin, xmax, ymax;
        private double highpercent, widthpercent, scale;//缩放比例
        private int flag = Form1.flag;//标记变量

        public Draw(List<PointF[]> coordinate_st, Rectangle rct_pic)
        {
            this.coordinate = coordinate_st;
            this.rct = rct_pic;
        }

        public Bitmap DrawGraticule()
        {
            Bitmap map = new Bitmap(rct.Width, rct.Height);
            Graphics g = Graphics.FromImage(map);
            g.TranslateTransform(0, rct.Height);
            g.ScaleTransform(1, -1);
            Pen pen = new Pen(Brushes.Black);
            Graticule gr = new Graticule(coordinate);
            List<PointF[]> graticule = gr.StoreGraticule();//存了网格坐标
            switch (flag)
            {
                case 1://原始坐标系
                    Boundary bd = new Boundary(graticule);
                    bd.FindBoundary();
                    xmin = bd.xmin; ymin = bd.ymin; xmax = bd.xmax; ymax = bd.ymax;
                    highpercent = rct.Height / (ymax - ymin);
                    widthpercent = rct.Width / (xmax - xmin);
                    scale = highpercent > widthpercent ? widthpercent : highpercent;//取小的比例，统一
                    foreach (PointF[] pointf in graticule)
                    {
                        PointF[] tempkro = new PointF[pointf.Length];
                        for (int i = 0; i < pointf.Length; i++)
                        {
                            tempkro[i].X = (float)((pointf[i].X - xmin) * scale + (rct.Width - (xmax - xmin) * scale) / 2);
                            tempkro[i].Y = (float)((pointf[i].Y - ymin) * scale + (rct.Height - (ymax - ymin) * scale) / 2);
                        }
                        g.DrawLines(pen, tempkro);
                    }
                    break;
                case 2://北京54转兰伯特
                    List<PointF[]> grlam = new List<PointF[]>();
                    ProConvert pclam = new ProConvert(graticule);
                    foreach (PointF[] pf in graticule)
                    {
                        //感觉存储可能会出错误,因为list的引用型错误
                        PointF[] ptemp = new PointF[pf.Length];
                        for (int i = 0; i < pf.Length; i++)
                            ptemp[i] = pclam.KrasovskyToLambert(pf[i]);
                        grlam.Add(ptemp);
                    }
                    Boundary bdlam = new Boundary(grlam);
                    bdlam.FindBoundary();
                    xmin = bdlam.xmin; ymin = bdlam.ymin; xmax = bdlam.xmax; ymax = bdlam.ymax;
                    highpercent = rct.Height / (ymax - ymin);
                    widthpercent = rct.Width / (xmax - xmin);
                    scale = highpercent > widthpercent ? widthpercent : highpercent;//取小的比例，统一
                    foreach (PointF[] pointf in grlam)
                    {
                        for (int i = 0; i < pointf.Length; i++)
                        {
                            pointf[i].X = (float)((pointf[i].X - xmin) * scale + (rct.Width - (xmax - xmin) * scale) / 2);
                            pointf[i].Y = (float)((pointf[i].Y - ymin) * scale + (rct.Height - (ymax - ymin) * scale) / 2);
                        }
                        g.DrawLines(pen, pointf);
                    }
                    break;
                case 3://北京54转墨卡托
                    List<PointF[]> grmer = new List<PointF[]>();
                    ProConvert pcmer = new ProConvert(graticule);
                    foreach (PointF[] pf in graticule)
                    {
                        //感觉存储可能会出错误,因为list的引用型错误
                        PointF[] ptemp = new PointF[pf.Length];
                        for (int i = 0; i < pf.Length; i++)
                            ptemp[i] = pcmer.krasovskyToMercator(pf[i]);
                        grmer.Add(ptemp);
                    }
                    Boundary bdmer = new Boundary(grmer);
                    bdmer.FindBoundary();
                    xmin = bdmer.xmin; ymin = bdmer.ymin; xmax = bdmer.xmax; ymax = bdmer.ymax;
                    highpercent = rct.Height / (ymax - ymin);
                    widthpercent = rct.Width / (xmax - xmin);
                    scale = highpercent > widthpercent ? widthpercent : highpercent;//取小的比例，统一
                    foreach (PointF[] pointf in grmer)
                    {
                        for (int i = 0; i < pointf.Length; i++)
                        {
                            pointf[i].X = (float)((pointf[i].X - xmin) * scale + (rct.Width - (xmax - xmin) * scale) / 2);
                            pointf[i].Y = (float)((pointf[i].Y - ymin) * scale + (rct.Height - (ymax - ymin) * scale) / 2);
                        }
                        g.DrawLines(pen, pointf);
                    }
                    break;
                case 4://WGS84转墨卡托
                    List<PointF[]> grwgs = new List<PointF[]>();
                    ProConvert pcwgs = new ProConvert(graticule);
                    foreach (PointF[] pf in graticule)
                    {
                        //感觉存储可能会出错误,因为list的引用型错误
                        PointF[] ptemp = new PointF[pf.Length];
                        for (int i = 0; i < pf.Length; i++)
                            ptemp[i] = pcwgs.WGS84ToMercator(pf[i]);
                        grwgs.Add(ptemp);
                    }
                    Boundary bdwgs = new Boundary(grwgs);
                    bdwgs.FindBoundary();
                    xmin = bdwgs.xmin; ymin = bdwgs.ymin; xmax = bdwgs.xmax; ymax = bdwgs.ymax;
                    highpercent = rct.Height / (ymax - ymin);
                    widthpercent = rct.Width / (xmax - xmin);
                    scale = highpercent > widthpercent ? widthpercent : highpercent;//取小的比例，统一
                    foreach (PointF[] pointf in grwgs)
                    {
                        for (int i = 0; i < pointf.Length; i++)
                        {
                            pointf[i].X = (float)((pointf[i].X - xmin) * scale + (rct.Width - (xmax - xmin) * scale) / 2);
                            pointf[i].Y = (float)((pointf[i].Y - ymin) * scale + (rct.Height - (ymax - ymin) * scale) / 2);
                        }
                        g.DrawLines(pen, pointf);
                    }
                    break;
            }
            return map;

        }

        public Bitmap DrawPro(Bitmap map)
        {
            Bitmap nmap = new Bitmap(map);
            Graphics g = Graphics.FromImage(nmap);
            g.TranslateTransform(0, rct.Height);
            g.ScaleTransform(1, -1);
            Pen pen = new Pen(Brushes.Blue);
            switch (flag)
            {
                case 1://原始坐标系
                    Boundary bd = new Boundary(coordinate);
                    bd.FindBoundary();
                    xmin = bd.xmin; ymin = bd.ymin; xmax = bd.xmax; ymax = bd.ymax;
                    highpercent = rct.Height / (ymax - ymin);
                    widthpercent = rct.Width / (xmax - xmin);
                    scale = highpercent > widthpercent ? widthpercent : highpercent;//取小的比例，统一
                    foreach (PointF[] pointf in coordinate)
                    {
                        PointF[] tempkro = new PointF[pointf.Length];
                        for (int i = 0; i < pointf.Length; i++)
                        {
                            tempkro[i].X = (float)((pointf[i].X - xmin) * scale + (rct.Width - (xmax - xmin) * scale) / 2);
                            tempkro[i].Y = (float)((pointf[i].Y - ymin) * scale + (rct.Height - (ymax - ymin) * scale) / 2);
                        }
                        g.DrawLines(pen, tempkro);
                    }
                    break;
                case 2://北京54转兰伯特
                    List<PointF[]> grlam = new List<PointF[]>();
                    ProConvert pclam = new ProConvert(coordinate);
                    foreach (PointF[] pf in coordinate)
                    {
                        PointF[] ptemp = new PointF[pf.Length];
                        for (int i = 0; i < pf.Length; i++)
                            ptemp[i] = pclam.KrasovskyToLambert(pf[i]);
                        grlam.Add(ptemp);
                    }
                    Boundary bdlam = new Boundary(grlam);
                    bdlam.FindBoundary();
                    xmin = bdlam.xmin; ymin = bdlam.ymin; xmax = bdlam.xmax; ymax = bdlam.ymax;
                    highpercent = rct.Height / (ymax - ymin);
                    widthpercent = rct.Width / (xmax - xmin);
                    scale = highpercent > widthpercent ? widthpercent : highpercent;//取小的比例，统一
                    foreach (PointF[] pointf in grlam)
                    {
                        for (int i = 0; i < pointf.Length; i++)
                        {
                            pointf[i].X = (float)((pointf[i].X - xmin) * scale + (rct.Width - (xmax - xmin) * scale) / 2);
                            pointf[i].Y = (float)((pointf[i].Y - ymin) * scale + (rct.Height - (ymax - ymin) * scale) / 2);
                        }
                        g.DrawLines(pen, pointf);
                    }
                    break;
                case 3://北京54转墨卡托
                    List<PointF[]> grmer = new List<PointF[]>();
                    ProConvert pcmer = new ProConvert(coordinate);
                    foreach (PointF[] pf in coordinate)
                    {
                        //感觉存储可能会出错误,因为list的引用型错误
                        PointF[] ptemp = new PointF[pf.Length];
                        for (int i = 0; i < pf.Length; i++)
                            ptemp[i] = pcmer.krasovskyToMercator(pf[i]);
                        grmer.Add(ptemp);
                    }
                    Boundary bdmer = new Boundary(grmer);
                    bdmer.FindBoundary();
                    xmin = bdmer.xmin; ymin = bdmer.ymin; xmax = bdmer.xmax; ymax = bdmer.ymax;
                    highpercent = rct.Height / (ymax - ymin);
                    widthpercent = rct.Width / (xmax - xmin);
                    scale = highpercent > widthpercent ? widthpercent : highpercent;//取小的比例，统一
                    foreach (PointF[] pointf in grmer)
                    {
                        for (int i = 0; i < pointf.Length; i++)
                        {
                            pointf[i].X = (float)((pointf[i].X - xmin) * scale + (rct.Width - (xmax - xmin) * scale) / 2);
                            pointf[i].Y = (float)((pointf[i].Y - ymin) * scale + (rct.Height - (ymax - ymin) * scale) / 2);
                        }
                        g.DrawLines(pen, pointf);
                    }
                    break;
                case 4://WGS84转墨卡托
                    List<PointF[]> grwgs = new List<PointF[]>();
                    ProConvert pcwgs = new ProConvert(coordinate);
                    foreach (PointF[] pf in coordinate)
                    {
                        PointF[] ptemp = new PointF[pf.Length];
                        for (int i = 0; i < pf.Length; i++)
                            ptemp[i] = pcwgs.WGS84ToMercator(pf[i]);
                        grwgs.Add(ptemp);
                    }
                    Boundary bdwgs = new Boundary(grwgs);
                    bdwgs.FindBoundary();
                    xmin = bdwgs.xmin; ymin = bdwgs.ymin; xmax = bdwgs.xmax; ymax = bdwgs.ymax;
                    highpercent = rct.Height / (ymax - ymin);
                    widthpercent = rct.Width / (xmax - xmin);
                    scale = highpercent > widthpercent ? widthpercent : highpercent;//取小的比例，统一
                    foreach (PointF[] pointf in grwgs)
                    {
                        for (int i = 0; i < pointf.Length; i++)
                        {
                            pointf[i].X = (float)((pointf[i].X - xmin) * scale + (rct.Width - (xmax - xmin) * scale) / 2);
                            pointf[i].Y = (float)((pointf[i].Y - ymin) * scale + (rct.Height - (ymax - ymin) * scale) / 2);
                        }
                        g.DrawLines(pen, pointf);
                    }
                    break;
            }
            return nmap;
        }

        public Bitmap DrawOrbit(Bitmap map)
        {
            Bitmap nnmap = new Bitmap(map);
            Graphics g = Graphics.FromImage(nnmap);
            //反转坐标系绘图
            g.TranslateTransform(0, rct.Height);
            g.ScaleTransform(1, -1);
            Pen pen = new Pen(Brushes.Red);
            Orbit or = new Orbit();
            PointF[] pt = or.OrbitPoint(10);
            ProConvert pc=new ProConvert(coordinate);
            for (int i = 0; i < pt.Length; i++)
            {
                pt[i] =pc.WGS84ToMercator(pt[i]);
            }
            for (int i = 0; i < pt.Length; i++)
            {
                pt[i] = new PointF((float)((pt[i].X - xmin) * scale + (rct.Width - (xmax - xmin) * scale) / 2), (float)((pt[i].Y - ymin) * scale + (rct.Height - (ymax - ymin) * scale) / 2));
            }
            g.DrawLines(pen, pt.ToArray());
            //恢复原坐标系，输出字体
            g.TranslateTransform(0, rct.Height);
            g.ScaleTransform(1, -1);
            string bplace = "北京";
            string pplace = "巴黎";
            pt[0].Y = rct.Height - pt[0].Y;
            pt[pt.Length - 1].Y = rct.Height - pt[pt.Length - 1].Y;
            g.DrawString(bplace, new Font("宋体", 15), Brushes.Red, pt[pt.Length - 1]);
            g.DrawString(pplace, new Font("宋体", 15), Brushes.Red, pt[0]);
            return nnmap;
        }
    }
}


