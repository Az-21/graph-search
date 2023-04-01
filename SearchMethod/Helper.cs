﻿namespace Search.SearchMethod;
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
}