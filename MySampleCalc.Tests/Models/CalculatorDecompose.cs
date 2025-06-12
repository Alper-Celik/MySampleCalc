using MySampleCalc.Models;
using NUnit.Framework;

namespace MySampleCalc.Tests.Models;

public class DecomposeTests
{

    [TestCase("", new string[] { "" })]
    public void Dceompose_Test(string op, string[] result)
    {
        Assert.That(Calculator.DecomposeOperation(op).ToArray().Equals(result));
    }
}
