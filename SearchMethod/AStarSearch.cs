namespace Search.SearchMethod;
public static class AStarSearch
{
  // Structure require to represent current state in A*
  public readonly record struct MetadataAStar(int Name, double CumulativeCost, double HeuristicCost, List<int> Path);

  // Wrapper for A* algorithm
  public static void Run(in double[][] matrix, in Program.SearchWith config)
  {
    List<int>? path = FindPathByAStar(in matrix, in config);
    Print.Header.PathWithUnderline(Print.HeaderOfSearchMethod.ASTAR);
    Print.Path.WithSeparator(in path);
    Helper.PrintPathCost(in matrix, in path);
  }

  // A* Algorithm
  private static List<int>? FindPathByAStar(in double[][] matrix, in Program.SearchWith config)
  {
    // Unpack search options
    int startNode = config.StartNode;
    int goalNode = config.GoalNode;

    // Parse heuristic table from adjacency matrix
    double[] h = Helper.GetHeuristics(in matrix);

    // Initialize lowest <cumulative + hueristic> cost node first stack -> Initialize with starting condition
    MetadataAStar start = new(startNode, 0, h[startNode], new List<int>());
    List<MetadataAStar> stack = new() { start };

    // Initialize a dictionary to only keep the least `cumulative cost + heuristic cost` for each node
    Dictionary<int, double> minCost = new();
    for (int i = 0; i < matrix.GetLength(0); i++) { minCost.Add(i, int.MaxValue); }
    minCost[startNode] = 0 + h[startNode]; // Cost(n) = cumulative(n) + h(n)

    // Recursively iterate over stack
    while (stack.Count > 0)
    {
      // Print the KVP of node and best cost (cumulative + heuristic)
      Print.Debug.CurrentBestNode(in minCost);

      // Pop the node with lowest cumulative cost
      stack = stack.OrderBy(x => x.CumulativeCost).ToList(); // Ensures stack[0] has lowest CC
      int node = stack[0].Name;
      double cuCost = stack[0].CumulativeCost;
      double hnCost = stack[0].HeuristicCost;
      double nodeCost = cuCost + hnCost;
      List<int> nodePath = stack[0].Path;
      stack.RemoveAt(0);
      Print.Debug.Message(in node, Print.About.PoppedNodeWithMinTotalCost, nodeCost);

      // Handle a special case when a newer and lower cost is found **after** a node has been pushed to stack
      if (nodeCost > minCost[node])
      {
        Print.Debug.Message(in node, Print.About.FoundNodeWithLowerCost, in nodeCost, minCost[node]);
        continue;
      }

      // Check for goal state
      if (node == goalNode)
      {
        Print.Debug.Message(in node, Print.About.GoalReached);
        return nodePath.Append(node).ToList();
      }

      // Otherwise append the children of current node to the stack
      List<int> children = Helper.GetChildrenOfNode(in matrix, in node); // Sorting is **not** required

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
        // Only add the element to the stack if the cost of child > current best cost of child
        double cuChild = cuCost + matrix[node][child];
        double hnChild = h[child];
        double childCost = cuChild + hnChild;

        if (childCost >= minCost[child])
        {
          Print.Debug.Message(in child, Print.About.CostHigherThanCurrentBest, in childCost, minCost[child]);
          continue;
        }

        // If this is the new minimum cost, update the dictionary
        Print.Debug.Message(in child, Print.About.AppendedWithCost, in childCost, minCost[child]);
        minCost[child] = childCost;
        stack.Add(new MetadataAStar(child, cuChild, hnChild, newPath));
      }

      if (Input.Read.DebugFlag()) { Console.WriteLine(); }
    }

    // Reaching here implies no path was found
    Print.Debug.Message(Print.About.Exhausted);
    return null;
  }
}
