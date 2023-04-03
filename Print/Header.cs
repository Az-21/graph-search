namespace Search.Print;
// Enum of all the search methods implemented
public enum HeaderOfSearchMethod { BFS, DFS, DLS, IDLS, UCS, GBFS, ASTAR };
public static class Header
{
  // Custom message -> decorate with underline
  public static void CustomMessageWithUnderline(in string header)
  {
    Console.WriteLine(header);
    Console.WriteLine(new string('-', header.Length));
  }

  // Preset messages for '<Method> Search Path'
  public static void PathWithUnderline(in HeaderOfSearchMethod header)
  {
    string fHeader = $"\n{GetHeader(header)} Search Path";
    Console.WriteLine(fHeader);
    Console.WriteLine(new string('-', fHeader.Length));
  }

  // Preset messages for '(Un)informed Search -> <Method> Search'
  public static void SectionHeader(in HeaderOfSearchMethod header, in bool isInformed)
  {
    string searchType = isInformed ? "Informed Search" : "Uninformed Search";
    string fHeader = $"{searchType} -> {GetHeader(in header)} Search";
    string decorator = new('=', fHeader.Length);

    Spacer();
    Console.WriteLine(decorator);
    Console.WriteLine(fHeader);
    Console.WriteLine(decorator);
  }

  // Function to expand enum to string presets
  private static string GetHeader(in HeaderOfSearchMethod header)
  {
    return header switch
    {
      HeaderOfSearchMethod.DFS => "Depth First",
      HeaderOfSearchMethod.BFS => "Breadth First",
      HeaderOfSearchMethod.DLS => "Depth Limited",
      HeaderOfSearchMethod.IDLS => "Iterative Depth Limited",
      HeaderOfSearchMethod.UCS => "Uniform Cost",
      HeaderOfSearchMethod.GBFS => "Greedy Best First",
      HeaderOfSearchMethod.ASTAR => "A*",
      _ => "Uncaught Print.HeaderOf enum passed",
    };
  }

  // Padding before section header
  private static void Spacer()
  {
    string divider = new('`', 100);
    Console.WriteLine($"\n\n{divider}\n{divider}\n\n");
  }
}
