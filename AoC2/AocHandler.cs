using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Configuration;
using Utils.Strings;


namespace AoC;

public enum AocSolutionPart
{
    Part1,
    Part2
}

public static class AocHandler
{
    private static IConfigurationBuilder? _builder;
    private static IConfigurationRoot? _config;
    private static HttpClient _http = new();

    /// <summary>
    /// Fetches input from over http. Will try to infer year/day from file path if not provided.
    /// Example call locations:
    /// <code>/src/anythng/something/2023/5/DoesntMatter.cs</code>
    /// <code>/src/anythng/something/2023/Day6Solution.cs</code>
    /// Requires the session token, which it receives from reading user secrets and env var for "AOC_SESSION_TOKEN". You can remove the session cookie key or include it if you want.
    /// </summary>
    public static string ImportHttp(string? year = null,
                                    string? day = null,
                                    [CallerFilePath] string callerFilePath = "")
    {
        var (inferredYear, inferredDay) = InferYearAndDay(callerFilePath);
        year ??= inferredYear;
        day ??= inferredDay;

        if (year is null || day is null) throw new Exception("Couldn't infer year/day and wasn't provided.");

        var finalInput = FetchInput(year, day).GetAwaiter().GetResult();

        return finalInput.Trim();
    }

    public static void SubmitSolution(int solution,
                                      AocSolutionPart part,
                                      string? year = null,
                                      string? day = null,
                                      [CallerFilePath] string callerFilePath = "") =>
            SubmitSolution(solution.ToString(), part, year, day, callerFilePath);
    public static void SubmitSolution(string solution,
                                      AocSolutionPart part,
                                      string? year = null,
                                      string? day = null,
                                      [CallerFilePath] string callerFilePath = "")
    {
        var (inferredYear, inferredDay) = InferYearAndDay(callerFilePath);
        year ??= inferredYear;
        day ??= inferredDay;

        if (year is null || day is null) throw new Exception("Couldn't infer year/day and wasn't provided.");

        PostSolution(solution, part, year, day);
    }


    private static string? _aocToken;

    private static string? AocToken
    {
        get
        {
            if (_aocToken is not null) return _aocToken;

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

            return _aocToken;
        }
    }

    private static (string year, string day) InferYearAndDay(string callerFilePath)
    {
        string? year = null;
        string? day = null;

        /* infer year and day from file path if not provided */
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

        return (year, day);
    }

    private static async Task<string> FetchInput(string year, string day)
    {
        if (AocToken.IsNullOrEmpty())
            throw new Exception("AoC Token wasn't populated yet. This shouldn't happen");

        var httpMsg = new HttpRequestMessage(HttpMethod.Get, $"https://adventofcode.com/{year}/day/{day}/input");
        httpMsg.Headers.Add("Cookie", AocToken);
        var res = await _http.SendAsync(httpMsg);

        if (res.StatusCode != HttpStatusCode.OK)
            throw new Exception(await res.Content.ReadAsStringAsync());

        return await res.Content.ReadAsStringAsync();
    }

    private static async void PostSolution(string solution,
                                           AocSolutionPart part,
                                           string year,
                                           string day)
    {
        var httpMsg = new HttpRequestMessage(HttpMethod.Post, $"https://adventofcode.com/{year}/day/{day}/answer");
        httpMsg.Headers.Add("Cookie", AocToken);;

        httpMsg.Content = new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            { "level", part == AocSolutionPart.Part1
                ? "1"
                : "2"
            },
            { "answer", solution}
        });
        var res= await _http.SendAsync(httpMsg);

        if (res.StatusCode != HttpStatusCode.OK)
            throw new Exception(await res.Content.ReadAsStringAsync());
    }
}