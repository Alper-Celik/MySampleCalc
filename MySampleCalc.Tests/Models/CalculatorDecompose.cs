using MySampleCalc.Models;
using NUnit.Framework;

namespace MySampleCalc.Tests.Models;

public class DecomposeTests
{

    [TestCase("", new string[] { })]
    [TestCase("15 50", new string[] { "1550" })]
    [TestCase("15 +   50", new string[] { "15", "+", "50" })]
    [TestCase("15 ()( +  50-)", new string[] { "15", "(", "+", "50", "-", ")" })]
    [TestCase("15+50", new string[] { "15", "+", "50" })]
    public void Dceompose_Test(string op, string[] result)
    {
        Assert.That(Calculator.DecomposeOperation(op), Is.EqualTo(result));
    }
}
