using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace GraphTheory
{
    public class NewPrimMST
    {
        AdjacencyMatrix matrix;

        public NewPrimMST(AdjacencyMatrix pmatrix)
        {
            matrix = pmatrix;
        }

        private int MaxKey(int[] key, bool[] mstSet)
        {
            int max = int.MinValue, max_index = -1;

            for (int v = 0; v < matrix.VertexCount; v++)
            {
                if (mstSet[v] == false && key[v] > max)
                {
                    max = key[v];
                    max_index = v;
                }
            }

            return max_index;
        }

        public void PrimMaxMST(int startVertex)
        {
            int[] parent = new int[matrix.VertexCount];
            int[] key = new int[matrix.VertexCount];
            bool[] mstSet = new bool[matrix.VertexCount];

            for (int i = 0; i < matrix.VertexCount; i++)
            {
                key[i] = int.MinValue;
                mstSet[i] = false;
            }

            key[startVertex] = int.MaxValue; // Khởi tạo key của đỉnh bắt đầu là max để chọn đỉnh này đầu tiên

            parent[startVertex] = -1;

            for (int count = 0; count < matrix.VertexCount - 1; count++)
            {
                int u = MaxKey(key, mstSet);

                mstSet[u] = true;

                for (int v = 0; v < matrix.VertexCount; v++)
                {
                    if (matrix.Data[u, v] != 0 && mstSet[v] == false && matrix.Data[u, v] > key[v])
                    {
                        parent[v] = u;
                        key[v] = matrix.Data[u, v];
                    }
                }
            }

            Console.WriteLine("Giải thuật Prim");
            Console.WriteLine("Tập cạnh của cây khung");
            int Max = 0;
            for (int i = 0; i < matrix.VertexCount; i++)
            {
                if (parent[i] != -1)
                {
                    Console.WriteLine(parent[i] + " - " + i + "    " + matrix.Data[i, parent[i]]);
                }
            }
            for (int i = 0; i < key.Length; i++)
            {
                if (parent[i] != -1)
                {
                    Max += key[i];
                }
                 
            }

            Console.WriteLine($"Trọng số của cây khung: {Max}");
            
        }
    }
    
    public class KruskalMST
    {

        class Edge : IComparable<Edge>
        {
            public int Source, Destination, Weight;

            public int CompareTo(Edge compareEdge)
            {
                return compareEdge.Weight - this.Weight; // Đảo ngược thứ tự sắp xếp để tìm cây khung lớn nhất
            }
        };

        int V, E;
        Edge[] edges;

        public KruskalMST(AdjacencyMatrix pmatrix)
        {
            V = pmatrix.VertexCount;
            E = 0;
            // Dem so canh cua do thi
            for (int i = 0; i < pmatrix.Data.GetLength(0); i++)
            {
                for (int j = 0; j < pmatrix.Data.GetLength(1); j++)
                {
                    if (pmatrix.Data[i, j] != 0)
                    {
                        E++;
                    }
                }
            }
            edges = new Edge[E];
            //Khoi tao tap canh cua do thi
            int tmp = 0;
            for (int i = 0; i < pmatrix.Data.GetLength(0); i++)
            {
                for (int j = 0; j < pmatrix.Data.GetLength(1); j++)
                {
                    if (pmatrix.Data[i, j] != 0)
                    {
                        edges[tmp] = new Edge();
                        edges[tmp].Source = i;
                        edges[tmp].Destination = j;
                        edges[tmp].Weight = pmatrix.Data[i, j];
                        tmp++;
                    }
                }
            }
        }

        public KruskalMST(int v, int e)
        {
            V = v;
            E = e;
            edges = new Edge[E];
            for (int i = 0; i < e; ++i)
                edges[i] = new Edge();
        }

        int find(int[] parent, int i)
        {
            if (parent[i] == -1)
                return i;
            return find(parent, parent[i]);
        }

        void Union(int[] parent, int x, int y)
        {
            int xset = find(parent, x);
            int yset = find(parent, y);
            parent[xset] = yset;
        }

        public void KruskalMSTAl()
        {
            Edge[] result = new Edge[V];
            int e = 0;
            int i = 0;

            Array.Sort(edges);

            int[] parent = new int[V];
            for (int v = 0; v < V; ++v)
                parent[v] = -1;

            while (e < V - 1 && i < E)
            {
                Edge nextEdge = edges[i++];

                int x = find(parent, nextEdge.Source);
                int y = find(parent, nextEdge.Destination);

                if (x != y)
                {
                    result[e++] = nextEdge;
                    Union(parent, x, y);
                }
            }

            Console.WriteLine("Giải thuật Kruskal");
            Console.WriteLine("Tập cạnh của cây khung");
            int Max = 0;
            for (i = 0; i < e; ++i)
            {
                Console.WriteLine(result[i].Source + " - " + result[i].Destination + "\t" + result[i].Weight);
                Max += result[i].Weight;
            }
            Console.WriteLine($"Trọng số của cây khung: {Max}");
        }
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

        public bool Validate()
        {
            // Kiểm tra đồ thị có vô hướng hay không
            // Đồ thị có hướng -> return
            if (!Validation.IsUndirectedGraph(adjMatrix)) {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi input khong phai do thi vo huong");
                return false;
            };

            // Kiem tra do thi vo huong lien thong
            if (!Validation.IsBiConnectedGraph(adjMatrix)) {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi input khong phai la do thi lien thong");
                return false;
            };
            return true;
        }

        public void Prim()
        {
            NewPrimMST PrimObject = new NewPrimMST(adjMatrix);
            Console.Write("Nhap dinh bat dau giai thuat prim:");
            int Source = int.Parse(Console.ReadLine());
            PrimObject.PrimMaxMST(Source);
        }

        //Giải thuật Krukal tìm cây khung lớn nhất
        public void KruskalAL()
        {

            KruskalMST KruskalObject = new KruskalMST(adjMatrix);
            KruskalObject.KruskalMSTAl();
        }
    }
}


