namespace Search.Print;

public enum About
{
  Appended,
  Exhausted,
  PoppedNode,
  GoalReached,
  NoUniqueChild,
  AddedToVisited,
  AlreadyVisited,
  ListOfChildren,
  AppendedWithCost,
  AlreadyInOpenList,
  ReachedDepthLimit,
  AppendedWithHeuristic,
  PoppedNodeWithMinCuCost,
  PoppedNodeWithMinHnCost,
  FoundNodeWithLowerCuCost,
  CostHigherThanCurrentBest,
};

public static class Debug
{
  // Debug messages which need the current `node` (and optionally depth) to print a message
  public static void Message(in int node, in About message, in int depth = 0)
  {
    if (!Input.Read.DebugFlag()) { return; }
    string id = Convert.NodeName.ConvertToNumberOrAlphabet(node);

    switch (message)
    {
      case Print.About.Appended:
        Console.WriteLine($"\t+ Appending `Node {id}` to the stack/queue"); break;

      case Print.About.GoalReached:
        Console.WriteLine($"\t* `Node {id}` is the goal"); break;

      case Print.About.PoppedNode:
        Console.WriteLine($"Popped `Node {id}` from the stack/queue"); break;

      case Print.About.AlreadyInOpenList:
        Console.WriteLine($"\tx Not appending `Node {id}` because it is already in open list"); break;

      case Print.About.AlreadyVisited:
        Console.WriteLine($"\t! `Node {id}` is already in visited list\n"); break;

      case Print.About.AddedToVisited:
        Console.WriteLine($"\t> Added `Node {id}` to the visited list"); break;

      case Print.About.NoUniqueChild:
        Console.WriteLine($"\t~ `Node {id}` does not have unique children\n"); break;

      case Print.About.ReachedDepthLimit:
        Console.WriteLine($"\tx `Node {id}` is at depth limit [d={depth}] -> not expanding further\n");
        break;
    }
  }

  // Debug messages for search methods which use filtering based on cumulative cost
  public static void Message(in int node, in About message, in double cost, in double best = 0)
  {
    if (!Input.Read.DebugFlag()) { return; }
    string id = Convert.NodeName.ConvertToNumberOrAlphabet(node);

    switch (message)
    {
      case Print.About.AppendedWithCost:
        Console.WriteLine($"\t+ Appended `Node {id}` with cumulative cost of {cost} (previous best was {best})");
        break;

      case Print.About.AppendedWithHeuristic:
        Console.WriteLine($"\t+ Appended `Node {id}` with heuristic of h({id}) = {cost}");
        break;

      case Print.About.CostHigherThanCurrentBest:
        Console.WriteLine($"\tx Not appending `Node {id}` with cumulative cost of {cost} because current best is {best}");
        break;

      case Print.About.PoppedNodeWithMinCuCost:
        Console.WriteLine($"Popped `Node {id}` from the stack/queue because it has lowest cumulative cost of {cost}");
        break;

      case Print.About.FoundNodeWithLowerCuCost:
        Console.WriteLine($"\t> `Node {id}` with cost of {cost} is a dead end because another path with cost of {best} was found");
        break;

      case Print.About.PoppedNodeWithMinHnCost:
        Console.WriteLine($"Popped `Node {id}` from the stack because it has lowest heuristic value of {cost}");
        break;
    }
  }

  // Debug messages which describe neighbors of a node
  public static void Message(in int parent, in List<int> children, in About message)
  {
    if (!Input.Read.DebugFlag()) { return; }
    string fParent = Convert.NodeName.ConvertToNumberOrAlphabet(parent);
    List<string> fChildren = Convert.NodeName.ConvertListToNumberOrAlphabet(children);

    switch (message)
    {
      case Print.About.ListOfChildren:
        Console.WriteLine($"\t> `Node {fParent}` has {fChildren.Count} children: {{ {string.Join(", ", fChildren)} }}");
        break;
    }
  }

  // Debug messages which don't need the current `node` to print a message
  public static void Message(in About message)
  {
    if (!Input.Read.DebugFlag()) { return; }

    switch (message)
    {
      case Print.About.Exhausted:
        Console.WriteLine("Exhausted stack/queue. Terminating search."); break;
    }
  }

  public static void OpenAndVisitedList(in HashSet<int> open, in HashSet<int> visited)
  {
    if (!Input.Read.DebugFlag()) { return; }

    List<string> fOpen = Convert.NodeName.ConvertListToNumberOrAlphabet(open);
    List<string> fVisited = Convert.NodeName.ConvertListToNumberOrAlphabet(visited);
    Console.WriteLine($"Closed list: {{ {string.Join(", ", fVisited)} }}");
    Console.WriteLine($"Open list: {{ {string.Join(", ", fOpen)} }}");
  }

  // Debug messages about the heuristic values and current best estimations
  public static void CurrentBestCumulative(in Dictionary<int, double> kvp)
  {
    if (!Input.Read.DebugFlag()) { return; }

    Console.WriteLine("\nCurrent best cumulative cost of each node");
    foreach (KeyValuePair<int, double> item in kvp)
    {
      string node = Convert.NodeName.ConvertToNumberOrAlphabet(item.Key);
      Console.Write($"{node}: {item.Value}; ");
    }
    Console.WriteLine();
  }

  public static void HeuristicValuesOfOpenNodes(in List<SearchMethod.GreedyBestFirstSearch.MetadataGBFS> stack)
  {
    if (!Input.Read.DebugFlag()) { return; }

    Console.WriteLine("\nHeuristic values of currently open nodes");
    foreach (var item in stack)
    {
      string node = Convert.NodeName.ConvertToNumberOrAlphabet(item.Name);
      Console.Write($"{node}: {item.Heuristic}; ");
    }
    Console.WriteLine();
  }
}
