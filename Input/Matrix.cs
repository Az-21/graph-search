namespace Search.Input;

public static class Matrix
{
  // Convert a comma separated string to array of double
  private static double[] ParseCsvAsDouble(in string input, in int n)
  {
    string[] values = input.Split(',');
    double[] doubles = new double[n];
    for (int i = 0; i < n; i++) { doubles[i] = double.Parse(values[i]); }

    return doubles;
  }

  public static double[][] ParseSquareMatrixCsv(in string path)
  {
    // Read CSV as stream of lines
    using StreamReader reader = new(path);

    // Read the first line to get the number of nodes -> create NxN matrix
    string firstLine = reader.ReadLine()!;
    int n = firstLine.Split(',').Length; // Number of nodes
    double[][] matrix = ZerosMatrix(in n);

    // Reset the stream reader to head
    ResetStreamReader(reader);

    // Parse as matrix<double>
    for (int row = 0; row < n; row++) // `!reader.EndOfStream()` is also fine
    {
      double[] values = ParseCsvAsDouble(reader.ReadLine()!, n);
      for (int i = 0; i < n; i++) { matrix[row][i] = values[i]; }
    }

    return matrix;
  }

  // Reset the stream reader to the start of the file
  private static void ResetStreamReader(StreamReader reader)
  {
    reader.DiscardBufferedData();
    reader.BaseStream.Seek(0, SeekOrigin.Begin);
  }

  // Helper function to initialize a zeros square matrix
  private static double[][] ZerosMatrix(in int size)
  {
    // Init Nx_ matrix (length of jagged arrays can vary)
    double[][] matrix = new double[size][];

    // Init 1xN row and attach it to row[i]
    for (int i = 0; i < size; i++) { matrix[i] = new double[size]; }

    return matrix;
  }
}
