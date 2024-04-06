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
            List<EDGE> edgesResult = new ();

            bool[] marked = new bool[adjMatrix.VertexCount];
            for (int v = 0; v < adjMatrix.VertexCount; v++)
            {
                marked[v] = false;
            }
            marked[source] = true;
            //Thực hiện giải thuật
            while (nT < adjMatrix.VertexCount - 1)
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
                                }
                            }
                        }
                    }
                    edgesResult.Add(edgeMax);
                    marked[w] = true;
                }
            }
            //Tính trọng số của cây khung
            int Max = 0;
            //In cây khung từ T
            Console.WriteLine("Giải thuật Prim");
            Console.WriteLine("Tập cạnh của cây khung");
            for (int i = 0; i < edgesResult.Count; i++)
            {
                Console.WriteLine(edgesResult[i].V + " - " + edgesResult[i].W + ": " + adjMatrix.Data[edgesResult[i].V, edgesResult[i].W]);
                Max += edgesResult[i].Weight;
            }
            Console.WriteLine($"Trọng số của cây khung: {Max}");
        }

        private List<EDGE> InitListEdge(AdjacencyMatrix adjacencyMatrix)
        {
            List<EDGE> listEdges = new();
            for (int i = 0; i < adjMatrix.VertexCount; i++)
            {
                for (int j = 0; j < adjMatrix.VertexCount; j++)
                {
                    if (adjMatrix.Data[i, j] > 0)
                    {
                        EDGE edge = new EDGE ();
                        edge.V = i;
                        edge.W = j;
                        edge.Weight = adjacencyMatrix.Data[i, j];
                        listEdges.Add(edge);
                    }
                }
            }
            return listEdges;
        }

        //Sắp xếp các cạnh giảm dần theo thứ tự về trọng số
        private void SortListEdge(List<EDGE> listEdges)
        {
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

        private int Find(int x, List<int> label)
        {
            while (label[x] != 1)
            {
                x = label[x];
            }
            return x;
        }

        //Kiểm tra cạnh có tạo thành chu trình không
        private void Union(int v, int w, List<int> label)
        {
            if (label[v] != label[w])
            {
                if (label[v] > label[w])
                {
                    label[w] += label[v];
                    label[v] = w;
                }
                else
                {
                    label[v] += label[w];
                    label[w] = v;
                }
            }
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

            List<EDGE> edgesResult = new ();

            //Khởi tạo mảng chứa mọi cạnh của đồ thị
            List<EDGE> listEdges = new ();

            List<int> label = new();
            for (int i = 0; i < label.Count; i++)
            {
                label[i] = i;
            }
            InitListEdge(adjMatrix);
            SortListEdge(listEdges);
            for (int eMaxIndex = 0; eMaxIndex < listEdges.Count; eMaxIndex++)
            {
                int v = Find(listEdges[eMaxIndex].V, label);
                int w = Find(listEdges[eMaxIndex].W, label);

                if (v != w)
                {
                    edgesResult.Add(listEdges[eMaxIndex]);
                }
                Union(v, w, label);
            }
            
            //Tính trọng số của cây khung
            int S = 0;
            for (int i = 0; i < edgesResult.Count; i++)
            {
                var weight = edgesResult[i].Weight;
                S += weight;
            }

            //In cây khung từ T
            Console.WriteLine("Giải thuật Kruskal");
            Console.WriteLine("Tập cạnh của cây khung");
            for (int i = 0; i < edgesResult.Count; i++)
            {
                Console.WriteLine(edgesResult[i].V + " - " + edgesResult[i].W + ": " + adjMatrix.Data[edgesResult[i].V, edgesResult[i].W]);
            }
            Console.WriteLine($"Trọng số của cây khung: {S}");
        }
    }
}

