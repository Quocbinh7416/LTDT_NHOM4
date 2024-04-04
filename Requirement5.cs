using GraphTheory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project {
    
    // Kiem tra chu trinh Euler
    class Requirement5 {
        private readonly AdjacencyList adjList;
        private readonly AdjacencyMatrix adjMatrix;
        public Requirement5(string filePath) {
            adjList = new AdjacencyList(filePath);
            adjMatrix = new AdjacencyMatrix(adjList);
        }

        public void KiemTraEuler() {
            // Kiểm tra đồ thị có vô hướng hay không
            // Đồ thị có hướng -> return
            if (!Validation.IsUndirectedGraph(adjMatrix)) {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi co huong");
                return;
            };

            // Kiem tra do thi vo huong lien thong
            if (!Validation.IsBiConnectedGraph(adjMatrix)) {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi khong lien thong");
                return;
            };
            // Lấy bậc của các đỉnh
            int[] vertex =  adjMatrix.CountVertexDegree();
            List<int> oddVertexCount = new();
            for(int i = 0; i < vertex.Length; i++) {
                if (vertex[i] % 2 != 0) {
                    oddVertexCount.Add(i);
                }
            }
            //Console.WriteLine($"So dinh bac le: {oddVertexCount}");
            int casePath = 0;
            if(oddVertexCount.Count > 2) {
                Console.WriteLine("Do thi khong Euler");
                return;
            } else if (oddVertexCount.Count == 0) {
                Console.WriteLine("Do thi Euler");
                casePath = 1;
            } else if(oddVertexCount.Count == 2) {
                Console.WriteLine("Do thi nua Euler");
                casePath = 2;
            }
            List<int> vertexPath = new(0);
            int edgeCount = adjList.GetTotalEdges();
            switch (casePath) {
                case 1:
                    vertexPath = EulerCircuitsFinding(adjMatrix);
                    if (vertexPath.Count == edgeCount + 1) {
                        Console.Write("Chu trinh Euler:");
                        foreach (int i in vertexPath) {
                            Console.Write($" {i}");
                        }
                    } else {
                        Console.Write("Khong co loi giai");
                    }
                    break;
                case 2:
                    vertexPath = EulerPathFinding(adjMatrix, oddVertexCount);
                    if (vertexPath.Count == edgeCount + 1) {
                        Console.Write("Duong di Euler:");
                        foreach (int i in vertexPath) {
                            Console.Write($" {i}");
                        }
                    } else {
                        Console.Write("Khong co loi giai");
                    }
                    break;
                default:
                    break;
            }
        }

        private List<int> EulerCircuitsFinding(AdjacencyMatrix adjacencyMatrix) {
            // Băt đầu từ đỉnh 0
            int startVertex = 0;
            return EulerFinding(adjacencyMatrix, startVertex);
        }

        private List<int> EulerPathFinding(AdjacencyMatrix adjacencyMatrix, List<int> oddVertex) {
            int startVertex = oddVertex.LastOrDefault(0);

            return EulerFinding(adjacencyMatrix, startVertex);
        }


        private List<int> EulerFinding(AdjacencyMatrix adjacencyMatrix, int startVertex) {
            int[,] matrix = adjacencyMatrix.Data;
            // 
            Stack<int> stack = new();
            List<int> cycle = new();
            stack.Push(startVertex);
            while (stack.Count > 0) {
                int current = stack.Peek();
                if (isAdjectEdge(current, matrix)) {
                    for (int i = adjacencyMatrix.VertexCount - 1; i >= 0; i--) {
                        if (matrix[current, i] != 0) {
                            // xoa canh
                            matrix[current, i] = 0;
                            matrix[i, current] = 0;
                            stack.Push(i);
                            break;
                        }
                    }
                } else {
                    stack.Pop();
                    cycle.Add(current);
                }
            }
            return cycle;
        }

        // Kiem tra canh ke cua dinh
        private bool isAdjectEdge(int i, int[,] matrix) {
            bool isAdjectEdge = false;
            for (int j = 0; j < matrix.GetLength(0); j++) {
                if (matrix[j, i] != 0) {
                    isAdjectEdge = true;
                    break;
                }
            }
            return isAdjectEdge;
        }
    }
}
