namespace Search.Network;
public static class Schema
{
  // Child of node -> name and movement cost
  public readonly record struct Child
  {
    public readonly int Name { get; init; }
    public readonly double Cost { get; init; }
  }

  // Node -> depth, h(n), and array of Child
  public readonly record struct Node
  {
    public readonly int Depth { get; init; }
    public readonly double Heuristic { get; init; }
    public readonly Child[] Children { get; init; }
  }
}
