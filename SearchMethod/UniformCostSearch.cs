namespace Search.SearchMethod;
public static class UniformCostSearch
{
  // Structure require to represent current state in UCS
  private readonly record struct Metadata(int Name, double CumulativeCost, List<int> Path);

  // Wrapper for UCS algorithm
  public static void Run(in double[][] matrix, in Program.SearchWith config)
  {
    List<int>? path = Algo(in matrix, in config);

    // Header
    const string header = "\nUniform Cost Search Path";
    Print.Header.WithUnderline(header);

    const string separator = " --> ";
    Print.Path.WithSeparator(in path, separator);
  }

  // UCS Algorithm
  private static List<int>? Algo(in double[][] matrix, in Program.SearchWith config)
  {
    // Unpack search options
    int startNode = config.StartNode;
    int goalNode = config.GoalNode;

    // Initialize lowest cumulative cost node first stack -> Initialize with starting condition
    Metadata start = new(startNode, 0, new List<int>());
    List<Metadata> stack = new() { start };

    // Initialize a dictionary to only keep the least cumulative cost for each node
    Dictionary<int, double> leastCost = new();
    for (int i = 0; i < matrix.GetLength(0); i++) { leastCost.Add(i, int.MaxValue); }
    leastCost[startNode] = 0; // Cumulative cost of starting node is 0

    // Recursively iterate over stack
    while (stack.Count > 0)
    {
      // Print the KVP of node and best cumulative distance
      Print.Debug.CurrentBestCumulative(in leastCost);

      // Pop the node with lowest cumulative cost
      stack = stack.OrderBy(x => x.CumulativeCost).ToList(); // Ensures stack[0] has lowest CC
      int node = stack[0].Name;
      double cumulativeCost = stack[0].CumulativeCost;
      List<int> nodePath = stack[0].Path;
      stack.RemoveAt(0);
      Print.Debug.Message(in node, Print.About.PoppedNodeWithMinCuCost, cumulativeCost);

      // Handle a special case when a newer cumulative cost is found **after** a node has been pushed to stack
      if (cumulativeCost > leastCost[node])
      {
        Print.Debug.Message(in node, Print.About.FoundNodeWithLowerCuCost, in cumulativeCost, leastCost[node]);
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
        // Ensure a value corresponding to least CuSum exists in dictionary (eg: node 10 in 7x7 matrix)
        if (!leastCost.ContainsKey(child)) { leastCost.Add(child, int.MaxValue); }

        // Only add the element to the stack if the CuSum of child < current best CuSum
        double ccChild = cumulativeCost + matrix[node][child];
        if (ccChild >= leastCost[child])
        {
          Print.Debug.Message(in child, Print.About.CostHigherThanCurrentBest, in ccChild, leastCost[child]);
          continue;
        }

        // If this is the new minimum CuSum, update the dictionary
        Print.Debug.Message(in child, Print.About.AppendedWithCost, in ccChild, leastCost[child]);
        leastCost[child] = ccChild;
        stack.Add(new Metadata(child, ccChild, newPath));
      }

      if (Input.Read.DebugFlag()) { Console.WriteLine(); }
    }

    // Reaching here implies no path was found
    Print.Debug.Message(Print.About.Exhausted);
    return null;
  }
}
