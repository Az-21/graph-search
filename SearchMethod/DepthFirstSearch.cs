namespace Search.SearchMethod;
internal readonly record struct NodeMetadata(int Name, List<int> Path);

public static class DepthFirstSearch
{
  public static void Run(in double[][] matrix, in int startNode, in int goalNode)
  {
    List<int>? path = Algo(in matrix, in startNode, in goalNode);

    // Handle null type => Implies no path was found
    if (path is null) { Console.WriteLine("No path was found"); return; }

    // Otherwise, print the path received
    const string header = "Depth First Search Path";
    Console.WriteLine(header);
    Console.WriteLine(new string('-', header.Length));

    // Conversion IF_DEF
    if (!Program.USE_ALPHABET) { Console.WriteLine(string.Join(" --> ", path)); }
    List<string> alphabetPath = path.ConvertAll(Convert.NodeName.FromNumberToAlphabet);
    Console.WriteLine(string.Join(" --> ", alphabetPath));
  }
  public static List<int>? Algo(in double[][] matrix, in int startNode, in int goalNode)
  {
    // Print parsed matrix | IF_DEF
    if (Program.DEBUG) { Track.Progress.ParsedSquareMatrix(in matrix); }

    // Initialize a list to keep track of visited nodes
    List<int> visited = new();

    // Initialize last come first serve stack -> Initialize with starting condition
    NodeMetadata start = new(startNode, new List<int>());
    List<NodeMetadata> stack = new() { start };

    // Recursively iterate over stack
    while (stack.Count > 0)
    {
      // Capture and pop last
      int iLast = stack.Count - 1;
      int nodeName = stack[iLast].Name;
      List<int> nodePath = stack[iLast].Path;
      stack.RemoveAt(iLast);

      // IF_DEF
      string fNodeName = nodeName.ToString();
      if (Program.USE_ALPHABET) { fNodeName = Convert.NodeName.FromNumberToAlphabet(nodeName); }

      // Check if the node already exists in the visited list
      if (visited.Contains(nodeName))
      {
        // IF_DEF
        if (!Program.DEBUG) { continue; }
        Console.WriteLine($"  Node {fNodeName} is already in visited list. Skipping.");
        continue;
      }

      // Otherwise, append the node to the visited list
      visited.Add(nodeName);
      if (Program.DEBUG) { Console.WriteLine($"Now visiting Node {fNodeName}."); }

      // Check for goal state
      if (nodeName == goalNode)
      {
        if (Program.DEBUG) { Console.WriteLine($"  Node {fNodeName} is the goal.\n"); }
        return nodePath.Append(nodeName).ToList();
      }

      // Otherwise append the children of current node to the stack
      List<int> children = Helper.GetChildrenOfNode(in matrix, in nodeName);
      children.Sort(); children.Reverse(); // Sort and reverse to ensure [C, B, A] like order, A will pop next
      children = children.Where(x => !visited.Contains(x)).ToList(); // Filter out visited nodes

      // IF_DEF for empty children set (duplicates or empty)
      if (Program.DEBUG && children.Count == 0)
      {
        Console.WriteLine($"  Node {fNodeName} doesn't have unique children.");
      }

      // Append the current node to the path => newPath = oldPath + currentNode
      List<int> newPath = new();
      newPath.AddRange(nodePath); newPath.Add(nodeName);
      foreach (int child in children)
      {
        stack.Add(new NodeMetadata(child, newPath));

        // IF_DEF
        if (!Program.DEBUG) { continue; }
        string fChildName = child.ToString();
        if (Program.USE_ALPHABET) { fChildName = Convert.NodeName.FromNumberToAlphabet(child); }
        Console.WriteLine($"  Appending Node {fChildName} to the stack");
      }

      Console.WriteLine();
    }

    // Reaching here implies no path was found
    return null;
  }
}
