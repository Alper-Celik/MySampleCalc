
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Avalonia.Controls.Platform;

namespace MySampleCalc.Models;

public static class Calculator
{
  public static readonly List<char> MathOperationChars = ['+', '-', '/', '*'];
  public static bool IsMathOperation(char x) => MathOperationChars.Contains(x);

  public static readonly List<char> ParenChars = ['(', ')'];
  public static bool IsParenOperation(char x) => ParenChars.Contains(x);

  public static readonly List<char> NumberChars = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
  public static bool IsDigit(char x) => NumberChars.Contains(x);

  enum OperationType
  {
    Digit,
    MathOperation,
    Paren,
    delimitor
  }
  static OperationType GetOperationType(char x)
  {
    if (IsDigit(x))
    {
      return OperationType.Digit;
    }
    if (IsMathOperation(x))
    {
      return OperationType.MathOperation;
    }
    if (IsParenOperation(x))
    {
      return OperationType.Paren;
    }
    throw new ArgumentException($"'{x}'operation is invaild");
  }


  public static bool IsValidOperation(string operation) => operation != String.Empty;
  public static bool CouldBeValidOperation(string operation, char addition)
  {
    if (MathOperationChars.Contains(addition))
    {

    }

    if (addition == ')')
    {
      return LeftCloseParens(operation) > 0;
    }
    return true;
  }
  public static string Calculate(string operation) => "not implemented";

  private static List<string> DecomposeOperation(string operation)
  {
    List<string> operationParts = new();
    int lastPartStart = 0;
    bool delimitorCame = false;
    for (int i = 1; i < operation.Length; i++)
    {
      if (operation[i] == ' ')
      {
        continue;
      }
      if (GetOperationType(operation[i - 1]) == OperationType.Digit && operation[i] == '.')
      {
        if (delimitorCame)
        {
          throw new ArgumentException($"more than one delimitor at {i}");
        }

        delimitorCame = true;
        continue;
      }

      if (GetOperationType(operation[i - 1]) != GetOperationType(operation[i]))
      {
        operationParts.Add(operation.Substring(lastPartStart, i - lastPartStart));
        lastPartStart = i;
        delimitorCame = false;
      }
    }
    operationParts.RemoveAll((x) => x == "()");
    return operationParts;
  }

  private static int LeftCloseParens(string op)
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
    return numParenOpen;
  }
}