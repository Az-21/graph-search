namespace Search.SearchMethod;
public enum UsingMethod { DepthFirstSearch }

public static class Handler
{
  public static void SearchWith(in double[][] matrix, in int startNode, in int goalNode, in UsingMethod usingMethod)
  {
    switch (usingMethod)
    {
      case UsingMethod.DepthFirstSearch:
        SearchMethod.DepthFirstSearch.Run(in matrix, in startNode, goalNode); break;

      default: throw new Exception("Unrecognized UsingMethod enum passed");
    }
  }
}
