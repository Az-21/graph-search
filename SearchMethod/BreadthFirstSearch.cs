namespace Search.SearchMethod;
public static class BreadthFirstSearch
{
  // Structure require to represent current state in BFS
  private readonly record struct Metadata(int Name, List<int> Path);

  // Wrapper for BFS algorithm
  public static void Run(in double[][] matrix, in int startNode, in int goalNode)
  {
    List<int>? path = Algo(in matrix, in startNode, in goalNode);

    // Header
    const string header = "\nBreadth First Search Path";
    Print.Header.WithUnderline(header);

    const string separator = " --> ";
    Print.Path.WithSeparator(in path, separator);
  }

  // BFS Algorithm
  private static List<int>? Algo(in double[][] matrix, in int startNode, in int goalNode)
  {
    // Initialize a list to keep track of visited nodes
    HashSet<int> visited = new();

    // Initialize first come first serve queue -> Initialize with starting condition
    Metadata start = new(startNode, new List<int>());
    Queue<Metadata> queue = new(); queue.Enqueue(start);

    // Recursively iterate over queue
    while (queue.Count > 0)
    {
      // Capture and pop first element in queue
      int nodeName = queue.Peek().Name; // queue[0].Name
      List<int> nodePath = queue.Peek().Path; // queue[0].Path
      queue.Dequeue();
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

      // Otherwise append the children of current node to the queue
      List<int> children = Helper.GetChildrenOfNode(in matrix, in nodeName);
      children = children.Where(x => !visited.Contains(x)).ToList(); // Filter visited nodes
      children.Sort(); // Sort to ensure [A, B, C] like order, A will eventually pop first

      // Skip to next item in queue for empty children set (duplicates or empty)
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
        queue.Enqueue(new Metadata(child, newPath));
      }
#pragma warning disable CS0162 // Unreachable code detected
      if (Program.DEBUG) { Console.WriteLine(); }
#pragma warning restore CS0162 // Unreachable code detected
    }

    // Reaching here implies no path was found
    Print.Debug.Exhausted("queue");
    return null;
  }
}
