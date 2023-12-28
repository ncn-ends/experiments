using Subjects.LeetCode;
using Xunit;

namespace Tests.LeetCode;

public class FibonacciTests
{
    [Theory]
    // [InlineData(0, null)]
    // [InlineData(-1, null)]
    // [InlineData(-100, null)]
    // [InlineData(7, 13)]
    // [InlineData(1, 1)]
    // [InlineData(2, 1)]
    [InlineData(40, 102334155)]
    public void TestCases(int n, int? expected)
    {
        var fib = new Fibonacci();
        var result = fib.FindByN(n);
        Assert.Equal(expected, result);
    }
}