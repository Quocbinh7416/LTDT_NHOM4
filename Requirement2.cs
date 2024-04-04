using System;
using System.Collections.Generic;

namespace GraphTheory
{
    class Requirement_2
    {
        private readonly AdjacencyList adjList;
        private readonly AdjacencyMatrix adjMatrix;

        public Requirement_2(string filePath)
        {
            adjList = new AdjacencyList(filePath);
            adjMatrix = new AdjacencyMatrix(adjList);
        }

        private int BfsUtill(int u, bool[] visited)
        {
            int counter = 0;
            Queue<int> queue = new Queue<int>();
            visited[u] = true;
            queue.Enqueue(u);
            while (queue.Count != 0)
            {
                int curr = queue.Dequeue();
                counter++;
                for (int i = 0; i < adjMatrix.VertexCount; i++)
                {
                    if (visited[i] == false && adjMatrix.Data[curr, i] > 0)
                    {
                        queue.Enqueue(i);
                        visited[i] = true;
                    }
                }
            }
            return counter;
        }

        //Kiểm tra đồ thị liên thông mạnh, liên thông một phần, liên thông yếu
        private void CheckConnected()
        {
            bool a = Validation.Connected(adjMatrix);
            if (a == false)
            {
                Console.WriteLine("Đồ thị không liên thông");
            }
            else
            {
                bool[] visited = new bool[adjMatrix.VertexCount];
                int Dem = 0;
                for (int i = 0; i < adjMatrix.VertexCount; i++)
                {
                    if (BfsUtill(i, visited) == adjMatrix.VertexCount)
                    {
                        Dem++;
                    }
                    for (i = 0; i < adjMatrix.VertexCount; i++)
                    {
                        visited[i] = false;
                    }
                }

                if (Dem == adjMatrix.VertexCount)
                {
                    Console.WriteLine("Đồ thị liên thông mạnh");
                }

                if (Dem > 0 && Dem < adjMatrix.VertexCount)
                {
                    Console.WriteLine("Đồ thị liên thông một phần");
                }
                else
                {
                    Console.WriteLine("Đồ thị liên thông yếu");
                }
            }
        }

        private void BFS(int u, int solt, int[] visited)
        {
            Queue<int> queue = new Queue<int>();
            visited[u] = solt;
            queue.Enqueue(u);
            while (queue.Count != 0)
            {
                int curr = queue.Dequeue();
                for (int i = 0; i < adjMatrix.VertexCount; i++)
                {
                    if (visited[i] == 0 && adjMatrix.Data[curr, i] > 0)
                    {
                        queue.Enqueue(i);
                        visited[i] = solt;
                    }
                }
            }
        }

        //Đếm thành phần liên thông mạnh
        private void ConnectedComponent()
        {
            int i;
            int solt = 0;
            int[] marked = new int[adjList.VertexCount];

            //khởi tạo giá trị ban đầu cho mảng marked
            for (i = 1; i < adjList.VertexCount; i++)
            {
                marked[i] = 0;
            }

            //Đếm số thành phần liên thông 
            for (i = 1; i < adjList.VertexCount; i++)
            {
                if (marked[i] == 1)
                {
                    solt = solt + 1;
                    BFS(i, solt, marked);
                }
            }

            //In thành phần liên thông mạnh 
            for (i = 1; i < solt; i++)
            {
                Console.Write($"Thành phần liên thông mạnh {i}: ");
                for (int j = 1; j < adjList.VertexCount; j++)
                {
                    if (marked[j] == 1)
                    {
                        Console.Write(j + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        public void Connection()
        {
            // Kiểm tra điều kiện của đồ thị
            bool Undirected = Validation.IsUndirectedGraph(adjMatrix);
            bool Loop = Validation.IsGraphHasLoops(adjMatrix);
            bool Multi = Validation.IsMultiGraph(adjList);

            if (Undirected == false && Loop == false && Multi == false)
            {
                CheckConnected();
                ConnectedComponent();
            }
            else
            {
                Console.WriteLine("Đồ thị không thoả yêu cầu của điều kiện");
            }
        }

    }
}
