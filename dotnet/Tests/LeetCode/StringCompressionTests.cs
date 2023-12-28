using Subjects.LeetCode;
using Xunit;

namespace Tests.LeetCode;

public class StringCompressionTests
{
    /* TODO: come back to this */
    [Theory(Skip = "WIP")]
    [InlineData(new[] {'a', 'a', 'b', 'b', 'c', 'c', 'c'},
                new[] {'a', '2', 'b', '2', 'c', '3', 'c'},
                6)]
    [InlineData(new[] {'a'},
                new[] {'a'},
                1)]
    [InlineData(new[] {'a', 'b'},
                new[] {'a', 'b'},
                2)]
    [InlineData(new[] {'a', 'a'},
                new[] {'a', '2'},
                2)]
    [InlineData(new[] {'a', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b'},
                new[] {'a', 'b', '1', '2', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b'},
                2)]
    [InlineData(new[] {'a', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'c', 'c', 'c', 'd'},
                new[] {'a', 'b', '1', '2', 'c', '3', 'd', 'b', 'b', 'b', 'b', 'b', 'b', 'c', 'c', 'c', 'd'},
                4)]
    public static void Tests(char[] chars, char[] expectedTransformation, int expectedReturn)
    {
        var actual = StringCompression.Compress(chars);
        Assert.Equal(chars, expectedTransformation);
        // Assert.Equal(expectedReturn, actual);
    }
}