namespace Search;

internal static class Program
{
  const bool DEBUG = true;
  static void Main()
  {
    // Read and parse adjacency matrix from CSV
    double[,] matrix = Input.Matrix.ParseSquareMatrixCsv("graph01.csv");
    if (DEBUG) { Track.Progress.ParsedSquareMatrix(in matrix); }
  }
}
