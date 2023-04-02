namespace Search.SearchMethod;
public static class Handler
{
  public static void SearchUsingAllMethods(in double[][] matrix, in Program.SearchWith searchWith)
  {
    // Print parsed matrix | IF_DEF
    Print.Matrix.FromParsedCsv(in matrix);

    // DFS
    Spacer();
    Print.Header.WithUnderline("Uninformed Search -> i) Depth first search (DFS)");
    DepthFirstSearch.Run(in matrix, in searchWith);

    // BFS
    Spacer();
    Print.Header.WithUnderline("Uninformed Search -> ii) Breadth first search (BFS)");
    BreadthFirstSearch.Run(in matrix, in searchWith);

    // DLS
    Spacer();
    Print.Header.WithUnderline("Uninformed Search -> iii) Depth limited search (DLS)");
    DepthLimitedSearch.Run(in matrix, in searchWith);

    // ID-DFS
    Spacer();
    Print.Header.WithUnderline("Uninformed Search -> iv) Iterative deepening search (ID-DFS)");
    IterativeDeepeningSearch.Run(in matrix, in searchWith);
  }

  private static void Spacer()
  {
    string divider = new('>', 80);
    Console.WriteLine($"\n\n{divider}\n{divider}\n\n");
  }
}
