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

        private int[,] Transpose(int[,] TransGraph, AdjacencyMatrix adjacencyMatrix)
        {
            for (int i = 0; i < adjMatrix.VertexCount; i++)
            {
                for (int j = 0; j < adjMatrix.VertexCount; j++)
                {
                    TransGraph[i, j] = adjacencyMatrix.Data[j, i];
                }
            }
            return TransGraph;
        }

        private void BFS(int u, int[] visited, int[,] TransGraph)
        {
            visited[u] = 1;
            Console.Write(u + " ");
            for (int i = 0; i < TransGraph.GetLength(0); i++)
            {
                if (TransGraph[u, i] != 0)
                {
                    if (visited[i] == 0)
                    {
                        BFS(i, visited, TransGraph);
                    }
                }
            }
        }

        private void BFS2(int u, int[] visited, Stack<int> stack)
        {
            visited[u] = 1;
            for (int i = 0; i < adjMatrix.VertexCount; i++)
            {
                if (adjMatrix.Data[u, i] != 0)
                {
                    if (visited[i] == 0)
                    {
                        BFS2(i, visited, stack);
                    }
                }
            }
            stack.Push(u);
        }

        //Đếm thành phần liên thông mạnh
        private void ConnectedComponent()
        {
            int i, CSS;
            CSS = 0;
            int[] marked = new int[adjMatrix.VertexCount];
            Stack<int> stack = new();
            int[,] TransGraph = new int[adjMatrix.VertexCount, adjMatrix.VertexCount];
            //khởi tạo giá trị ban đầu cho mảng marked
            for (i = 0; i < marked.Length; i++)
            {
                marked[i] = 0;
            }

            //Đếm số thành phần liên thông 
            for (i = 0; i < adjList.VertexCount; i++)
            {
                if (marked[i] == 0)
                {
                    BFS2(i, marked, stack);
                }
            }

            for (i = 0; i < marked.Length; i++)
            {
                marked[i] = 0;
            }

            TransGraph = Transpose(TransGraph, adjMatrix);
            while (stack.Count > 0)
            {
                int cur = stack.Peek();
                stack.Pop();
                if (marked[cur] == 0)
                {
                    CSS++;
                    Console.WriteLine($"Thanh phan lien thong manh thu {CSS}: ");
                    BFS(cur, marked, TransGraph);
                    Console.WriteLine();
                }
            }
        }

        public void Connection()
        {
            // Kiểm tra đồ thị có cạnh khuyên không
            if (!Validation.IsdirectedGraphHasLoop(adjList))
            {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi input khong phai la do thi khong co canh khuyen");
                return;
            };

            if (Validation.IsMultiDirectedGraph(adjMatrix))
            {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi input khong phai la do thi khong co canh boi");
                return;
            };

            // Kiểm tra đồ thị có vô hướng hay không
            if (Validation.IsUndirectedGraph(adjMatrix))
            {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi input khong phai la do thi co huong");
                return;
            };

            
            CheckConnected();
            ConnectedComponent();
        }

    }
}
