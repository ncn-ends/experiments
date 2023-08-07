using System;
using System.Collections.Generic;
using FluentAssertions;
using Subjects.LeetCode;
using Xunit;

namespace Tests.LeetCode;

public class BestTimeToBuyAndSellStockTests
{

    [Theory]
    [InlineData(new[] {7, 1, 5, 3, 6, 4}, 5)]
    [InlineData(new[] {7, 6, 4, 3, 1}, 0)]
    [InlineData(new[] {2, 1, 4}, 3)]
    public void Tests(int[] prices, int expected)
    {
        var vp = new BestTimeToBuyAndSellStock();
        var actual = vp.Do(prices);
        actual.Should().Be(expected);
    }
}