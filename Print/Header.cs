namespace Search.Print;
public static class Header
{
  public static void WithUnderline(in string header)
  {
    Console.WriteLine(header);
    Console.WriteLine(new string('-', header.Length));
  }
}
