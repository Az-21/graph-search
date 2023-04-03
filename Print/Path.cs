namespace Search.Print;
public static class Path
{
  private const string separator = " --> ";

  public static void WithSeparator(in List<int>? path)
  {
    // Handle null type => Implies no path was found
    if (path is null) { Console.WriteLine("No path was found"); return; }

    // Print formatted path
    List<string> fPath = Convert.NodeName.ConvertListToNumberOrAlphabet(in path);
    Console.WriteLine(string.Join(separator, fPath));
  }

  // Debug message for cost involved with traveling from one node to another
  public static void TravelCost(in double[][] matrix, in int origin, in int destination)
  {
    if (!Input.Read.DebugFlag()) { return; }

    string fOrigin = Convert.NodeName.ConvertToNumberOrAlphabet(origin);
    string fDestination = Convert.NodeName.ConvertToNumberOrAlphabet(destination);
    Console.Write($"/ {fOrigin} >> {fDestination} = {matrix[origin][destination]} /");
  }
}
