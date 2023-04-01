namespace Search.Track;
public static class Progress
{
#pragma warning disable CS0162 // Unreachable code detected
  public static void ParsedSquareMatrix(in double[][] matrix)
  {
    if (!Program.DEBUG) { return; }

    // Get dimension of square matrix
    int n = matrix.GetLength(0);

    // Header
    string header = $"Parsed CSV as a(n) {n}x{n} matrix";
    Print.Header.WithUnderline(header);

    // NxN matrix
    for (int i = 0; i < n; i++)
    {
      for (int j = 0; j < n; j++)
      {
        Console.Write(matrix[i][j].ToString("00.0"));
        Console.Write("  ");
      }
      Console.WriteLine("\n");
    }
  }
#pragma warning restore CS0162 // Unreachable code detected
}
