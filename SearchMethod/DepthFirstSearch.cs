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
    // Print parsed matrix | IF_DEF
    Print.Matrix.FromParsedCsv(in matrix);

    // Initialize a list to keep track of visited nodes
    List<int> visited = new();

    // Initialize last come first serve stack -> Initialize with starting condition
    Metadata start = new(startNode, new List<int>());
    List<Metadata> stack = new() { start };

    // Recursively iterate over stack
    while (stack.Count > 0)
    {
      // Capture and pop last
      int iLast = stack.Count - 1;
      int nodeName = stack[iLast].Name;
      List<int> nodePath = stack[iLast].Path;
      stack.RemoveAt(iLast);
      Print.Debug.PoppedNode(in nodeName);

      // Check if the node already exists in the visited list
      if (visited.Contains(nodeName))
      {
        Print.Debug.AlreadyVisited(in nodeName);
        continue;
      }

      // Otherwise, append the node to the visited list
      visited.Add(nodeName);
      Print.Debug.NowVisiting(in nodeName);

      // Check for goal state
      if (nodeName == goalNode)
      {
        Print.Debug.GoalReached(in nodeName);
        return nodePath.Append(nodeName).ToList();
      }

      // Otherwise append the children of current node to the stack
      List<int> children = Helper.GetChildrenOfNode(in matrix, in nodeName);
      children.Sort(); children.Reverse(); // Sort and reverse to ensure [C, B, A] like order, A will pop next
      children = children.Where(x => !visited.Contains(x)).ToList(); // Filter out visited nodes

      // Skip to next item in stack for empty children set (duplicates or empty)
      if (children.Count == 0)
      {
        Print.Debug.NoUniqueChildren(in nodeName);
        continue;
      }

      // Append the current node to the path => newPath = oldPath + currentNode
      List<int> newPath = new();
      newPath.AddRange(nodePath); newPath.Add(nodeName);
      foreach (int child in children)
      {
        Print.Debug.AppendedNode(in child);
        stack.Add(new Metadata(child, newPath));
      }
#pragma warning disable CS0162 // Unreachable code detected
      if (Program.DEBUG) { Console.WriteLine(); }
#pragma warning restore CS0162 // Unreachable code detected
    }

    // Reaching here implies no path was found
    Print.Debug.StackExhausted();
    return null;
  }
}
