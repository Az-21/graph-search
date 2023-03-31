namespace Search.Input;

public static class Matrix
{
  // Construct path to "./Input/Graph/graph.csv"
  private static string GetCsvPath(in string csv)
  {
    const string r1 = "Input"; // Relative path root
    const string r2 = "Graphs"; // Relative path child

    // Combine method takes care of directory separator (Win\\, UNIX/) internally
    return Path.Combine(Directory.GetCurrentDirectory(), r1, r2, csv);
  }

  // Convert a comma separated string to array of double
  private static double[] ParseCsvAsDouble(in string input, in int n)
  {
    string[] values = input.Split(',');
    double[] doubles = new double[n];
    for (int i = 0; i < n; i++) { doubles[i] = double.Parse(values[i]); }

    return doubles;
  }

  public static double[,] ParseSquareMatrixCsv(in string csv = "graph01.csv")
  {
    // Read CSV as stream of lines
    using StreamReader reader = new(GetCsvPath(csv));

    // Read the first line to get the number of nodes -> create NxN matrix
    string firstLine = reader.ReadLine()!;
    int n = firstLine.Split(',').Length; // Number of nodes
    double[,] matrix = new double[n, n]; // NxN matrix [T: rectangular array]

    // Reset the stream reader to head
    ResetStreamReader(reader);

    // Parse as matrix<double>
    for (int row = 0; row < n; row++) // `!reader.EndOfStream()` is also fine
    {
      double[] values = ParseCsvAsDouble(reader.ReadLine()!, n);
      for (int i = 0; i < n; i++) { matrix[row, i] = values[i]; }
    }

    return matrix;
  }

  // Reset the stream reader to the start of the file
  private static void ResetStreamReader(StreamReader reader)
  {
    reader.DiscardBufferedData();
    reader.BaseStream.Seek(0, SeekOrigin.Begin);
  }
}
