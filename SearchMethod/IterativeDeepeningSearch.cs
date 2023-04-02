namespace Search.SearchMethod;
public static class IterativeDeepeningSearch
{
  // Wrapper for DLS algorithm
  public static void Run(in double[][] matrix, in Program.SearchWith config)
  {
    const string header = "\nIterative Deepening Search Path";
    const string separator = " --> ";

    // Increment depth while no path is found
    int depth = 0;
    const int maxDepth = 10; // Failsafe for no path found in reasonable time
    while (depth <= maxDepth)
    {
      // Custom message for reaching hard coded iteration limit
      if (depth == maxDepth)
      {
        Console.WriteLine($"Reached hard coded depth limit of [d={maxDepth}]");
        Console.WriteLine("No path was found. If a patch exists, consider increasing `maxDepth`.");
        return;
      }

      // Try to find a path at current depth. Start from 0
      Program.SearchWith iDepth = new(config.StartNode, config.GoalNode, depth);
      List<int>? path = DepthLimitedSearch.AlgoDLS(in matrix, in iDepth);

      // Increment depth if a path was not found
      if (path is null)
      {
        Console.WriteLine($"ID-DFS failed to find goal with [d={depth}]. Retrying with [d={depth + 1}]\n");
        depth++; continue;
      }

      // Otherwise, print the path
      Console.WriteLine($"\nID-DFS found the goal at depth limit of [d={depth}]");
      Print.Header.WithUnderline(header);
      Print.Path.WithSeparator(in path, separator);
      return;
    }
  }
}
