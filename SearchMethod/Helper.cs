namespace Search.SearchMethod;
public static class Helper
{
  // Returns name/id of children of a node as an array
  public static List<int> GetChildrenOfNode(in double[][] matrix, in int node)
  {
    List<int> children = new();
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
      // Skip the diagonal node which contains heuristic value h(n) -> Not used in DFS
      if (i == node) { continue; }

      // If node-to-child distance in non-zero, add name/id of child to list
      if (matrix[node][i] != 0) { children.Add(i); } // Index corresponds to name/id of child
    }

    return children;
  }

  // Returns the primary diagonal values of matrix -> h(n) | h[0] -> h(node 0)
  public static double[] GetHeuristics(in double[][] matrix)
  {
    int n = matrix.GetLength(0);
    double[] h = new double[n]; // Heuristic h(n)

    // Get (i, j) where i == j
    for (int i = 0; i < n; i++) { for (int j = 0; j < n; j++) { if (i == j) { h[i] = matrix[i][i]; } } }
    return h;
  }

  // Total traversal cost
  public static void PrintPathCost(in double[][] matrix, in List<int>? path)
  {
    if (path is null) { return; }

    double cost = Convert.Path.ToCost(in matrix, in path);
    Console.WriteLine($"Total cost = {cost}");
  }
}
