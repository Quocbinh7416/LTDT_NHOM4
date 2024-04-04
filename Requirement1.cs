using System;
using System.Linq;
using System.Collections.Generic;

namespace GraphTheory
{
    class Requirement1

    {

        private readonly AdjacencyList adjList;
        private readonly AdjacencyMatrix adjMatrix;
        public Requirement1(string filePath)
        {
            adjList = new AdjacencyList(filePath);
            adjMatrix = new AdjacencyMatrix(adjList);
        }

        private int[] CountVertexDegree()
        {
            var vertexCount = adjMatrix.VertexCount;
            var data = adjMatrix.Data;

            int[] degrees = new int[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    degrees[i] += data[i, j];
                }
            }

            return degrees;
        }

        private bool IsSubCompleteGraph(List<int> vertices)
        {
            var data = adjMatrix.Data;

            foreach (int i in vertices)
            {
                foreach (int j in vertices)
                {
                    if (i != j && data[i, j] == 0)
                        return false;
                }
            }

            return true;
        }

        private bool IsWindmillGraph(out int n)
        {
            // Khởi tạo params
            int k = 3; // Theo đề bài
            n = 0;

            var vertexCount = adjMatrix.VertexCount;
            var data = adjMatrix.Data;

            // Đếm bậc của từng đỉnh
            int[] degrees = CountVertexDegree();

            // Tìm đỉnh chung là đỉnh có kết nối với tất cả các đỉnh còn lại
            int universalVertex = Array.FindIndex(degrees, index => index == vertexCount - 1);
            if (universalVertex == -1) return false;

            // Kiểm tra bậc của tất cả các đỉnh còn lại có bằng nhau và bằng 2 không
            int minDegree = k - 1;
            for (int i = 0; i < vertexCount; i++)
            {
                if (i != universalVertex && degrees[i] != minDegree) return false;
            }

            // Tìm số bản sao của đồ thị con đầy đủ
            int edgeSum = degrees.Sum() / 2; // Tổng số cạnh của đồ thị
            int subCompleteGraphEdgeSum = k * (k - 1) / 2;  // Số cạnh của một đồ thị con đầy đủ
            n = edgeSum / subCompleteGraphEdgeSum;

            return true;
        }

        private bool IsBarbellGraph(out int n)
        {
            // Khởi tạo params
            n = 0;

            var vertexCount = adjMatrix.VertexCount;
            var data = adjMatrix.Data;

            // Đếm bậc của từng đỉnh
            int[] degrees = CountVertexDegree();

            // Tìm số đỉnh của mỗi đồ thị con (trái và phải)
            int leftVertexCount = vertexCount / 2;
            int rightVertexCount = vertexCount - leftVertexCount;
            if (leftVertexCount != rightVertexCount) return false; // Số đỉnh hai bên đồ thị không đều

            // Tìm 2 đỉnh của cầu nối giữa 2 đồ thị con
            int maxDegree = degrees.Max();
            int leftBridgeVertex = Array.FindIndex(degrees, index => index == maxDegree);
            int rightBridgeVertex = Array.FindLastIndex(degrees, index => index == maxDegree);
            if (leftBridgeVertex == rightBridgeVertex) return false; // Hai đỉnh của cầu nối phải phân biệt

            // Kiểm tra 2 đỉnh của cầu nối có nối với nhau không
            if (data[leftBridgeVertex, rightBridgeVertex] == 0) return false;

            // Tìm các đỉnh của đồ thị con bên trái
            List<int> leftSubGraphVertices = new List<int>(leftVertexCount)
            {
                leftBridgeVertex
            };
            for (int j = 0; j < vertexCount; j++)
            {
                if (data[leftBridgeVertex, j] != 0 && j != rightBridgeVertex)
                {
                    leftSubGraphVertices.Add(j);
                }
            }

            // Tìm các đỉnh của đồ thị con bên phải
            List<int> rightSubGraphVertices = new List<int>(rightVertexCount) {
                rightBridgeVertex
            };
            for (int j = 0; j < vertexCount; j++)
            {
                if (data[rightBridgeVertex, j] != 0 && j != leftBridgeVertex)
                {
                    rightSubGraphVertices.Add(j);
                }
            }

            // Kiểm tra 2 đồ thị con có phải là đồ thị đầy đủ
            if (!IsSubCompleteGraph(leftSubGraphVertices) || !IsSubCompleteGraph(rightSubGraphVertices))
                return false;

            // Gán giá trị bậc cho đồ thị
            n = leftVertexCount;
            return true;
        }

