namespace Search.Input;
public static class ReadGraph
{
  // Construct path to "./Input/Graph/filename"
  private static string GetGraphPath(in string filename)
  {
    const string source = "Graphs"; // Folder containing all the graphs as .CSV || .JSON

    // Combine method takes care of directory separator (Win\\, UNIX/) internally
    string path = Path.Combine(Directory.GetCurrentDirectory(), source, filename);

    // Check if a file at specified path exists
    if (!File.Exists(path))
    {
      Console.WriteLine($"[ FATAL ]\tFile `{filename}` does not exist in `Graphs` folder");
      System.Environment.Exit(3);
    }

    return path;
  }

  public static double[][] WithFilename(in string filename)
  {
    string path = GetGraphPath(filename);

    // Adjacency list is declared with .JSON
    if (filename.EndsWith(".json")) { return AdjacencyList.ConvertToMatrix(path); }

    // Adjacency matrix is declared with .CSV
    if (filename.EndsWith(".csv")) { return Matrix.ParseSquareMatrixCsv(path); }

    // Reaching here implies that file is neither CSV nor JSON
    Console.WriteLine("[ FATAL ]\tEnsure either JSON or CSV is provided as graph source");
    System.Environment.Exit(3);
    return Array.Empty<double[]>(); // Just to satisfy compiler
  }
}
