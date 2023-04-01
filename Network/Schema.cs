namespace Search.Network;
public static class Schema
{
  // Child of node -> name and movement cost
  public readonly record struct Child
  {
    public int Name { get; init; }
    public double Cost { get; init; }
  }

  // Node -> depth, h(n), and array of Child
  public readonly record struct Node
  {
    public int Depth { get; init; }
    public double Heuristic { get; init; }
    public Child[] Children { get; init; }
  }
}
