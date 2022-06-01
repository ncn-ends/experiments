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
        yield return new object[] {new List<int> {4, 4}, 8, new Tuple<int, int>(0, 1)};
    }

    [MemberData(nameof(Data))]
    [Theory]
    public void SolutionTests(List<int> nums, int target, Tuple<int, int> solution)
    {
        var twoSum = new TwoSum(nums, target);
        var actualA = twoSum.SolutionA();
        var actualB = twoSum.SolutionB();
        var actualC = twoSum.SolutionC();
        var actualD = twoSum.SolutionD();
        var actualE = twoSum.SolutionE();
        actualA.Should().BeEquivalentTo(solution);
        actualB.Should().BeEquivalentTo(solution);
        actualC.Should().BeEquivalentTo(solution);
        actualD.Should().BeEquivalentTo(solution);
        actualE.Should().BeEquivalentTo(solution);
    }
}