using System.Text.Json;

namespace Search.Input;
public static class AdjacencyList
{
  public static double[][] ConvertToMatrix(in string path)
  {
    // Read JSON from specified path
    string json;
    using (StreamReader sr = new(path))
    {
      json = sr.ReadToEnd();
    }

    // Preprocess JSON
    json = json.Trim();
    json = json.ToUpperInvariant();

    // De-serialize the adjacency list JSON
    Dictionary<string, string>? adjList = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    if (adjList is null)
    {
      Console.WriteLine("[ FATAL ]\tCannot parse adjacency matrix. Ensure JSON is formatted correctly.");
      System.Environment.Exit(2);
    }

    // Even isolated and one-way nodes need to be present for this to work.
    List<string> nodes = adjList.Keys.ToList();
    int size = nodes.Count;

    // Generate NxN matrix -> fill M(i,j) with 1 if an edge exists
    double[][] matrix = new double[size][];
    for (int i = 0; i < size; i++)
    {
      matrix[i] = new double[size];
      string[] edges = adjList[nodes[i]].Split(",", StringSplitOptions.TrimEntries);
      foreach (string edge in edges)
      {
        int j = nodes.IndexOf(edge);
        matrix[i][j] = 1;
      }
    }

    return matrix;
  }
}
