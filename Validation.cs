namespace GraphTheory
{
    class Validation
    {
        // Kiểm tra đồ thị có cạnh bội hay không
        public static bool IsMultiGraph(AdjacencyList adjacencyList)
        {
            for (int i = 0; i < adjacencyList.VertexCount; i++)
            {
                var edges = adjacencyList.GetEdges(i);

                // Kiểm tra từng đỉnh cuối trong list các cạnh xem có bị lặp lại hay không
                for (int j = 0; j < edges.Count; j++)
                {
                    for (int k = j + 1; k < edges.Count; k++)
                    {
                        if (edges[j].Destination == edges[k].Destination) return true;
                    }
                }
            }

            return false;
        }

        public static bool IsUndirectedGraph(AdjacencyMatrix adjacencyMatrix)
        {
            int i, j;
            bool isSymmetric = true;

            for (i = 0; i < adjacencyMatrix.VertexCount && isSymmetric; i++)
            {
                for (j = i + 1; (j < adjacencyMatrix.VertexCount) && (adjacencyMatrix.Data[i, j] == adjacencyMatrix.Data[j, i]); j++) ;
                if (j < adjacencyMatrix.VertexCount)
                    isSymmetric = false;
            }
            return isSymmetric;
        }

        public static bool IsGraphHasLoops(AdjacencyMatrix adjacencyMatrix)
        {
            for (int i = 0; i < adjacencyMatrix.VertexCount && adjacencyMatrix.Data[i, i] == 0; i++)
                if (i < adjacencyMatrix.VertexCount)
                    return true;
            return false;
        }
    }
}