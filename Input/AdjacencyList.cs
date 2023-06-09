﻿using System.Text.Json;

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
    nodes.RemoveAt(nodes.Count - 1); // Pop last element, it contains heuristic data
    int size = nodes.Count;

    // Generate NxN matrix -> fill M(i,j) with 1 if an edge exists
    double[][] matrix = new double[size][];
    for (int i = 0; i < size; i++)
    {
      matrix[i] = new double[size];
      string[] edges = adjList[nodes[i]].Split(",", StringSplitOptions.TrimEntries);
      foreach (string edge in edges)
      {
        if (string.IsNullOrWhiteSpace(edge)) { continue; }
        int j = nodes.IndexOf(edge);

        // Check for unexpected adjacency matrix (eg: Starting with node "S"->18 instead of "A"->0)
        if (j == -1)
        {
          Console.WriteLine("Please ensure adjacency list is given in alphabetical order.");
          System.Environment.Exit(5);
        }
        matrix[i][j] = 1;
      }
    }

    // Ensure the number of nodes and the number of heuristic values match
    string[] heuristic = adjList["H(N)"].Split(",", StringSplitOptions.TrimEntries);
    int hSize = heuristic.Length;
    if (hSize != size)
    {
      Console.WriteLine($"[ FATAL ]\tExpected {size} heuristic values, got {hSize}");
      System.Environment.Exit(6);
    }

    // Fill heuristic values in the primary diagonal of the matrix
    double[] h = new double[hSize];
    h = Array.ConvertAll(heuristic, double.Parse);
    for (int i = 0; i < size; i++) { matrix[i][i] = h[i]; }

    return matrix;
  }
}
