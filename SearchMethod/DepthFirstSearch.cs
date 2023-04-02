namespace Search.SearchMethod;
public static class DepthFirstSearch
{
  // Structure require to represent current state in DFS
  private readonly record struct Metadata(int Name, List<int> Path);

  // Wrapper for DFS algorithm
  public static void Run(in double[][] matrix, in int startNode, in int goalNode)
  {
    List<int>? path = Algo(in matrix, in startNode, in goalNode);

    // Header
    const string header = "\nDepth First Search Path";
    Print.Header.WithUnderline(header);

    const string separator = " --> ";
    Print.Path.WithSeparator(in path, separator);
  }

  // DFS Algorithm
  private static List<int>? Algo(in double[][] matrix, in int startNode, in int goalNode)
  {
    // Initialize a list to keep track of visited nodes
    HashSet<int> visited = new();

    // Initialize last come first serve stack -> Initialize with starting condition
    Metadata start = new(startNode, new List<int>());
    List<Metadata> stack = new() { start };

    // Recursively iterate over stack
    while (stack.Count > 0)
    {
      // Capture and pop last
      int iLast = stack.Count - 1;
      int node = stack[iLast].Name;
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
      foreach (int child in children)
      {
        Print.Debug.Message(in child, Print.About.Appended);
        stack.Add(new Metadata(child, newPath));
      }

      if (Input.Read.DebugFlag()) { Console.WriteLine(); }
    }

    // Reaching here implies no path was found
    Print.Debug.Message(Print.About.Exhausted);
    return null;
  }
}
