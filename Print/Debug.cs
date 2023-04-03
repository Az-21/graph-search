﻿namespace Search.Print;

public enum About
{
  Appended,
  Exhausted,
  PoppedNode,
  GoalReached,
  NoUniqueChild,
  AddedToVisited,
  AlreadyVisited,
  AppendedWithCost,
  ReachedDepthLimit,
  PoppedNodeWithMinCuCost,
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
        Console.WriteLine($"\t+ Appended `Node {id}` with cumulative cost of {cost}");
        Console.WriteLine($"\t  Previous best was {best}");
        break;

      case Print.About.CostHigherThanCurrentBest:
        Console.WriteLine($"\tx Not appending `Node {id}` with cumulative cost of {cost}");
        Console.WriteLine($"\t  Current best is {best}");
        break;

      case Print.About.PoppedNodeWithMinCuCost:
        Console.WriteLine($"Popped `Node {id}` from the stack/queue because it has lowest cumulative sum of {cost}");
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
}
