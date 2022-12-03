using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Utils;
using Xunit;

namespace Tests.Utils;

public class FetchTests
{
    [Fact]
    public async Task GetTests()
    {
        var asd = await Fetch.Get("https://adventofcode.com/2020/day/2/input", new RequestDetails
        {
            cookies = new
                {
                    session = "53616c7465645f5fd757a843e9590c5939b8490f4dee28a97a431b6539cc141d214b59e5dc38c9bf68f9b7d029b8c8bd4a676d2449843e6612a448cbc6e15663"
                }
        });
        Debugger.Break();
    }
}