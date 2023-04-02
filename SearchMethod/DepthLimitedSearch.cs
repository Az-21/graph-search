namespace Search.SearchMethod;
public static class DepthLimitedSearch
{
  // Structure require to represent current state in DLS
  private readonly record struct Metadata(int Name, int Depth, List<int> Path);

  // Wrapper for DLS algorithm
  public static void Run(in double[][] matrix, in Program.SearchWith config)
  {
    List<int>? path = Algo(in matrix, in config);

    // Header
    const string header = "\nDepth Limited Search Path";
    Print.Header.WithUnderline(header);

    const string separator = " --> ";
    Print.Path.WithSeparator(in path, separator);
  }

  // DLS Algorithm
  private static List<int>? Algo(in double[][] matrix, in Program.SearchWith config)
  {
    // Unpack search options
    int startNode = config.StartNode;
    int goalNode = config.GoalNode;
    int depthLimit = config.DepthLimit;

    // Initialize a list to keep track of visited nodes
    HashSet<int> visited = new();

    // Initialize last come first serve stack -> Initialize with starting condition
    Metadata start = new(startNode, 0, new List<int>());
    List<Metadata> stack = new() { start };

    // Recursively iterate over stack
    while (stack.Count > 0)
    {
      // Capture and pop last
      int iLast = stack.Count - 1;
      int node = stack[iLast].Name;
      int depth = stack[iLast].Depth;
      List<int> nodePath = stack[iLast].Path;
      stack.RemoveAt(iLast);
      Print.Debug.Message(in node, Print.About.PoppedNode);

      // Check if the node already exists in the visited list
      if (visited.Contains(node))
      {
        Print.Debug.Message(in node, Print.About.AlreadyVisited);
        continue;
      }

      // Otherwise, append the node to the visited list
      visited.Add(node);
      Print.Debug.Message(in node, Print.About.AddedToVisited);

      // Check for depth limit
      if (depth >= depthLimit)
      {
        Print.Debug.Message(node, Print.About.ReachedDepthLimit, depth);
        continue;
      }

      // Check for goal state
      if (node == goalNode)
      {
        Print.Debug.Message(in node, Print.About.GoalReached);
        return nodePath.Append(node).ToList();
      }

      // Otherwise append the children of current node to the stack
      List<int> children = Helper.GetChildrenOfNode(in matrix, in node);
      children.Sort(); children.Reverse(); // Sort and reverse to ensure [C, B, A] like order, A will pop next
      children = children.Where(x => !visited.Contains(x)).ToList(); // Filter out visited nodes

      // Skip to next item in stack for empty children set (duplicates or empty)
      if (children.Count == 0)
      {
        Print.Debug.Message(in node, Print.About.NoUniqueChild);
        continue;
      }

      // Append the current node to the path => newPath = oldPath + currentNode
      List<int> newPath = new();
      newPath.AddRange(nodePath); newPath.Add(node);
      depth++; // Children of a node get +1 depth
      foreach (int child in children)
      {
        Print.Debug.Message(in child, Print.About.Appended);
        stack.Add(new Metadata(child, depth, newPath));
      }

      if (Input.Read.DebugFlag()) { Console.WriteLine(); }
    }

    // Reaching here implies no path was found
    Print.Debug.Message(Print.About.Exhausted);
    return null;
  }
}
