namespace Search.Print;
public static class Matrix
{
  public static void FromParsedCsv(in double[][] matrix, in int n)
  {
    if (!Input.Read.DebugFlag()) { return; }

    // Header
    string header = $"Parsed CSV as a {n}x{n} matrix";
    Print.Header.CustomMessageWithUnderline(header);

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
