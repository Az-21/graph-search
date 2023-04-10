namespace Search.Input;
public static class Verify
{
  public static void Configuration(in Program.SearchWith config)
  {
    int n = config.NxN;

    // Verify start node
    if (config.StartNode >= n)
    {
      Console.WriteLine("[ FATAL ]\tStart node must be a member of given graph. Terminating.");
      System.Environment.Exit(1);
    }

    // Verify goal node
    if (config.GoalNode >= n)
    {
      Console.WriteLine("[ FATAL ]\tGoal node must be a member of given graph. Terminating.");
      System.Environment.Exit(1);
    }
  }
}
