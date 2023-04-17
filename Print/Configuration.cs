namespace Search.Print;
public static class Configuration
{
  public static void Details(in Input.Config.SearchParameters config)
  {
    Console.WriteLine($"Graph name -> {config.GraphName}");
    Console.WriteLine($"Start node -> {Convert.NodeName.ConvertToNumberOrAlphabet(config.StartNode)}");
    Console.WriteLine($"Goal node  -> {Convert.NodeName.ConvertToNumberOrAlphabet(config.GoalNode)}\n");
  }
}
