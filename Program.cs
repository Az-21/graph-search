using Search.SearchMethod;

namespace Search;
internal static class Program
{
  // Global constants for debugging and tracing steps
  public const bool DEBUG = true;
  public const bool USE_ALPHABET = true;

  static void Main()
  {
    // Read and parse adjacency matrix from CSV
    double[][] matrix = Input.Matrix.ParseSquareMatrixCsv("graph01.csv");

    // Run search
    const UsingMethod method = SearchMethod.UsingMethod.DepthFirstSearch;
    SearchMethod.Handler.SearchWith(in matrix, 0, 6, method);
  }
}
