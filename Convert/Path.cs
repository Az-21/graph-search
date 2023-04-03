namespace Search.Convert;
public static class Path
{
  public static double ToCost(in double[][] matrix, in List<int> path)
  {
    double sum = 0;
    int nodes = path.Count;

    for (int i = 0; i < nodes - 1; i++)
    {
      int origin = path[i];
      int destination = path[i + 1];
      sum += matrix[origin][destination];

      Print.Path.TravelCost(in matrix, in origin, in destination);
      if (Input.Read.DebugFlag() && i != nodes - 2) { Console.Write(" + "); }
    }

    return sum;
  }
}
