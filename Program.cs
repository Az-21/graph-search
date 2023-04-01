namespace Search;
public static class Program
{
  // Global constants for debugging and tracing steps
  public const bool DEBUG = true;
  public const bool USE_ALPHABET = true;

  // Structure of options and constraints
  public readonly record struct SearchWith(int StartNode, int GoalNode, int DepthLimit);

  static void Main()
  {
    // Read and parse adjacency matrix from CSV
    double[][] matrix = Input.Matrix.ParseSquareMatrixCsv("graph01.csv");

    // Run search using all search methods
    SearchWith options = new(StartNode: 0, GoalNode: 6, DepthLimit: 1);
    SearchMethod.Handler.SearchUsingAllMethods(in matrix, options);
  }
}
