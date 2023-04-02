﻿namespace Search.SearchMethod;
public static class BreadthFirstSearch
{
  // Structure require to represent current state in BFS
  private readonly record struct Metadata(int Name, List<int> Path);

  // Wrapper for BFS algorithm
  public static void Run(in double[][] matrix, in Program.SearchWith config)
  {
    List<int>? path = Algo(in matrix, in config);

    // Header
    const string header = "\nBreadth First Search Path";
    Print.Header.WithUnderline(header);

    const string separator = " --> ";
    Print.Path.WithSeparator(in path, separator);
  }

  // BFS Algorithm
  private static List<int>? Algo(in double[][] matrix, in Program.SearchWith config)
  {
    // Unpack search options
    int startNode = config.StartNode;
    int goalNode = config.GoalNode;

    // Initialize a list to keep track of visited nodes
    HashSet<int> visited = new();

    // Initialize first come first serve queue -> Initialize with starting condition
    Metadata start = new(startNode, new List<int>());
    Queue<Metadata> queue = new(); queue.Enqueue(start);

    // Recursively iterate over queue
    while (queue.Count > 0)
    {
      // Capture and pop first element in queue
      int node = queue.Peek().Name; // queue[0].Name
      List<int> nodePath = queue.Peek().Path; // queue[0].Path
      queue.Dequeue();
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

      // Otherwise append the children of current node to the queue
      List<int> children = Helper.GetChildrenOfNode(in matrix, in node);
      children = children.Where(x => !visited.Contains(x)).ToList(); // Filter visited nodes
      children.Sort(); // Sort to ensure [A, B, C] like order, A will eventually pop first

      // Skip to next item in queue for empty children set (duplicates or empty)
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
        queue.Enqueue(new Metadata(child, newPath));
      }

      if (Input.Read.DebugFlag()) { Console.WriteLine(); }
    }

    // Reaching here implies no path was found
    Print.Debug.Message(Print.About.Exhausted);
    return null;
  }
}
