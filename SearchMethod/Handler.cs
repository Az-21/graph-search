namespace Search.SearchMethod;
public static class Handler
{
  public static void SearchUsingAllMethods(in double[][] matrix, in Program.SearchWith searchWith)
  {
    // Print parsed matrix | IF_DEF
    Print.Matrix.FromParsedCsv(in matrix);

    // Search configuration/constraints
    Print.Configuration.Details(in searchWith);

    // DFS
    Print.Header.SectionHeader(Print.HeaderOfSearchMethod.DLS, false);
    DepthFirstSearch.Run(in matrix, in searchWith);

    // BFS
    Print.Header.SectionHeader(Print.HeaderOfSearchMethod.BFS, false);
    BreadthFirstSearch.Run(in matrix, in searchWith);

    // DLS
    const int depthLimit = 4;
    Print.Header.SectionHeader(Print.HeaderOfSearchMethod.DLS, false);
    DepthLimitedSearch.Run(in matrix, in searchWith, depthLimit);

    // ID-DFS (IDLS)
    Print.Header.SectionHeader(Print.HeaderOfSearchMethod.IDLS, false);
    IterativeDeepeningSearch.Run(in matrix, in searchWith);

    // UCS
    Print.Header.SectionHeader(Print.HeaderOfSearchMethod.UCS, false);
    UniformCostSearch.Run(in matrix, in searchWith);

    // GBFS
    Print.Header.SectionHeader(Print.HeaderOfSearchMethod.GBFS, isInformed: true);
    GreedyBestFirstSearch.Run(in matrix, in searchWith);

    // A*
    Print.Header.SectionHeader(Print.HeaderOfSearchMethod.ASTAR, isInformed: true);
    AStarSearch.Run(in matrix, in searchWith);
  }
}
