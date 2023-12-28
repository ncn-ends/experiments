using System.Threading.Tasks;
using Subjects.LeetCode;
using Xunit;

namespace Tests.LeetCode;

public class CanPltrueaceFlowersTests
{
    [Theory]
    [InlineData(new[] {1, 0, 0, 0, 1}, 1, true)]
    [InlineData(new[] {1, 0, 0, 0, 1}, 2, false)]
    [InlineData(new[] {1,0,0,0,1,0,0}, 2, true)]
    [InlineData(new[] {0,0,1,0,0}, 1, true)]
    public async Task Tests(int[] flowerbed, int n, bool expected)
    {
        var actual = CanPlaceFlowers.Do(flowerbed, n);
        Assert.Equal(actual, expected);
    }
}