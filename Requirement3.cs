using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace GraphTheory
{
    public class EDGE
    {
        public int V { get; set; }
        public int W { get; set; }
        public int Weight { get; set; }
    }

    public class Requirement_3
    {
        private readonly AdjacencyList adjList;
        private readonly AdjacencyMatrix adjMatrix;

        public Requirement_3(string filePath)
        {
            adjList = new AdjacencyList(filePath);
            adjMatrix = new AdjacencyMatrix(adjList);
        }

        public void Prim(int source)
        {
            // Kiểm tra đồ thị có vô hướng hay không
            // Đồ thị có hướng -> return
            if (!Validation.IsUndirectedGraph(adjMatrix))
            {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi co huong");
                return;
            };

            // Kiem tra do thi vo huong lien thong
            if (!Validation.IsBiConnectedGraph(adjMatrix))
            {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi khong lien thong");
                return;
            };

            //Khởi tạo các biến cần thiết
            int nT = adjMatrix.VertexCount - 1;
            EDGE[] T = new EDGE[nT];

            bool[] marked = new bool[adjMatrix.VertexCount];
            for (int v = 0; v < adjMatrix.VertexCount; v++)
            {
                marked[v] = false;
            }
            marked[source] = true;
            //Thực hiện giải thuật
            for (int i = 0; i < nT; i++)
            {
                EDGE edgeMax = new EDGE();
                int nMaxWeight = 0;

                for (int w = 0; w < adjMatrix.VertexCount; w++)
                {
                    if (marked[w] == false)
                    {
                        for (int v = 0; v < adjMatrix.VertexCount; v++)
                        {
                            if (marked[v] == true && adjMatrix.Data[v, w] > 0)
                            {
                                if (nMaxWeight == 0 || adjMatrix.Data[v, w] > nMaxWeight)
                                {
                                    edgeMax.V = v;
                                    edgeMax.W = w;
                                    edgeMax.Weight = adjMatrix.Data[v, w];
                                    nMaxWeight = adjMatrix.Data[v, w];
                                    marked[w] = true;
                                }
                            }
                        }
                    }
                    T[i] = edgeMax;
                }
            }
            //Tính trọng số của cây khung
            int S = 0;
            for (int i = 0; i < T.Length; i++)
            {
                S = S + T[i].Weight;
            }

            //In cây khung từ T
            Console.WriteLine("Giải thuật Prim");
            Console.WriteLine("Tập cạnh của cây khung");
            for (int i = 0; i < adjMatrix.VertexCount - 1; i++)
            {
                Console.WriteLine(T[i].V + " - " + T[i].W + ": " + adjMatrix.Data[T[i].V, T[i].W]);
            }
            Console.WriteLine($"Trọng số của cây khung: {S}");
        }

        private List<EDGE> InitListEdge(AdjacencyMatrix adjacencyMatrix)
        {
            List<EDGE> listEdges = new();
            int nEdgeCount = 0;
            for (int i = 0; i < adjMatrix.VertexCount; i++)
            {
                for (int j = 0; j < adjMatrix.VertexCount; j++)
                {
                    if (adjMatrix.Data[i, j] > 0)
                    {
                        listEdges[nEdgeCount].V = i;
                        listEdges[nEdgeCount].W = j;
                        listEdges[nEdgeCount].Weight = adjMatrix.Data[i, j];
                        nEdgeCount++;
                    }
                }
            }
            return listEdges;
        }

        //Sắp xếp các cạnh giảm dần theo thứ tự về trọng số
        private void SortListEdge()
        {
            List<EDGE> listEdges = new();
            int nEdges = listEdges.Count;
            for (int i = 0; i < nEdges - 1; i++)
            {
                for (int j = i + 1; j < nEdges; j++)
                {
                    if (listEdges[j].Weight > listEdges[i].Weight)
                    {
                        EDGE E = listEdges[i];
                        listEdges[i] = listEdges[j];
                        listEdges[j] = E;
                    }
                }
            }
        }

        //Kiểm tra cạnh có tạo thành chu trình không
        private bool IsCricle(int Index, int[] label, EDGE[] listEdges)
        {
            int v = listEdges[Index].V;
            int w = listEdges[Index].W;
            int lab1;
            int lab2;
            if (label[v] == label[w])
            {
                return true;
            }
            else
            {
                if (label[v] > label[w])
                {
                    lab1 = label[w];
                    lab2 = label[v];
                }
                else
                {
                    lab1 = label[v];
                    lab2 = label[w];
                }

                for (int i = 0; i < label.Length; i++)
                {
                    if (label[i] == lab2)
                    {
                        i = lab1;
                    }
                }
            }
            return false;
        }

        // Tinh so canh cua do thi
        private int NumberListEdge(AdjacencyMatrix adjacencyMatrix)
        {
            int Count = 0;
            for (int i = 0; i < adjMatrix.VertexCount; i++)
            {
                for (int j = 0; j < adjMatrix.VertexCount; j++)
                {
                    if (adjMatrix.Data[i, j] > 0)
                    {
                        Count++;
                    }
                }
            }
            return Count;
        }


        //Giải thuật Krukal tìm cây khung lớn nhất
        public void KruskalAL()
        {
            // Kiểm tra đồ thị có vô hướng hay không
            // Đồ thị có hướng -> return
            if (!Validation.IsUndirectedGraph(adjMatrix))
            {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi co huong");
                return;
            };

            // Kiem tra do thi vo huong lien thong
            if (!Validation.IsBiConnectedGraph(adjMatrix))
            {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi khong lien thong");
                return;
            };


            EDGE[] T = new EDGE[adjMatrix.VertexCount - 1];
            int nT = 0;

            //Khởi tạo mảng chứa mọi cạnh của đồ thị
            int nEdgeCount = 0;
            int nlistEdge = NumberListEdge(adjMatrix);
            EDGE[] listEdges = new EDGE[nEdgeCount];


            int[] label = new int[adjMatrix.VertexCount];
            for (int i = 0; i < label.Length; i++)
            {
                label[i] = i;
            }
            InitListEdge(adjMatrix);
            SortListEdge();

            int eMaxIndex = 0;
            while (nT < adjMatrix.VertexCount - 1)
            {
                EDGE MaxEgde = new EDGE();
                if (eMaxIndex < nEdgeCount)
                {
                    if (IsCricle(eMaxIndex, label, listEdges) == false)
                    {
                        MaxEgde.V = listEdges[eMaxIndex].V;
                        MaxEgde.W = listEdges[eMaxIndex].W;
                        MaxEgde.Weight = listEdges[eMaxIndex].Weight;
                    }
                    T[nT] = MaxEgde;
                    eMaxIndex++;
                }
                else
                {
                    break;
                }
            }
            //Tính trọng số của cây khung
            int S = 0;
            for (int i = 0; i < T.Length; i++)
            {
                S = S + T[i].Weight;
            }

            Console.WriteLine("Giải thuật Kruskal");
            Console.WriteLine("Tập cạnh của cây khung");
            for (int i = 1; i < adjMatrix.VertexCount; i++)
            {
                Console.WriteLine(T[i].V + " - " + T[i].W + ": " + adjMatrix.Data[T[i].V, T[i].W]);
            }
            Console.WriteLine($"Trọng số của cây khung: {S}");
        }
    }
}

