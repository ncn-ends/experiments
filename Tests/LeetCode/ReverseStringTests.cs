using System.Collections.Generic;
using FluentAssertions;
using Subjects.LeetCode;
using Xunit;

namespace Tests.LeetCode;

public class ReverseStringTests
{
    public static IEnumerable<object[]> Data()
    {
        yield return new object[]
        {
            new List<char> {'h', 'e', 'l', 'l', 'o'},
            new List<char> {'o', 'l', 'l', 'e', 'h'}
        };
        yield return new object[]
        {
            new List<char> {'H', 'a', 'n', 'n', 'a', 'h'},
            new List<char> {'h', 'a', 'n', 'n', 'a', 'H'}
        };
    }

    [MemberData(nameof(Data))]
    [Theory]
    public void SolutionATest(List<char> input, List<char> expected)
    {
        var reverseString = new ReverseString(input);
        var actual = reverseString.SolutionA();
        actual.Should().BeEquivalentTo(expected);
    }


    [MemberData(nameof(Data))]
    [Theory]
    public void SolutionBTest(List<char> input, List<char> expected)
    {
        var reverseString = new ReverseString(input);
        var actual = reverseString.SolutionB();
        actual.Should().BeEquivalentTo(expected);
    }
    
    
    [MemberData(nameof(Data))]
    [Theory]
    public void SolutionCTest(List<char> input, List<char> expected)
    {
        var reverseString = new ReverseString(input);
        var actual = reverseString.SolutionC();
        actual.Should().BeEquivalentTo(expected);
    }
}