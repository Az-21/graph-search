namespace Search.Input;
// This is to hide the 'unreachable code' warning. Not the best practice, but *it is what it is*.
public static class Read
{
  public static bool DebugFlag() => Program.DEBUG;
  public static bool AlphabetFlag() => Program.USE_ALPHABET;
}
