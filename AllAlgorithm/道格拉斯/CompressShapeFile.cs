using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Douglas
{
    class CompressShapeFile //shapefile的压缩
    {
        public ArrayList AllData = new ArrayList();
        int compress = 0;
        public ArrayList PtData = new ArrayList(); 

        ArrayList distmp = new ArrayList();
        

        public CompressShapeFile(ArrayList YourData,int YourCompress)
        {
            foreach (ArrayList data1 in YourData)//数据复制，将YourData复制到AllData
            {
                ArrayList datatmp=new ArrayList();
                foreach (PointF data2 in data1)
                    datatmp.Add(data2);
                AllData.Add(datatmp);
            }
            compress = YourCompress;

            
            foreach(ArrayList data1 in AllData) //每一条直线进行处理
            {
                ArrayList node = new ArrayList();//记录每一条直线的关键节点
                ArrayList ThisLine = new ArrayList();
                ArrayList TmpNode = new ArrayList();
                node.Add(0);
                node.Add(data1.Count - 1);
                if (data1.Count <= 2) //压缩过小
                {
                    ThisLine.Add(data1[0]);
                    ThisLine.Add(data1[data1.Count - 1]);
                    PtData.Add(ThisLine);
                    continue;
                }


                int num=(int)(100.0/compress+0.5);
                int Num2 = (int)(data1.Count * 1.0 * (compress * 1.0) / 100.0 + 0.5);//需要的总节点数目
                int Mid = KeyNode(data1, 0, data1.Count - 1, 1);
                string s = Mid.ToString() + "," + "0," + (data1.Count-1).ToString();
                TmpNode.Add(s);
                node.Add(Mid);
                DaogeNode2(data1, node, TmpNode, Num2);

                ArrayList Node2=new ArrayList();
                for (int i = 0; (i <= Num2)&(i<=node.Count-1); i++)
                    Node2.Add(node[i]);
                    Node2.Sort();
                foreach (int i in Node2)
                    ThisLine.Add(data1[i]);
                PtData.Add(ThisLine);
            }


        }

        public float Dist(PointF C,PointF A,PointF B)//求C到直线A,B的距离
        {
                float Dis = 0f;
                double L2 = (A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y);
                if (L2 == 0.0)
                {
                    return (float)Math.Sqrt((C.X - A.X) * (C.X - A.X) + (C.Y - A.Y) * (C.Y - A.Y));
                }
                double s = ((A.Y - C.Y) * (B.X - A.X) - (A.X - C.X) * (B.Y - A.Y)) / L2;
                Dis = (float)(Math.Abs(s * Math.Sqrt(L2)));
                return Dis;
        }

        public void DaogeNode2(ArrayList pts,ArrayList ResNode,ArrayList TmpNode,int AllNum)//广度优先遍历，pts直线，ResNode关键节点，TmpNode队列类型为string,AllNum为所需要的节点数目
        {
            if ((TmpNode.Count == 0)|(ResNode.Count >= AllNum)) 
                return;
            
            string s = (string)TmpNode[0];
            TmpNode.RemoveAt(0);//出队列
            string[] Info = s.Split(new char[] { ',' });
            int Mid = Convert.ToInt16(Info[0]);
            int Fir = Convert.ToInt16(Info[1]);
            int End = Convert.ToInt16(Info[2]);
            //找到Fir和Mid、Mid和End之间的最大节点
            int NodeNum = KeyNode(pts, Fir, Mid, 1);
            if(NodeNum!=-1)
            {
                ResNode.Add(NodeNum);
                //入队列
                s = NodeNum.ToString() + "," + Fir.ToString() + "," + Mid.ToString();
                TmpNode.Add(s);
            }

            NodeNum = KeyNode(pts,Mid,End, 1);
            if (NodeNum != -1)
            {
                ResNode.Add(NodeNum);
                //入队列
                s = NodeNum.ToString() + "," + Mid.ToString() + "," + End.ToString();
                TmpNode.Add(s);
            }
            DaogeNode2(pts, ResNode, TmpNode, AllNum);//递归，广度优先遍历寻找下一个关键节点
        }

        public int KeyNode(ArrayList pts,int StartIndex,int EndIndex,int num)//返回StartIndex和EndIndex之间的关键节点
        {
            int NodeNum = -1;
            if (EndIndex - StartIndex <= num) //表示EndIndex和StartIndex之间没有关键节点
                return NodeNum;
            float dis = float.MinValue;
            for (int i = StartIndex + 1; i < EndIndex; i++)//求最大距离
            {
                PointF tmp = (PointF)pts[i];
                float distmp = Dist(tmp, (PointF)pts[StartIndex], (PointF)pts[EndIndex]);
                if (distmp > dis)
                {
                    dis = distmp;
                    NodeNum=i;
                }
            }
            return NodeNum;
        }
    }
}
