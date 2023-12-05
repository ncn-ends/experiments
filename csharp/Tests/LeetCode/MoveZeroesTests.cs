using System.Threading.Tasks;
using Subjects.LeetCode;
using Xunit;

namespace Tests.LeetCode;

public class MoveZeroesTests
{
    [Theory(Skip = "Come back to this")]
    [InlineData(new[] {0, 1, 0, 3, 12}, new []{1, 3, 12, 0, 0})]
    [InlineData(new[] {0}, new []{0})]
    [InlineData(new[] {1}, new []{1})]
    [InlineData(new[] {1, 0}, new []{1, 0})]
    [InlineData(new[] {0, 1}, new []{1, 0})]
    [InlineData(new[] {2, 1}, new []{2, 1})]
    [InlineData(new[] {0, 1, 0}, new []{1, 0, 0})]
    [InlineData(new[] {0, 0, 1}, new []{1, 0, 0})]
    [InlineData(new[] {1, 0, 0}, new []{1, 0, 0})]
    [InlineData(new[] {1, 1, 0}, new []{1, 1, 0})]
    [InlineData(new[] {1, 0, 1}, new []{1, 1, 0})]
    [InlineData(new[] {0, 1, 1}, new []{1, 1, 0})]
    [InlineData(new[] { -58305,-22022,0,0,0,0,0,-76070,42820,48119,0,95708,-91393,60044,0,-34562,0,-88974}, new [] {-58305,-22022,-76070,42820,48119,95708,-91393,60044,-34562,-88974,0,0,0,0,0,0,0,0})]
    [InlineData(new[] { -13009,0,-81471,93346,0,-71602,-18829,-45703,0,0,0,15246,0,51324,89825,0,70362,0,50913,0,47988,-87456,94441,0,0,77733,9338},
        new [] {-13009,-81471,93346,-71602,-18829,-45703,15246,51324,89825,70362,50913,47988,-87456,94441,77733,9338,0,0,0,0,0,0,0,0,0,0,0})]
    public async Task Tests(int[] input, int[] expected)
    {
        MoveZeroes.Do(input);
        Assert.Equal(expected: expected, actual: input);
    }
}