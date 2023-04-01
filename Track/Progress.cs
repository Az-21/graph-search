namespace Search.Track;
public static class Progress
{
  public static void ParsedSquareMatrix(in double[][] matrix)
  {
    // Get dimension of square matrix
    int n = matrix.GetLength(0);

    // Header
    string header = $"Parsed CSV as a(n) {n}x{n} matrix";
    string underline = new('-', header.Length);
    Console.WriteLine($"{header}\n{underline}\n");

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
}
