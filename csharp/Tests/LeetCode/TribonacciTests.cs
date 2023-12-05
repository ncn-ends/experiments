using System.Threading.Tasks;
using Subjects.LeetCode;
using Xunit;

namespace Tests.LeetCode;

public class TribonacciTests
{
    [Theory]
    [InlineData(25, 1389537)]
    [InlineData(4, 4)]
    public async Task ByNTests(int n, int expected)
    {
        var actual = Tribonacci.ByN(n);
        Assert.Equal(actual: actual, expected: expected);
    }
}