namespace Search.SearchMethod;
public static class UniformCostSearch
{
  // Structure require to represent current state in UCS
  private readonly record struct Metadata(int Name, double CumulativeCost, List<int> Path);

  // Wrapper for UCS algorithm
  public static void Run(in double[][] matrix, in Program.SearchWith config)
  {
    List<int>? path = FindPathByUCS(in matrix, in config);
    Print.Header.PathWithUnderline(Print.HeaderOfSearchMethod.UCS);
    Print.Path.WithSeparator(in path);
    Helper.PrintPathCost(in matrix, in path);
  }

  // UCS Algorithm
  private static List<int>? FindPathByUCS(in double[][] matrix, in Program.SearchWith config)
  {
    // Unpack search options
    int n = config.NxN;
    int goalNode = config.GoalNode;
    int startNode = config.StartNode;

    // Initialize lowest cumulative cost node first stack -> Initialize with starting condition
    Metadata start = new(startNode, 0, new List<int>());
    List<Metadata> stack = new() { start };

    // Initialize a dictionary to only keep the least cumulative cost for each node
    Dictionary<int, double> minCost = new(n);
    for (int i = 0; i < n; i++) { minCost.Add(i, int.MaxValue); }
    minCost[startNode] = 0; // Cumulative cost of starting node is 0

    // Recursively iterate over stack
    while (stack.Count > 0)
    {
      // Print the KVP of node and best cumulative distance
      Print.Debug.CurrentBestNode(in minCost);

      // Pop the node with lowest cumulative cost
      stack = stack.OrderBy(x => x.CumulativeCost).ToList(); // Ensures stack[0] has lowest CC
      int node = stack[0].Name;
      double cumulativeCost = stack[0].CumulativeCost;
      List<int> nodePath = stack[0].Path;
      stack.RemoveAt(0);
      Print.Debug.Message(in node, Print.About.PoppedNodeWithMinCuCost, cumulativeCost);

      // Handle a special case when a newer cumulative cost is found **after** a node has been pushed to stack
      if (cumulativeCost > minCost[node])
      {
        Print.Debug.Message(in node, Print.About.FoundNodeWithLowerCost, in cumulativeCost, minCost[node]);
        continue;
      }

      // Check for goal state
      if (node == goalNode)
      {
        Print.Debug.Message(in node, Print.About.GoalReached);
        return nodePath.Append(node).ToList();
      }

      // Otherwise append the children of current node to the stack
      List<int> children = Helper.GetChildrenOfNode(in matrix, in node, in n); // Sorting is **not** required

      // Skip to next item in stack for empty children set (duplicates or empty)
      if (children.Count == 0)
      {
        Print.Debug.Message(in node, Print.About.NoUniqueChild);
        continue;
      }

      // Otherwise, print the list of connected nodes (children)
      Print.Debug.Message(in node, in children, Print.About.ListOfChildren);

      // Append the current node to the path => newPath = oldPath + currentNode
      List<int> newPath = new();
      newPath.AddRange(nodePath); newPath.Add(node);
      foreach (int child in children)
      {
        // Only add the element to the stack if the CuSum of child < current best CuSum
        double ccChild = cumulativeCost + matrix[node][child];
        if (ccChild >= minCost[child])
        {
          Print.Debug.Message(in child, Print.About.CostHigherThanCurrentBest, in ccChild, minCost[child]);
          continue;
        }

        // If this is the new minimum CuSum, update the dictionary
        Print.Debug.Message(in child, Print.About.AppendedWithCost, in ccChild, minCost[child]);
        minCost[child] = ccChild;
        stack.Add(new Metadata(child, ccChild, newPath));
      }

      if (Input.Read.DebugFlag()) { Console.WriteLine(); }
    }

    // Reaching here implies no path was found
    Print.Debug.Message(Print.About.Exhausted);
    return null;
  }
}
