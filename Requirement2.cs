using System;
using System.Linq;
using System.Collections.Generic;

namespace GraphTheory
{
    public class Requirement_2
    {
        private readonly AdjacencyMatrix adjMatrix;
        private readonly AjacencyList adjList;

        public Requirement_2(string filePath)
        {
            adjList = new AjacencyList(filePath);
            adjMatrix = new AdjacencyMatrix(adjList);
        }

        //Kiểm tra đồ thị có cạnh khuyên hay không
        public static bool isGraphHasNoLoop(AdjacencyMatrix adjacencyMatrix)
        {
            for (int i = 0; i < adjacencyMatrix.VertexCount && adjacencyMatrix.Data[i, i] == 0; i++)
                if (i < adjacencyMatrix.VertexCount)
                    return false;
            return true;
        }

        //Kiểm tra đồ thị có cạnh bội hay không
        public static bool IsMultiGraph(AjacencyList ajacencyList)
        {
            for (int i = 0; i < ajacencyList.VertexCount; i++)
            {
                var edges = ajacencyList.GetEdges(i);

                // Kiểm tra từng đỉnh cuối trong list các cạnh xem có bị lặp lại hay không?
                for (int j = 0; j < edges.Count; j++)
                {
                    for (int k = 0; k < edges.Count; k++)
                    {
                        if (edges[j].Destination == edges[k].Destination)
                            return true;
                    }
                }
            }
            return false;
        }

        //Kiểm tra đồ thị vô hướng
        public static bool IsUndirectGraph(AdjacencyMatrix adjacencyMatrix)
        {
            bool isSymmectric = true;

            for (int i = 0; i < adjacencyMatrix.VertexCount; i++)
            {
                for (int j = i + 1; j < adjacencyMatrix.VertexCount && adjacencyMatrix.Data[i, j] == adjacencyMatrix.Data[j, i]; j++)
                    if (j < adjacencyMatrix.VertexCount)
                        isSymmectric = false;
            }
            return true;
        }

        //Kiểm tra đồ thị liên thông hay không liên thông
        public static int Connected(AdjacencyMatrix adjacencyMatrix)
        {
            bool[] marked = new bool[adjacencyMatrix.VertexCount];
            bool connected = true;
            int Dem = 0;

            //Khởi tạo mọi đỉnh chưa đánh dấu
            for (int i = 0; i < adjacencyMatrix.VertexCount; i++)
            {
                marked[i] = false;
                marked[0] = true;
                Dem++;
            }
            do
            {
                connected = true;
                for (int i = 0; i < adjacencyMatrix.VertexCount; i++)
                {
                    if (marked[i] == true)
                    {
                        for (int j = 0; j < adjacencyMatrix.VertexCount; j++)
                        {
                            if (marked[j] == false && adjacencyMatrix.Data[i, j] > 0)
                            {
                                marked[j] = true;
                                connected = true;
                                Dem++;
                                if (Dem == adjacencyMatrix.VertexCount)
                                    return 1;
                            }
                        }
                    }
                }
            }
            while (connected == false);
            return 0;
        }


        //Kiểm tra đồ thị liên thông mạnh, liên thông một phần, liên thông yếu
        public static int CheckConnected(AdjacencyMatrix adjacencyMatrix)
        {
            int a = Connected(adjacencyMatrix);

            if (a == 0)
            {
                Console.WriteLine("Đồ thị không liên thông");
                return 0;
            }
            else
            {
                bool strongly = true;
                //Kiểm tra đồ thị liên thông mạnh hay không?
                for (int i = 0; i < adjacencyMatrix.VertexCount; i++)
                {
                    for (int j = 0; j < adjacencyMatrix.VertexCount; j++)
                    {
                        if (adjacencyMatrix.Data[i, j] != 1)
                        {
                            strongly = false;
                            break;
                        }
                    }
                    if (!strongly)
                    {
                        break;
                    }
                }

                //Kiểm tra đồ thị liên thông một phần
                bool uppertri = true;
                for (int i = 0; i < adjacencyMatrix.VertexCount; i++)
                {
                    for (int j = 0; j < adjacencyMatrix.VertexCount; j++)
                    {
                        if ((i > j && adjacencyMatrix.Data[i, j] == 0) || (i < j && adjacencyMatrix.Data[i, j] == 0))
                        {
                            uppertri = false;
                            break;
                        }
                    }
                    if (!uppertri)
                    {
                        break;
                    }
                }

                if (strongly)
                {
                    Console.WriteLine("Đồ thị liên thông mạnh");
                    return 0;
                }

                if (uppertri)
                {
                    Console.WriteLine("Đồ thị liên thông một phần");
                    return 0;
                }
                else
                {
                    Console.WriteLine("Đồ thị liên thông yếu");
                    return 0;
                }
            }
        }

        //Duyệt đồ thị bằng BFS
        public void BFS(AdjacencyMatrix adj, int i, int solt, int[] chuaxet, int[] Queue)
        {
            int u;
            int src, des;
            src = 1; des = 1;

            Queue[des] = i;
            chuaxet[i] = solt;
            while (src <= des)
            {
                u = Queue[src];
                src = src + 1;
                for (int j = 1; j < adj.VertexCount; j++)
                {
                    if (adj.Data[u, j] == 1 && chuaxet[j] == 0)
                    {
                        des = des + 1;
                        Queue[des] = j;
                        chuaxet[j] = solt;
                    }
                }
            }

        }

        //Đếm thành phần liên thông mạnh
        public static void ConnectedComponent(AdjacencyMatrix adj)
        {
            int[] chuaxet = new int[adj.VertexCount];
            int[] Queue = new int[adj.VertexCount];
            int i;
            int solt = 0;

            //khởi tạo giá trị ban đầu cho mảng chuaxet
            for (i = 1; i < adj.VertexCount; i++)
            {
                chuaxet[i] = 0;
            }

            for (i = 1; i < adj.VertexCount; i++)
            {
                if (chuaxet[i] == 0)
                {
                    solt = solt + 1;
                    BFS(adj, i, solt, chuaxet, Queue);
                }
            }

            for (i = 1; i < solt; i++)
            {
                Console.Write($"Thành phần liên thông mạnh {i}: ");
                Console.Write("ertw");

            }

        }


    }
}


