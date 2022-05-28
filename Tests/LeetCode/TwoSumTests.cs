using System;
using System.Collections.Generic;
using FluentAssertions;
using Subjects.LeetCode;
using Xunit;

namespace Tests.LeetCode;

public class TwoSumTests
{
    public static IEnumerable<object[]> Data()
    {
        yield return new object[] {new List<int> {2, 7, 11, 15}, 9, new Tuple<int, int>(0, 1)};
        yield return new object[] {new List<int> {3, 2, 4}, 6, new Tuple<int, int>(1, 2)};
        yield return new object[] {new List<int> {3, 3}, 6, new Tuple<int, int>(0, 1)};
        
    }

    [MemberData(nameof(Data))]
    [Theory]
    public void SolutionATests(List<int> nums, int target, Tuple<int, int> solution)
    {
        var twoSum = new TwoSum(nums, target);
        var actual = twoSum.SolutionA();
        actual.Should().BeEquivalentTo(solution);
    }
}