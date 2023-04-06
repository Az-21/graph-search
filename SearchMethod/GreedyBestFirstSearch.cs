namespace Search.SearchMethod;
public static class GreedyBestFirstSearch
{
  // Structure require to represent current state in GBFS
  public readonly record struct MetadataGBFS(int Name, double Heuristic, List<int> Path);

  // Wrapper for GBFS algorithm
  public static void Run(in double[][] matrix, in Program.SearchWith config)
  {
    List<int>? path = FindPathByGBFS(in matrix, in config);
    Print.Header.PathWithUnderline(Print.HeaderOfSearchMethod.GBFS);
    Print.Path.WithSeparator(in path);
    Helper.PrintPathCost(in matrix, in path);
  }

  // UCS Algorithm
  private static List<int>? FindPathByGBFS(in double[][] matrix, in Program.SearchWith config)
  {
    // Unpack search options
    int startNode = config.StartNode;
    int goalNode = config.GoalNode;

    // Initialize a list to keep track of visited nodes and currently open nodes
    HashSet<int> visited = new();
    HashSet<int> open = new() { startNode };

    // Parse heuristic table from adjacency matrix
    double[] h = Helper.GetHeuristics(in matrix);

    // Initialize lowest cumulative cost node first stack -> Initialize with starting condition
    MetadataGBFS start = new(startNode, h[startNode], new List<int>());
    List<MetadataGBFS> stack = new() { start };

    // Recursively iterate over stack
    while (stack.Count > 0)
    {
      // Failsafe for infinite loop
      if (stack.Count > 100)
      {
        Console.WriteLine("Stack size reached limit of 100 elements --> Possibly caught in infinite loop");
        Console.WriteLine("Consider verifying admissibility and consistency of heuristic function");
        return null;
      }

      // Print the h(n) of currently open nodes (available to explore)
      Print.Debug.HeuristicValuesOfOpenNodes(in stack);

      // Pop the node with lowest h(n) in the stack
      stack = stack.OrderBy(x => x.Heuristic).ToList(); // Ensures stack[0] has lowest h(n)
      int node = stack[0].Name;
      double heuristic = stack[0].Heuristic;
      List<int> nodePath = stack[0].Path;
      stack.RemoveAt(0);
      Print.Debug.Message(in node, Print.About.PoppedNodeWithMinHnCost, heuristic);

      // Check for goal state
      if (node == goalNode)
      {
        Print.Debug.Message(in node, Print.About.GoalReached);
        return nodePath.Append(node).ToList();
      }

      // Otherwise, append the node to the visited list and remove from open list
      open.Remove(node);
      visited.Add(node);
      Print.Debug.Message(in node, Print.About.AddedToVisited);

      // Otherwise append the children of current node to the stack
      List<int> children = Helper.GetChildrenOfNode(in matrix, in node); // Sorting is **not** required
      children = children.Where(x => !visited.Contains(x)).ToList(); // Filter out visited nodes

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
        // Check if the child already exists in open list
        if (open.Contains(child))
        {
          Print.Debug.Message(in child, Print.About.AlreadyInOpenList);
          continue;
        }

        // Add to stack if the child is not present in open list
        open.Add(child);
        Print.Debug.Message(in child, Print.About.AppendedWithHeuristic, h[child]);
        stack.Add(new MetadataGBFS(child, h[child], newPath));
      }

      if (Input.Read.DebugFlag()) { Console.WriteLine(); }
    }

    // Reaching here implies no path was found
    Print.Debug.Message(Print.About.Exhausted);
    return null;
  }
}
