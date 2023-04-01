namespace Search.Print;
public static class Debug
{
  // Primary debug wrapper
  private static string? ProcessNodeName(in int nodeName)
  {
#pragma warning disable CS0162 // Unreachable code detected
    if (!Program.DEBUG) { return null; }
    return Convert.NodeName.ConvertToNumberOrAlphabet(nodeName);
#pragma warning restore CS0162 // Unreachable code detected
  }

  // Print the current node (which was just popped from stack)
  public static void PoppedNode(in int nodeName)
  {
    string? fNodeName = ProcessNodeName(in nodeName);
    if (fNodeName is null) { return; }
    Console.WriteLine($"Now visiting `Node {fNodeName}` from the stack");
  }

  // Print skipping message -> node is already visited
  public static void AlreadyVisited(in int nodeName)
  {
    string? fNodeName = ProcessNodeName(in nodeName);
    if (fNodeName is null) { return; }
    Console.WriteLine($"\t! `Node {fNodeName}` is already in visited list -> Skipping");
  }

  // Print message for vising a node which was not found in visited list
  public static void NowVisiting(in int nodeName)
  {
    string? fNodeName = ProcessNodeName(in nodeName);
    if (fNodeName is null) { return; }
    Console.WriteLine($"\t> Added `Node {fNodeName}` to the visited list");
  }

  // Print goal found message
  public static void GoalReached(in int nodeName)
  {
    string? fNodeName = ProcessNodeName(in nodeName);
    if (fNodeName is null) { return; }
    Console.WriteLine($"\t* `Node {fNodeName}` is the goal");
  }

  // Print message about node not having any unique/undiscovered children
  public static void NoUniqueChildren(in int nodeName)
  {
    string? fNodeName = ProcessNodeName(in nodeName);
    if (fNodeName is null) { return; }
    Console.WriteLine($"\t~ `Node {fNodeName}` does not have unique children\n");
  }

  // Print children of node
  public static void AppendedNode(in int childName)
  {
    string? fChildName = ProcessNodeName(in childName);
    if (fChildName is null) { return; }
    Console.WriteLine($"\t+ Appending `Node {fChildName}` to the stack");
  }
}
