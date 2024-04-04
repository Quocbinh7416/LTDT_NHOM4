namespace GraphTheory
{
    public class AdjacencyMatrix
    {
        public int VertexCount { get; }
        public int[,] Data { get; }

        public AdjacencyMatrix(AdjacencyList adjacencyList)
        {
            VertexCount = adjacencyList.VertexCount;

            Data = new int[VertexCount, VertexCount];

            for (int i = 0; i < VertexCount; i++)
            {
                var edges = adjacencyList.GetEdges(i);
                for (int j = 0; j < edges.Count; j++)
                {
                    Data[i, edges[j].Destination] = edges[j].Weight;
                }
            }
        }

        // Lay danh sach bac cua dinh
        public int[] CountVertexDegree() {
            var vertexCount = this.VertexCount;
            var data = this.Data;

            int[] degrees = new int[vertexCount];
            for (int i = 0; i < vertexCount; i++) {
                for (int j = 0; j < vertexCount; j++) {
                    if (data[i, j] != 0) 
                        degrees[i] ++;
                }
            }

            return degrees;
        }
    }
}

