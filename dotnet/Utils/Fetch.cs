using System.Diagnostics;
using System.Net;

namespace Utils;

public struct RequestDetails
{
    public dynamic? cookies { get; set; }

}

public static class Fetch
{
    private static readonly HttpClient _client = new();

    public static async Task<string> Get(string url, dynamic details)
    {
        var reqDetails = (RequestDetails) details;
        Debugger.Break();
        return await Get(url, reqDetails);
    }
    
    
    public static async Task<string> Get(string url, RequestDetails details)
    {
        var dict = details.cookies as Dictionary<string, string>;
        Debugger.Break();
        var res = await _client.GetStringAsync(url);
        return res;
    }
}