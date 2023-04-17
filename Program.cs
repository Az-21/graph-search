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
    // Get search options/parameters from `config.json`
    Input.Config.SearchParameters config = Input.Config.ParseConfig();

    // Read and parse adjacency graph | Supports .CSV for adjacency matrix and .JSON for adjacency list
    double[][] matrix = Input.ReadGraph.WithFilename(config.GraphName);

    // Search configuration
    int n = matrix.GetLength(0); // NxN matrix dimension
    SearchWith options = new(StartNode: config.StartNode, GoalNode: config.GoalNode, n);
    Input.Verify.Configuration(in options);

    // Print the parsed config
    Print.Configuration.Details(in config);

    // Run search using all search methods
    SearchMethod.Handler.SearchUsingAllMethods(in matrix, options);

    // Prompt to exit
    Console.WriteLine("\n\n\nPress any key to exit...");
    Console.Read();
  }
}
