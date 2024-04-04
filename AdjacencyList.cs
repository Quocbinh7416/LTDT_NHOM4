using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace GraphTheory
{
    public class Edge
    {
        public int Destination { get; set; }
        public int Weight { get; set; }
    }

    public class AdjacencyList
    {
        public List<List<Edge>> Data { get; }
        public int VertexCount { get; }

        // Khởi tạo danh sách kề
        public AdjacencyList(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                int.TryParse(sr.ReadLine(), out int n);
                VertexCount = n;

                Data = new List<List<Edge>>(VertexCount);
                for (int i = 0; i < VertexCount; i++)
                {
                    Data.Add(new List<Edge>());
                }


                for (int i = 0; i < VertexCount; i++)
                {

                    string[] line = sr.ReadLine()!.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    for (int j = 1; j < line.Length; j += 2)
                    {
                        var destination = int.Parse(line[j]);
                        var weight = int.Parse(line[j + 1]);

                        AddEdge(i, destination, weight);
                    }

                }
            }
        }

        // Thêm một cạnh với trọng số từ đỉnh source đến đỉnh destination
        private void AddEdge(int source, int destination, int weight)
        {
            Data[source].Add(new Edge { Destination = destination, Weight = weight });
        }

        // Lấy danh sách các cạnh bắt đầu từ đỉnh source
        public List<Edge> GetEdges(int source)
        {
            return Data[source];
        }

        // Lấy số các cạnh ( TH Vô hướng)
        public int GetTotalEdges() {
            int total = 0;
            foreach ( var edge in Data) {
                total += edge.Count;
            }
            return total / 2;
        }
    }

}