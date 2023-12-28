using System.Diagnostics;
using System.Threading.Tasks;
using Subjects.Experiments;
using Xunit;

namespace Tests.Experiments;

public class BinaryTests
{
    [Theory]
    [InlineData(0, "0")]
    [InlineData(1, "1")]
    [InlineData(2, "10")]
    [InlineData(3, "11")]
    [InlineData(4, "100")]
    [InlineData(5, "101")]
    [InlineData(6, "110")]
    [InlineData(7, "111")]
    [InlineData(8, "1000")]
    [InlineData(10, "1010")]
    [InlineData(16, "10000")]
    [InlineData(20, "10100")]
    public async Task ToBinaryString_ConvertIntToString(int n, string expected)
    {
        var actual = n.ToBinaryString();
        Assert.Equal(actual: actual, expected: expected);
    }

    [Theory]
    [InlineData(2, 2, 4)]
    public async Task BitwiseAdd_AddWithoutAdditionSign(int n, int m, int expected)
    {
        var asd = 0b0100; // 4
        var qwe = 0b1011; // 11
        var a = asd & qwe; // 0000 = only puts a 1 in the column when there's a common value
        var b = asd | qwe; // 1111 = prioritizes 1 over 0
        var c = asd ^ qwe; // 1111 = uses 1 if there's only 1 1
        Assert.Equal(0b0000, a);
        Assert.Equal(0b1111, b);
        Assert.Equal(0b1111, c);

        var zxc = 0b0110; // 6
        var d = zxc & qwe;
        var e = zxc | qwe;
        var f = zxc ^ qwe;

        Debugger.Break();
    }
}