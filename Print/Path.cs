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
}
