namespace Search;
public static class Program
{
  // Global constants for debugging and tracing steps
  public const bool DEBUG = true;
  public const bool USE_ALPHABET = true;

  // Structure of options and constraints
  public readonly record struct SearchWith(int StartNode, int GoalNode, int NxN);

  static void Main()
  {
    // Read and parse adjacency graph | Supports .CSV for adjacency matrix and .JSON for adjacency list
    const string filename = "matrix01.csv";
    double[][] matrix = Input.ReadGraph.WithFilename(filename);

    // Search configuration
    int n = matrix.GetLength(0); // NxN matrix dimension
    SearchWith options = new(StartNode: 0, GoalNode: 6, n);
    Input.Verify.Configuration(in matrix, in options);

    // Run search using all search methods
    SearchMethod.Handler.SearchUsingAllMethods(in matrix, options);
  }
}
