using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Utils.Extensions;
using Utils.Strings;


namespace AoC;

public static class AocInputHandler
{
    private static IConfigurationBuilder? _builder;
    private static IConfigurationRoot? _config;
    private static HttpClient _http = new HttpClient();
    private static string? _aocToken = null;

    /// <summary>
    /// Imports the adjacent in the directory to the caller as long as its named "input.txt".
    /// Can also specify the file path.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string ImportFile(string? path = null)
    {
        path ??= GetPairedInputFile();

        var text = File.ReadAllText(path);

        return text;
    }

    private static string GetPairedInputFile()
    {
        var st = new StackTrace(true);

        var exMsg = "Something went wrong when trying to find the file name in the stack trace.";
        StackFrame callerFrame = st.GetFrame(2) ?? throw new InvalidOperationException(exMsg);

        var callerFilePath = callerFrame.GetFileName();

        var callerInputFilePath = callerFilePath.Remove(callerFilePath.LastIndexOf("/")) + "/input.txt";

        return callerInputFilePath;
    }

    /// <summary>
    /// Fetches input from over http. Will try to infer year/day from file path if not provided.
    /// If inferring from file path, it's expected that the caller's file is in a directory with the day, and that is in a directory with the year as the name.
    /// Example: <code>/src/anythng/something/2023/5/DoesntMatter.cs</code>
    /// Requires the session token, which it receives from reading user secrets and env var for "AOC_SESSION_TOKEN". You can remove the session cookie key or include it if you want.
    /// </summary>
    /// <returns></returns>
    public static string ImportHttp(string? year = null,
                                    string? day = null,
                                    [CallerFilePath] string callerFilePath = "")
    {
        if (_aocToken is null)
        {
            const string tokenKey = "AOC_SESSION_TOKEN";

            /* try to get session token from user secrets / env var */
            _builder ??= new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddEnvironmentVariables()
                         .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: false);
            _config = _builder.Build();

            var sessionToken = _config[tokenKey];
            if (sessionToken.IsNullOrEmpty())
                throw new Exception("Failed to get session token");

            if (sessionToken!.StartsWith("session=")) _aocToken = sessionToken;
            else _aocToken = "session=" + sessionToken;
        }


        /* infer year and day from file path if not provided */
        if (year is null || day is null)
        {
            var split = callerFilePath.Split("/");
            /* Supports these paths:
             *   AoC/<year>/<day>/Day<day>Solutions.cs
             *   AoC/<year>/Day<day>Solutions.cs
             */
            if (split[^2].Length == 4 && split[^2].IsInt())
            {
                year ??= split[^2];
                day ??= split[^1].Split("Day")[1].Split("S")[0];
            }
            else
            {
                year ??= split[^3];
                day ??= split[^2];
            }
        }

        var finalInput = ResolveInput(year, day).GetAwaiter().GetResult();

        return finalInput.Trim();
    }


    private static async Task<string> ResolveInput(string year, string day)
    {
        if (_aocToken.IsNullOrEmpty())
            throw new Exception("AoC Token wasn't populated yet. This shouldn't happen");

        var httpMsg = new HttpRequestMessage(HttpMethod.Get, $"https://adventofcode.com/{year}/day/{day}/input");
        httpMsg.Headers.Add("Cookie", _aocToken);
        var res = await _http.SendAsync(httpMsg);

        if (res.StatusCode != HttpStatusCode.OK)
            throw new Exception(await res.Content.ReadAsStringAsync());

        return await res.Content.ReadAsStringAsync();
    }
}