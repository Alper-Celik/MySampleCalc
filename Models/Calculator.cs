
using System;

namespace MySampleCalc.Models;

public static class Calculator
{
  public static bool IsValidOperation(string operation) => operation != String.Empty;
  public static bool CouldBeValidOperation(string operation) => operation.Length > 2;
  public static string Calculate(string operation) => "not implemented";
}