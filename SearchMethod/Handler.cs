namespace Search.SearchMethod;
public static class Handler
{
  public static void SearchUsingAllMethods(in double[][] matrix, in Program.SearchWith searchWith)
  {
    // Unpack search options
    int startNode = searchWith.StartNode;
    int goalNode = searchWith.GoalNode;
    int depthLimit = searchWith.DepthLimit;

    // Print parsed matrix | IF_DEF
    Print.Matrix.FromParsedCsv(in matrix);

    // Uninformed Search -> i) Depth first search (DFS)
    SearchMethod.DepthFirstSearch.Run(in matrix, in startNode, in goalNode);
  }
}
