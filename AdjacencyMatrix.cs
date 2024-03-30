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
    }
}

