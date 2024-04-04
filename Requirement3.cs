using System;

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
            bool Undirected = Validation.IsUndirectedGraph(adjMatrix);
            bool Connected = Validation.Connected(adjMatrix);
            // Kiểm tra đồ thị liên thông vô hướng
            if (Undirected == false && Connected == true)
            {
                //Khởi tạo các biến cần thiết
                EDGE[] T = new EDGE[adjMatrix.VertexCount - 1];
                int nT = 0;
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
                    int nMaxWeight = -1;

                    //Duyệt các đỉnh thoả điều kiện chưa xét
                    for (int w = 0; w < adjMatrix.VertexCount; w++)
                    {
                        if (marked[w] == false)
                        {
                            //tìm v bất kỳ đã xét và có cạnh nối trực tiếp v - W
                            for (int v = 0; v < adjMatrix.VertexCount; v++)
                            {
                                if (marked[v] == true && adjMatrix.Data[w, v] > 0)
                                {
                                    if (nMaxWeight < 0 || adjMatrix.Data[w, v] > nMaxWeight)
                                    {
                                        edgeMax.V = v;
                                        edgeMax.W = w;
                                        nMaxWeight = adjMatrix.Data[v, w];
                                    }
                                }
                            }
                        }
                        T[nT++] = edgeMax;
                        marked[w] = true;
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
                for (int i = 1; i < adjMatrix.VertexCount; i++)
                {
                    Console.WriteLine(T[i].V + " - " + T[i].W + adjMatrix.Data[T[i].V, T[i].W]);
                }
                Console.WriteLine($"Trọng số của cây khung: {S}");
            }
            else
            {
                Console.WriteLine("Do thi khong thoa dieu kien yeu cau");
            }
        }

        //Sắp xếp các cạnh giảm dần theo thứ tự về trọng số
        private void SortListEdge(EDGE[] listEdges)
        {
            int nEdges = listEdges.Length;
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

        //Khởi tạo mảng chứa mọi cạnh của đồ thị
        private int InitListEdge(EDGE[] listEdges, int nEdgeCount)
        {

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
            return nEdgeCount;
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

        //Giải thuật Krukal tìm cây khung lớn nhất
        public void KruskalAL()
        {
            bool Undirected = Validation.IsUndirectedGraph(adjMatrix);
            bool Connected = Validation.Connected(adjMatrix);
            // Kiểm tra đồ thị liên thông vô hướng
            if (Undirected == false && Connected == true)
            {
                EDGE[] T = new EDGE[adjMatrix.VertexCount - 1];
                int nT = 0;

                int nlistEdge = adjMatrix.VertexCount * (adjMatrix.VertexCount - 1);
                EDGE[] listEdges = new EDGE[nlistEdge];
                int nEdgeCount = 0;

                int[] label = new int[adjMatrix.VertexCount];
                for (int i = 0; i < label.Length; i++)
                {
                    label[i] = i;
                }

                int eMaxIndex = 0;
                nEdgeCount = InitListEdge(listEdges, nEdgeCount);

                SortListEdge(listEdges);

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
                        T[nT++] = MaxEgde;
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
            else
            {
                Console.WriteLine("Do thi khong thoa dieu kien yeu cau");
            }

        }
    }
}

