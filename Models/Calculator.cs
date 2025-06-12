
using System;

namespace MySampleCalc.Models;

public static class Calculator
{
  public static bool IsValidOperation(string operation) => operation != String.Empty;
  public static bool CouldBeValidOperation(string operation, char addition)
  {
    if (addition == ')')
    {
      return CanAddParenClose(operation);
    }
    return true;
  }
  public static string Calculate(string operation) => "not implemented";

  private static bool CanAddParenClose(string op)
  {
    int numParenOpen = 0;
    foreach (var ch in op)
    {
      if (ch == '(')
      {
        numParenOpen++;
      }
      else if (ch == ')')
      {
        numParenOpen--;
      }
    }
    return numParenOpen > 0;
  }
}