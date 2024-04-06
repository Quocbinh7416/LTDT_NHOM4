using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
namespace GraphTheory
{

    class Program1
    {

        class Requirement4
        {
            private readonly adjacencyList adjList;
            private readonly adjacencyMatrix adjMatrix;

            public Requirement4(string filePath)
            {
                adjList = new adjacencyList(filePath);
                adjMatrix = new adjacencyMatrix(adjList);
            }
        }

        class Program2
        {
            static void Floyd(string[] args)
            {
                Floyd();
            }

            static void Floyd()
            {
                try
                {
                    string filePath = "your_file_path_here.txt"; // Provide the correct file path
                                                                 // Kiểm tra xem tệp tin có tồn tại không
                    if (File.Exists(filePath))
                    {
                        // Đọc tất cả các dòng từ tệp tin
                        string[] lines = File.ReadAllLines(filePath);

                        // Đọc số đỉnh
                        int V = int.Parse(lines[0]);

                        // Khởi tạo danh sách kề và ma trận trọng số
                       
                            List<List<Tuple<int, int>>> adjacencyList = new List<List<Tuple<int, int>>>();
                        
                        // Đọc danh sách kề và trọng số từ tệp tin
                        for (int i = 1; i <= V; i++)
                        {
                            string[] vertexEdges = lines[i].Split(' ');
                            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
                            for (int j = 1; j < vertexEdges.Length; j += 2)
                            {
                                int neighbor = int.Parse(vertexEdges[j]);
                                int weight = int.Parse(vertexEdges[j + 1]);
                                edges.Add(new Tuple<int, int>(neighbor, weight));
                            }
                            adjacencyList.Add(edges);
                        }

                        // In danh sách kề với trọng số tương ứng
                        Console.WriteLine("Danh sach ke:");
                        for (int i = 0; i < adjacencyList.Count; i++)
                        {
                            Console.Write($"Dinh {i + 1}: ");
                            foreach (var edge in adjacencyList[i])
                            {
                                Console.Write($"({edge.Item1}, {edge.Item2}) ");
                            }
                            Console.WriteLine();
                        }
                        // In ra chỉ mục và trọng số tương ứng
                        for (int i = 1; i < lines.Length; i++)
                        {
                            string[] elements = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                            Console.WriteLine($"Dinh so {i - 1}:");
                            for (int j = 1; j < elements.Length; j += 2)
                            {
                                int vertexIndex = int.Parse(elements[j]);
                                int weight = int.Parse(elements[j + 1]);
                                Console.WriteLine($"  Chi muc: {vertexIndex}, Trong so: {weight}");
                            }
                            Console.WriteLine();
                        }
                        for (int i = 0; i < adjacencyList.Count; i++)
                        {
                            foreach (var edge in adjacencyList[i])
                            {
                                if (edge.Item2 > 0)
                                {
                                    Console.WriteLine($"Trong so dinh {i} duong: {edge.Item2}");
                                }
                            }
                        }


                        // Tìm đường đi ngắn nhất và tổng trọng số của mỗi đỉnh
                        int[,] dist = new int[V, V]; // Ma trận lưu trữ độ dài ngắn nhất giữa các cặp đỉnh
                        int[,] next = new int[V, V]; // Ma trận lưu trữ đỉnh kế tiếp trên đường đi ngắn nhất

                        // Khởi tạo ma trận độ dài ban đầu và ma trận đỉnh kế tiếp
                        for (int i = 0; i < V; i++)
                        {
                            for (int j = 0; j < V; j++)
                            {
                                if (i == j)
                                {
                                    dist[i, j] = 0; // Đường đi từ một đỉnh đến chính nó có độ dài là 0
                                    next[i, j] = -1; // Khởi tạo đỉnh kế tiếp là -1 (không có đỉnh kế tiếp)
                                }
                                else if (adjacencyList[i].Exists(x => x.Item1 == j + 1))
                                {
                                    dist[i, j] = adjacencyList[i].Find(x => x.Item1 == j + 1).Item2; // Cập nhật độ dài từ đỉnh i đến đỉnh j nếu có cạnh nối trực tiếp
                                    next[i, j] = j; // Đỉnh kế tiếp là đỉnh j
                                }
                                else
                                {
                                    dist[i, j] = int.MaxValue; // Khởi tạo các đường đi khác với giá trị vô cực
                                    next[i, j] = -1; // Khởi tạo đỉnh kế tiếp là -1 (không có đỉnh kế tiếp)
                                }
                            }
                        }

                        // Áp dụng thuật toán Floyd Warshall để tính toán độ dài ngắn nhất và đỉnh kế tiếp
                        for (int k = 0; k < V; k++)
                        {
                            for (int i = 0; i < V; i++)
                            {
                                for (int j = 0; j < V; j++)
                                {
                                    if (dist[i, k] != int.MaxValue && dist[k, j] != int.MaxValue &&
                                        dist[i, k] + dist[k, j] < dist[i, j])
                                    {
                                        dist[i, j] = dist[i, k] + dist[k, j];
                                        next[i, j] = next[i, k]; // Cập nhật đỉnh kế tiếp trên đường đi ngắn nhất
                                    }
                                }
                            }
                        }

                        // In đường đi ngắn nhất và tổng trọng số của mỗi đỉnh
                        Console.WriteLine("Ket qua:");
                        for (int i = 0; i < V; i++)
                        {
                            for (int j = 0; j < V; j++)
                            {
                                if (i != j && next[i, j] != -1)
                                {
                                    int currentNode = i;
                                    int totalWeight = 0;
                                    string path = $"{currentNode + 1}";
                                    while (currentNode != j)
                                    {
                                        currentNode = next[currentNode, j];
                                        totalWeight += dist[currentNode, j];
                                        path += $" -> {currentNode + 1}";
                                    }
                                    Console.WriteLine($"Tu dinh {i + 1} den dinh {j + 1}: {path}, tong trong so: {totalWeight}");
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Khong co duong di ngan nhat");
                }
            }
        }
    }
}
