namespace Search.Convert;
public static class NodeName
{
  // Base10 to Base26 | 0->A, 1->B, 25->Z, 26->AA
  public static string ConvertToNumberOrAlphabet(in int number)
  {
    if (!Input.Read.AlphabetFlag()) { return number.ToString(); }
    return FromNumberToAlphabet(number);
  }

  public static List<string> ConvertListToNumberOrAlphabet(in List<int> list)
  {
    if (!Input.Read.AlphabetFlag()) { return list.ConvertAll(x => x.ToString()); }
    return list.ConvertAll(Convert.NodeName.FromNumberToAlphabet);
  }

  public static List<string> ConvertListToNumberOrAlphabet(in HashSet<int> list)
  {
    return list.Select(Convert.NodeName.FromNumberToAlphabet).ToList();
  }

  private static string FromNumberToAlphabet(int number)
  {
    string result = string.Empty;

    while (number >= 0)
    {
      int remainder = number % 26;
      result = (char)(65 + remainder) + result;
      number = (number / 26) - 1;
    }

    return result;
  }

  // Base26 to Base10 | A->0, B->1, Z->25, AA->26
  public static int FromAlphabetToNumber(in string alphabet)
  {
    int result = 0;
    int power = 0;

    foreach (char c in alphabet)
    {
      int value = c - 'A';
      result += (int)Math.Pow(26, power) * (value + 1);
      power++;
    }

    return result - 1;
  }
}