        // Kiểm tra xem 1 đỉnh con có kề với các đỉnh trong các list khác hay không
        bool IsElementAdjacencyToAll(List<List<int>> list, int currentIndex, int element)
        {
            var data = adjMatrix.Data;

            for (int i = 0; i < list.Count; i++)
            {
                if (i == currentIndex) continue;

                bool check = false;
                for (int j = 0; j < list[i].Count; j++)
                {
                    if (data[element, list[i][j]] != 0)
                    {
                        check = true;
                        break;
                    }
                }

                if (check == true) continue;
                else return false;
            }

            return true;
        }

        private bool IsKPartiteGraph(out int k, out List<List<int>> partiteList)
        {

            // Khởi tạo params
            k = 0;
            partiteList = new List<List<int>>();

            var vertexCount = adjMatrix.VertexCount;
            var data = adjMatrix.Data;

            // Khởi tạo partite đầu tiên chứa đỉnh 0
            var firstPartite = new List<int> { 0 };
            partiteList.Add(firstPartite);
            k++;

            // Vòng lặp qua tất cả các đỉnh còn lại
            for (int i = 1; i < vertexCount; i++)
            {
                // Mảng bool để kiểm tra đỉnh có kề với partite không
                bool[] checkAdjacencyPartite = new bool[partiteList.Count];
                for (int j = 0; j < checkAdjacencyPartite.Length; j++)
                {
                    checkAdjacencyPartite[j] = false;
                }

                // Vòng lặp qua tất cả các partite đã có
                for (int m = 0; m < partiteList.Count; m++)
                {
                    for (int n = 0; n < partiteList[m].Count; n++)
                    {
                        var vertex = partiteList[m][n];

                        // Nếu có cạnh nối với đỉnh i cần kiểm tra
                        if (data[i, vertex] != 0)
                        {
                            checkAdjacencyPartite[m] = true;
                            break;
                        }
                    }
                }

                // Nếu tất cả các đỉnh trong partie hiện tại đều kể với đỉnh i
                if (checkAdjacencyPartite.All(x => x == true))
                {
                    var partite = new List<int> { i };
                    partiteList.Add(partite);
                    k++;
                }
                else
                {
                    var partieIndex = Array.FindIndex(checkAdjacencyPartite, x => x == false);
                    partiteList[partieIndex].Add(i);
                }
            }

            // Vòng lặp kiểm tra xem mỗi đỉnh trong từng partie có kề với tất cả các partie khác hay không
            for (int i = 0; i < partiteList.Count; i++)
            {
                for (int j = 0; j < partiteList[i].Count; j++)
                {
                    int vertexToCheck = partiteList[i][j];

                    if (!IsElementAdjacencyToAll(partiteList, i, vertexToCheck))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void Implement()
        {
            // Kiểm tra đồ thị có cạnh bội không
            if (Validation.IsMultiGraph(adjList))
            {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi input khong phai la do thi khong co canh boi");
                return;
            };

            // Kiểm tra đồ thị có vô hướng hay không
            if (!Validation.IsUndirectedGraph(adjMatrix))
            {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi input khong phai la do thi vo huong");
                return;
            };

            // Kiểm tra đồ thị có cạnh khuyên không
            if (!Validation.IsGraphHasLoops(adjMatrix))
            {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi input khong phai la do thi khong co canh khuyen");
                return;
            };

            // Kiểm tra đồ thị cối xay gió
            if (IsWindmillGraph(out int nWindmill))
                Console.WriteLine($"Do thi coi xay gio: Wd(3,{nWindmill})");
            else Console.WriteLine("Do thi coi xay gio: Khong");

            // Kiểm tra đồ thị barbell
            if (IsBarbellGraph(out int nBarbell))
                Console.WriteLine($"Do thi barbell: Bac {nBarbell}");
            else Console.WriteLine("Do thi barbell: Khong");
            // Kiểm tra đồ thị barbell

            if (IsKPartiteGraph(out int kPartite, out List<List<int>> partiteList))
            {
                Console.Write($"Do thi k-phân: {kPartite}-partite ");
                foreach (List<int> partite in partiteList)
                {
                    Console.Write("{");
                    for (int i = 0; i < partite.Count; i++)
                    {
                        if (i == partite.Count - 1) Console.Write(partite[i]);
                        else Console.Write($"{partite[i]}, ");
                    }
                    Console.Write("} ");
                }
                Console.WriteLine();
            }
            else Console.WriteLine("Do thi k-phân: Khong");
        }

    }
}
