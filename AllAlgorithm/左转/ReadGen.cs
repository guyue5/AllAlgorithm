using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;


namespace CreateTopology
{
    class ReadGen//GEN读取类
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
                            double[] now_data=new double[2]{0d,0d};
                            now_data[0]=Convert.ToDouble(tmp[0]);
                            now_data[1]=Convert.ToDouble(tmp[1]);
                            Line.Add(now_data);
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
}

    public class Node//结点
    {
        public int ID//节点ID号
        {
            get;
            set;
        }
        public double X//节点位置
        {
            get;
            set;
        }
        public double Y
        {
            get;
            set;
        }

        public Node(int _ID,double _X,double _Y)
        {
            ID = _ID;
            this.X = _X;
            this.Y = _Y;
        }
        public Node()
        {
            ID = 0;
            X = 0d;
            Y = 0d;
        }
    }

    public class point//坐标
    {
        public double X;
        public double Y;
        public point(double _X,double _Y)
        {
            X = _X;
            Y = _Y;
        }
    }

    public class Arc//弧段
    {
        public int ID;
        public Node HeadNode;
        public Node EndNode;//起终结点
        public List<point> data;//中间数据点，包括起始终点结点
        public double Direction;//从起点开始的与X轴的正向夹角
        public double Direction2;//从末尾点开始的的夹角
        public int UseTime;//弧段使用次数,0代表未使用过，1代表使用了从HeadNode开始的弧段，
                           //2代表使用了从EndNode开始的方向，3代表使用了两次
        public int LeftPolyID;//左多边形ID号
        public int RightPolyID;//右多边形ID号
        public Arc(int _ID,Node _HeadNode,Node _EndNode,List<point> _data,double _Direction)
        {
            ID = _ID;
            HeadNode = _HeadNode;
            EndNode = _EndNode;
            data = _data;
            Direction = _Direction;
            UseTime = 0;
            LeftPolyID = -1;
            RightPolyID = -1;
        }
        public Arc()
        {
            HeadNode = new Node();
            EndNode = new Node();
            data = new List<point>();
            UseTime = 0;
            LeftPolyID = -1;
            RightPolyID = -1;
        }
    }

    public class Polygon//多边形
    {
        public int ID;
        public ArrayList ArcPart;//多边形外边界组成的弧段ID号集合
        public ArrayList ArcPart2;//多边形岛组成的弧段ID号集合

        public List<Arc> Boundary;//边界点集合
        //public ArrayList IsLand;//岛上的点集合
        public double ArcPartArea;//多边形外边界的面积
        public double ArcPartArea2;//多边形岛的总面积
        public double Area;//总面积
        public Polygon()
        {
            ArcPart = new ArrayList();
            ArcPart2 = new ArrayList();
            ArcPartArea = 0d;
            ArcPartArea2 = 0d;
        }
    }
}