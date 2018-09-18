using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CreateTopology
{
    class Method//方法类
    {
        public void CalNode(List<Node> NodeData,ArrayList GenData)//生成结点信息
        {
            ArrayList FirstLine =(ArrayList) GenData[0];
            int IDNum = 1;

            foreach (ArrayList OneArc in GenData)
            {
                double[] FirstPoint = (double[])OneArc[0];//取出第一个点
                double[] EndPoint = (double[])OneArc[OneArc.Count-1];//取出第一个点
                Node NewNode1 = new Node(IDNum, FirstPoint[0], FirstPoint[1]);
                Node NewNode2 = new Node(IDNum, EndPoint[0], EndPoint[1]);//可以暂时不考虑ID号

                if (NodeData == null)//结点数据区域为空的情况
                {
                    NodeData.Add(new Node(IDNum,FirstPoint[0], FirstPoint[1]));
                    IDNum++;
                }
                
                if(!InNodes(NewNode1,NodeData))//说明NewNode1不在NodeData中，可以放入结点泛型中
                {
                    NewNode1.ID = IDNum;
                    NodeData.Add(NewNode1);
                    IDNum++;
                }

                if (!InNodes(NewNode2, NodeData))//说明NewNode2不在NodeData中，可以放入结点泛型中
                {
                    NewNode2.ID = IDNum;
                    NodeData.Add(NewNode2);
                    IDNum++;
                }
            }
        }

        public bool IsOneNode(Node Node1,Node Node2)//判断两个结点是否是同一个结点
        {
            double MaxError = 0.01d;//容差为0.01
            double Dis = Math.Sqrt(Math.Pow(Node1.X - Node2.X,2) + Math.Pow(Node1.Y - Node2.Y,2));
            if (Dis > MaxError)
                return false;
            else
                return true;
        }

        public bool InNodes(Node Node1,List<Node> Nodes)//判断一个结点是否在一组节点中
        {
            foreach(Node TempNode in Nodes)
            {
                if (IsOneNode(Node1, TempNode))
                    return true;
            }
            return false;
        }

        public void CalArc(ArrayList GenData,List<Node> NodeData,List<Arc> ArcData)//生成弧段
        {
            int IDNum = 1;
            foreach (ArrayList OneLine in GenData)
            {
                Arc NewArc=new Arc();
                NewArc.ID = IDNum;
                if(OneLine.Count<=1)
                    continue;

                double[] FirstPoint = (double[])OneLine[0];//取出第一个点
                double[] EndPoint = (double[])OneLine[OneLine.Count-1];//取出第一个点
                Node NewNode1 = new Node(0, FirstPoint[0], FirstPoint[1]);
                Node NewNode2 = new Node(0, EndPoint[0], EndPoint[1]);//可以暂时不考虑ID号
                Node HeadNode = FindNode(NewNode1, NodeData);
                Node EndNode = FindNode(NewNode2, NodeData);
                if (HeadNode != null)
                    NewArc.HeadNode = HeadNode;
                else
                    continue;
                if (EndNode != null)
                    NewArc.EndNode = EndNode;
                else
                    continue;

                foreach (double[] OnePoint in OneLine)//坐标数据
                    NewArc.data.Add(new point(OnePoint[0], OnePoint[1]));

                double[] SecPoint=(double[])OneLine[1];
                double[] PointBySec=(double[])OneLine[OneLine.Count-2];
                NewArc.Direction=CalDirection(new point(FirstPoint[0],FirstPoint[1]),new point(SecPoint[0],SecPoint[1]));
                NewArc.Direction2 = CalDirection(new point(EndPoint[0], EndPoint[1]), new point(PointBySec[0], PointBySec[1]));
                IDNum++;
                ArcData.Add(NewArc);
            }
        }

        public Node FindNode(Node Node1,List<Node> Nodes)
        {
            foreach(Node OneNode in Nodes)
            {
                if (IsOneNode(Node1, OneNode))
                    return OneNode;
            }
            return null;
        }

        public double CalDirection(point pt1,point pt2)
        {
            double dx = pt2.X - pt1.X;
            double dy=pt2.Y-pt1.Y;
            if(dx==0)
            {
                if (dy >= 0)
                    return Math.PI / 2.0;
                else
                    return Math.PI * 3.0 / 2.0;
            }
            else
            {
                if (dx > 0 & dy >= 0)
                    return Math.Atan(Math.Abs(dy / dx));
                else if(dx<0&dy>=0)
                    return Math.PI - Math.Atan(Math.Abs(dy / dx));
                else if(dx<0&dy<=0)
                    return Math.PI + Math.Atan(Math.Abs(dy / dx));
                else
                    return Math.PI *2.0- Math.Atan(Math.Abs(dy / dx));
            }
        }

        public void CreatePoly(List<Node> AllNode,List<Arc> AllArc,List<Polygon> AllPoly)//构造多边形
        {
            double CurrentDirection = 0d;
            int PolyID = 1;
            foreach(Node OneNode in AllNode)//第一步：顺序取一个结点作为起始结点
            {
                CurrentDirection = -0.001d;
                int[] NextArc = FindFirstArc(OneNode, AllArc, CurrentDirection,-1);//寻找下一个弧段
                Node CurrentNode = new Node();
                Polygon NewPoly = new Polygon();

                while(NextArc!=null)//第二步找到这个结点关联的弧段集合中的本条弧段的下一条弧段
                {
                    NewPoly.ArcPart.Add(NextArc[0] * NextArc[1]);//添加弧段

                    if (AllArc[NextArc[1]-1].UseTime == 0)//设置访问次数
                        {
                            if (NextArc[0] == 1) //正方向被使用
                                AllArc[NextArc[1]-1].UseTime = 1;
                            else
                                AllArc[NextArc[1]-1].UseTime = 2;
                        }
                    else if (AllArc[NextArc[1]-1].UseTime < 3)
                        AllArc[NextArc[1]-1].UseTime = 3;

                    //设置当前Node的指向和方向
                    if (NextArc[0] == 1)//正向
                    {
                        CurrentNode = AllArc[NextArc[1]-1].EndNode;
                        CurrentDirection = AllArc[NextArc[1]-1].Direction2;//修改方向
                    }
                    else //反向
                    {
                        CurrentNode = AllArc[NextArc[1]-1].HeadNode;
                        CurrentDirection = AllArc[NextArc[1]-1].Direction;//修改方向
                    }

                    //判断是否闭合形成多边形，如果是，加入多边形泛型中
                    if (CurrentNode.ID == OneNode.ID)
                    {
                        Polygon NewPoly2 = new Polygon();
                        NewPoly2.ID = PolyID;
                        PolyID++;
                        NewPoly2.ArcPart = (ArrayList)NewPoly.ArcPart.Clone();
                        AllPoly.Add(NewPoly2);//记录多边形
                        NewPoly.ArcPart.Clear();//清空NewPoly里面的弧段信息

                        if (AllArc[NextArc[1]-1].UseTime == 3)//该弧段使用了两次，转第一步
                            break;
                        else  //转第二步
                        {
                            NextArc[0] = -1*NextArc[0];//掉头
                            if(NextArc[0]==-1)
                                CurrentDirection=AllArc[NextArc[1]-1].Direction2;
                            else
                                CurrentDirection=AllArc[NextArc[1]-1].Direction;
                            continue;
                        }
                    }
                    NextArc = FindFirstArc(CurrentNode, AllArc, CurrentDirection,NextArc[1]);//寻找下一个弧段
                }//---------while(NextArc!=null) 
            }//----------foreach(Node OneNode in AllNode) 
        }

        public int[]FindFirstArc(Node OneNode,List<Arc> AllArc,double Direction,int CurrentArcID) 
            //这个函数返回从OneNode开始的，在Direction最左边的弧段编号
        //返回一个数组，第一个数字为-1或1，-1代表负向，1代表正向；第二数字为弧段编号,若第二个数字为-1代表查找失败
        {                                                
            List<double[]> Temp = new List<double[]>();//临时数据，一层泛型中第一个元素存放Arc的ID，第二个存放Direction，第三个存放方向（-1,1)
            int[] ReturnData = new int[2] { 0, 0 };
            foreach(Arc OneArc in AllArc)//构建临时数组
            {
                if(OneArc.HeadNode.ID==OneNode.ID&OneArc.ID!=CurrentArcID)//必须是当前Arc以外的
                {
                    if(OneArc.UseTime<3&OneArc.UseTime!=1)//和起始点相同此时需要保证该弧段正方向没有使用过
                    {
                        double[] OneTemp = new double[3] { 0d, 0d, 0d };
                        OneTemp[2] = 1d;//正方向
                        OneTemp[1] = OneArc.Direction;
                        OneTemp[0] = OneArc.ID;
                        Temp.Add(OneTemp);
                    }
                    continue;
                }

                if (OneArc.EndNode.ID == OneNode.ID&OneArc.ID!=CurrentArcID)
                {
                    if (OneArc.UseTime<3&OneArc.UseTime!=2)//和终点相同此时需要保证该弧段负方向没有使用过
                    {
                        double[] OneTemp = new double[3] { 0d, 0d, 0d };
                        OneTemp[2] = -1d;//负方向
                        OneTemp[1] = OneArc.Direction2;
                        OneTemp[0] = OneArc.ID;
                        Temp.Add(OneTemp);
                    }
                }
            }//--------- foreach(Arc OneArc in AllArc)

            if (Temp.Count == 0)//没有找到
                return null;

            bool flag = false;
            double MinDirection = 0d;
            foreach(double[] OneTemp in Temp)
            {
                if(flag)
                {
                    if(OneTemp[1]>Direction&OneTemp[1]<MinDirection)
                    {
                        MinDirection = OneTemp[1];
                        ReturnData[0] = (int)OneTemp[2];//正负方向
                        ReturnData[1] = (int)OneTemp[0];//ID号
                    }
                }
                else
                {
                    if(OneTemp[1]>Direction)
                    {
                        MinDirection = OneTemp[1];
                        ReturnData[0] = (int)OneTemp[2];//正负方向
                        ReturnData[1] = (int)OneTemp[0];//ID号
                        flag = true;
                    }
                }
            }//--- foreach(double[] OneTemp in Temp)

            MinDirection=double.MaxValue;
            if(!flag)//说明Direction的方向最大，则需要找到Temp里面方向最小的
            {
                 foreach(double[] OneTemp in Temp)
                 {
                     if(MinDirection>OneTemp[1])
                     {
                         MinDirection = OneTemp[1];
                         ReturnData[0] = (int)OneTemp[2];//正负方向
                         ReturnData[1] = (int)OneTemp[0];//ID号
                     }
                 }
            }
            return ReturnData;
        }

        public double CalArea(List<Arc> AllArc,Polygon OnePolygon)//计算一个Polygon的面积
        {
            List<point> AllPt = new List<point>();//存放所有数据点
            double Area = 0d;
            foreach(int One in OnePolygon.ArcPart)
            {
                if(One>0)//说明该Arc是正方向 
                {
                    foreach (point pt in AllArc[One - 1].data)
                        AllPt.Add(pt);
                }
                else if(One<0)//说明该Arc是负方向
                {
                    int One2 = Math.Abs(One);
                    for(int i=AllArc[One2-1].data.Count-1;i>=0;i--)//没有使用Reverse函数是因为该函数会对原始数据产生影响
                    {
                        point Pt = new point(AllArc[One2-1].data[i].X,AllArc[One2-1].data[i].Y);
                        AllPt.Add(Pt);
                    }
                }
            }

            for(int i=0;i<AllPt.Count-1;i++)
            {
                point pt1 =AllPt[i];
                point pt2=AllPt[i+1];
                Area += 0.5 * (pt2.Y + pt1.Y) * (pt2.X - pt1.X);
            }
            return Area;
        }

        public bool ContainPoly(Polygon Poly1,Polygon Poly2,List<Arc>AllArc)//判断Poly1是否在Poly2中,返回true代表Poly2包含Poly1
        {
            List<point> Poly1Pts = ReturnPts(AllArc, Poly1.ArcPart);
            List<point> Poly2Pts = ReturnPts(AllArc, Poly2.ArcPart);
            foreach(point pt in Poly1Pts)
                if (!PnPoly(pt, Poly2Pts))
                    return false;

            return true;
        }

        public bool PnPoly(point pt,List<point> AllPt)//判断点pt是否在AllPt中
        {
            bool flag = false;
            int i = 0, j = 0;
            int Num=AllPt.Count;
            for(i=0,j=Num-1;i<Num;j=i,i++)
            {
                if((((AllPt[i].Y<=pt.Y)&(pt.Y<AllPt[j].Y))|
                    ((AllPt[j].Y<=pt.Y)&(pt.Y<AllPt[i].Y)))
                    &(pt.X<(AllPt[j].X-AllPt[i].X)*(pt.Y-AllPt[i].Y)/(AllPt[j].Y-AllPt[i].Y)+AllPt[i].X))
                    flag=!flag;
            }
            return flag;
        }

        public void IsLand(List<Polygon> AllPolygon,List<Arc> AllArc)
        {
            List<Polygon> PosPoly = new List<Polygon>();//面积为正的多边形的集合
            List<Polygon> NegPoly = new List<Polygon>();//面积为负的多边形集合

            foreach (Polygon Poly in AllPolygon)//计算多边形面积
            {
                double Area = CalArea(AllArc, Poly);
                if(Area>0)//面积为正
                {
                    Poly.ArcPartArea = Area;
                    Poly.Area=Area;
                    PosPoly.Add(Poly);
                }
                else
                {
                    Poly.ArcPartArea = Area;
                    NegPoly.Add(Poly);
                }
            }

            List<Polygon> DeletePoly = new List<Polygon>();//要删除的多边形集合
            if(NegPoly.Count>=2)
                for (int i = 0; i < NegPoly.Count;i++ )
                {
                    Polygon TmpPoly1 = NegPoly[i] as Polygon;
                    foreach (Polygon TmpPoly2 in PosPoly)
                    {
                        if (TmpPoly2.Area <= Math.Abs(TmpPoly1.Area))
                            continue;
                        else if (ContainPoly(TmpPoly1, TmpPoly2, AllArc))//说明该面积为正的多边形包含面积为负的多边形
                        {
                            TmpPoly2.ArcPart2.Add(TmpPoly1.ArcPart);
                            TmpPoly2.Area = TmpPoly2.Area + TmpPoly1.ArcPartArea;
                            TmpPoly2.ArcPartArea2 = TmpPoly2.ArcPartArea2 + TmpPoly1.ArcPartArea;
                            DeletePoly.Add(TmpPoly1);
                        }
                    }
                  }
            foreach (Polygon TmpPoly in DeletePoly)//删除NegPloy中和DeletePoly相同的多边形
                NegPoly.Remove(TmpPoly);

            AllPolygon.Clear();
            foreach (Polygon TmpPoly in PosPoly)
                AllPolygon.Add(TmpPoly);
            foreach (Polygon TmpPoly in NegPoly)
                AllPolygon.Add(TmpPoly);
        }

        public List<point> ReturnPts(List<Arc> AllArc,ArrayList ArcPart)//返回由ArcPart组成的点集合
        {
            List<point> AllPt = new List<point>();//存放所有数据点
            foreach (int One in ArcPart)
            {
                if (One > 0)//说明该Arc是正方向 
                {
                    foreach (point pt in AllArc[One - 1].data)
                        AllPt.Add(pt);
                }
                else if (One < 0)//说明该Arc是负方向
                {
                    int One2 = Math.Abs(One);
                    for (int i = AllArc[One2 - 1].data.Count - 1; i >= 0; i--)//没有使用Reverse函数是因为该函数会对原始数据产生影响
                    {
                        point Pt = new point(AllArc[One2 - 1].data[i].X, AllArc[One2 - 1].data[i].Y);
                        AllPt.Add(Pt);
                    }
                }
            }
            return AllPt;
        }
    }
}
