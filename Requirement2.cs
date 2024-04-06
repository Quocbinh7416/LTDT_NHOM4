using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;

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
            Stack<int> stack = new();
            stack.Push(u);
            visited[u] = true;
            // Duyệt đỉnh
            while (stack.Count > 0)
            {
                int cur = stack.Pop();
                counter++;
                for (int i = 0; i < adjMatrix.VertexCount; i++)
                {
                    if (adjMatrix.Data[cur, i] != 0 && visited[i] == false)
                    {
                        stack.Push(i);
                        visited[i] = true;
                    }
                }
            }
            return counter;
        }

        //Kiểm tra đồ thị liên thông mạnh, liên thông một phần, liên thông yếu
        private void CheckConnected()
        {
            bool[] visited = new bool[adjMatrix.VertexCount];
            for (int i = 0; i < visited.Length; i++)
            {
                visited[i] = false;
            }

            bool a = Validation.Connected(adjMatrix);
            int Dem = 0;
            if (a == false)
            {
                Console.WriteLine("Đồ thị không liên thông");
            }
            else
            {
                for (int i = 0; i < adjMatrix.VertexCount; i++)
                {
                    if (BfsUtill(i, visited) == adjMatrix.VertexCount)
                    {
                        Dem++;
                    }
                    for (int j = 0; j < visited.Length; j++)
                    {
                        visited[j] = false;
                    }
                }

                if (Dem > 0)
                {
                    if (Dem == adjMatrix.VertexCount)
                    {
                        Console.WriteLine("Đồ thị liên thông mạnh");
                    }
                    if (Dem < adjMatrix.VertexCount)
                    {
                        Console.WriteLine("Đồ thị liên thông một phần");
                    }
                }
                else
                {
                    Console.WriteLine("Đồ thị liên thông yếu");
                }
            }
        }

        private void BFS(int u, int solt, int[] visited)
        {
            Stack<int> stack = new();
            stack.Push(u);
            // Duyệt đỉnh
            visited[u] = solt;
            while (stack.Count > 0)
            {
                int cur = stack.Pop();
                for (int i = 0; i < adjMatrix.VertexCount; i++)
                {
                    if (adjMatrix.Data[cur, i] != 0 && visited[i] == 0)
                    {
                        stack.Push(i);
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
            for (i = 0; i < adjList.VertexCount; i++)
            {
                marked[i] = 0;
            }

            //Đếm số thành phần liên thông 
            for (i = 0; i < adjList.VertexCount; i++)
            {
                if (marked[i] == 0)
                {
                    solt = solt + 1;
                    BFS(i, solt, marked);
                }
            }

            //In thành phần liên thông mạnh 
            for (i = 1; i <= solt; i++)
            {
                Console.Write($"Thành phần liên thông mạnh thứ {i}: ");
                for (int j = 0; j < adjList.VertexCount; j++)
                {
                    if (marked[j] == i)
                    {
                        Console.Write(j + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        public void Connection()
        {
            // Kiểm tra đồ thị có cạnh bội không
            if (Validation.IsMultiGraph(adjList))
            {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi khong co canh boi");
                return;
            };

            // Kiểm tra đồ thị có vô hướng hay không
            if (Validation.IsUndirectedGraph(adjMatrix))
            {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi co huong");
                return;
            };

            // Kiểm tra đồ thị có cạnh khuyên không
            if (!Validation.IsGraphHasLoops(adjMatrix))
            {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi khong co canh khuyen");
                return;
            };
            CheckConnected();
            ConnectedComponent();
        }

    }
}
