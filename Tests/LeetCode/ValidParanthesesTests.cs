using System;
using System.Collections.Generic;
using FluentAssertions;
using Subjects.LeetCode;
using Xunit;

namespace Tests.LeetCode;

public class ValidParanthesesTests
{

    [Theory]
    [InlineData(")", false)]
    [InlineData("}", false)]
    [InlineData("]", false)]
    [InlineData("(", false)]
    [InlineData("[", false)]
    [InlineData("{", false)]
    [InlineData("()", true)]
    [InlineData("()[]{}", true)]
    [InlineData("(]", false)]
    [InlineData("({()()}[])", true)]
    public void Tests(string inputStr, bool expected)
    {
        var vp = new ValidParantheses();
        var actual = vp.DoIterative(inputStr);
        actual.Should().Be(expected);
    }
}