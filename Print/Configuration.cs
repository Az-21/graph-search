namespace Search.Print;
public static class Configuration
{
  public static void Details(in Program.SearchWith config)
  {
    Console.WriteLine($"Start node -> {Convert.NodeName.ConvertToNumberOrAlphabet(config.StartNode)}");
    Console.WriteLine($"Goal node -> {Convert.NodeName.ConvertToNumberOrAlphabet(config.GoalNode)}");
  }
}
