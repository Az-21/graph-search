namespace Search.Convert;
public static class Path
{
  public static double ToCost(in double[][] matrix, in List<int> path)
  {
    double sum = 0;
    for (int i = 0; i < path.Count - 1; i++)
    {
      int origin = path[i];
      int destination = path[i + 1];

      sum += matrix[origin][destination];
    }

    return sum;
  }
}
