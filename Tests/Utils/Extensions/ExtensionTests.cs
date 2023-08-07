using System.Collections.Generic;
using Utils.Extensions;
using Xunit;

namespace Tests.Utils.Extensions;

public class ExtensionTests
{
    [Fact]
    public void Something()
    {
        List<int> list = new() {5, 6, 7};
        list.RemoveAt(^1);
        Assert.Equal(list, new []{5, 6});
    }
}