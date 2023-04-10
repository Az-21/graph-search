using System.Text.Json;

namespace Search.Input;
public static class Config
{
  public static SearchParameters ParseConfig()
  {
    // Construct path to `config.json`
    // Combine method takes care of directory separator (Win\\, UNIX/) internally
    string path = Path.Combine(Directory.GetCurrentDirectory(), "Graphs", "config.json");

    // Check if `config.json` exists
    if (!File.Exists(path))
    {
      Console.WriteLine("[ FATAL ]\tFile `config.json` does not exist in `Graphs` folder. Please re-download app.");
      System.Environment.Exit(4);
    }

    // Parse search options/parameters from `config.json`
    string rawJson = File.ReadAllText(path).Trim();
    return DeserializeSearchParameters(rawJson);
  }

  // JSON schema
  private readonly record struct UnprocessedSearchParameters(string? GraphName, string? StartNode, string? GoalNode);
  public readonly record struct SearchParameters(string GraphName, int StartNode, int GoalNode);

  // Deserialize and then process start node and goal node
  private static SearchParameters DeserializeSearchParameters(string jsonString)
  {
    // Deserialize as is | This is to provide flexibility of setting nodes by number XOR alphabet
    UnprocessedSearchParameters json = JsonSerializer.Deserialize<UnprocessedSearchParameters>(jsonString);

    // Ensure all parameters are defined
    if (string.IsNullOrEmpty(json.GraphName) || string.IsNullOrEmpty(json.StartNode) || string.IsNullOrEmpty(json.GoalNode))
    {
      Console.WriteLine("[ FATAL ]\tError in `config.json` schema. Ensure PascalCase keys and corresponding non-null values.");
      System.Environment.Exit(5);
    }

    // Convert to int based nodes
    int[] parsed = new int[2];
    string[] nodes = new string[2] { json.StartNode.ToUpper(), json.GoalNode.ToUpper() }; // ToUpper ensures A->0 mapping
    for (int i = 0; i < 2; i++)
    {
      bool isBase10 = NodeContainsOnlyNumbers(nodes[i]); // 0, 1, 2, ... , 9
      bool isBase26 = NodeContainsOnlyCharacters(nodes[i]); // A, B, C, ... , Z, AA ... AAA ...

      // Invalid config -> is neither base10 nor base26
      if (!isBase10 && !isBase26)
      {
        Console.WriteLine($"[ FATAL ]\tInvalid `config.json`. Expected alphabet XOR number. Got `{nodes[i]}`.");
        System.Environment.Exit(4);
      }

      if (isBase10) { parsed[i] = int.Parse(nodes[i]); }
      if (isBase26) { parsed[i] = Convert.NodeName.FromAlphabetToNumber(nodes[i]); }
    }

    return new SearchParameters(json.GraphName, parsed[0], parsed[1]);
  }

  private static bool NodeContainsOnlyNumbers(in string input)
  {
    foreach (char c in input) { if (!char.IsDigit(c)) { return false; } }
    return true; // Reaching here implies input string only contains numbers
  }

  private static bool NodeContainsOnlyCharacters(in string input)
  {
    foreach (char c in input) { if (!char.IsLetter(c)) { return false; } }
    return true; // Reaching here implies input string only contains characters
  }
}
