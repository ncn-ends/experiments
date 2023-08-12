using System.Threading.Tasks;
using Subjects.RandomProblems;
using Xunit;

namespace Tests.RandomProblems;

public class CanSumTests
{
    [Theory]
    [InlineData(7, new[] {5, 3, 4, 7}, true)]
    [InlineData(7, new[] {2, 4}, false)]
    [InlineData(7, new[] {2, 3}, true)]
    [InlineData(8, new[] {2, 3, 5}, true)]
    [InlineData(100, new[] {7, 14}, false)]
    public async Task Tests(int target, int[] numbers, bool expected)
    {
        var actual = CanSum.Do(target, numbers);
        Assert.Equal(actual: actual, expected: expected);
    }
}