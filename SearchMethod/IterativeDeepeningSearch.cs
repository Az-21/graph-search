﻿namespace Search.SearchMethod;
public static class IterativeDeepeningSearch
{
  // Wrapper for ID-DFS (IDLS) algorithm
  public static void Run(in double[][] matrix, in Program.SearchWith config)
  {
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
      List<int>? path = DepthLimitedSearch.FindPathByDLS(in matrix, in config, depth);

      // Increment depth if a path was not found
      if (path is null)
      {
        Console.WriteLine($"ID-DFS failed to find goal with [d={depth}]. Retrying with [d={depth + 1}]\n");
        depth++; continue;
      }

      // Otherwise, print the path
      Console.WriteLine($"\nID-DFS found the goal at depth limit of [d={depth}]");
      Print.Header.PathWithUnderline(Print.HeaderOfSearchMethod.IDLS);
      Print.Path.WithSeparator(in path);
      Helper.PrintPathCost(in matrix, in path);
      return;
    }
  }
}
