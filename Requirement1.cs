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

        private bool IsWindmillGraph(out int k, out int n)
        {
            // Khởi tạo params
            k = 0;
            n = 0;

            var vertexCount = adjMatrix.VertexCount;
            var data = adjMatrix.Data;

            // Đếm bậc của từng đỉnh
            int[] degrees = CountVertexDegree();

            // Tìm đỉnh chung
            int maxDegree = degrees.Max();
            int universalVertex = Array.FindIndex(degrees, index => index == maxDegree);

            // Kiểm tra đỉnh chung có kết nối tới tất cả các đỉnh còn lại không
            if (maxDegree < vertexCount - 1) return false;

            // Kiểm tra bậc của tất cả các đỉnh còn lại có bằng nhau không
            int minDegree = degrees.Min();
            for (int i = 0; i < vertexCount && i != universalVertex; i++)
            {
                if (degrees[i] != minDegree) return false;
            }

            // Tìm số đỉnh của một đồ thị con đầy đủ
            k = minDegree + 1;

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

        public void Implement()
        {
            // Kiểm tra đồ thị có cạnh bội không
            if (Validation.IsMultiGraph(adjList))
            {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi khong co canh boi");
                return;
            };

            // Kiểm tra đồ thị có vô hướng hay không
            if (!Validation.IsUndirectedGraph(adjMatrix))
            {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi vo huong");
                return;
            };

            // Kiểm tra đồ thị có cạnh khuyên không
            if (!Validation.IsGraphHasLoops(adjMatrix))
            {
                Console.Write(Constant.GraphNotMeetRequirement);
                Console.WriteLine("Do thi khong co canh khuyen");
                return;
            };

            // Kiểm tra đồ thị cối xay gió
            if (IsWindmillGraph(out int kWindmill, out int nWindmill))
                Console.WriteLine($"Do thi coi xay gio: Wd({kWindmill},{nWindmill})");
            else Console.WriteLine("Do thi coi xay gio: Khong");

            // Kiểm tra đồ thị barbell
            if (IsBarbellGraph(out int nBarbell))
                Console.WriteLine($"Do thi barbell: Bac {nBarbell}");
            else Console.WriteLine("Do thi barbell: Khong");
        }

    }
}
