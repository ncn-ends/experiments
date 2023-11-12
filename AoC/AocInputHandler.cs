using System.Diagnostics;
using System.Reflection;

namespace Utils;

public static class AocInputHandler
{
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
        StackFrame callerFrame  = st.GetFrame(2) ?? throw new InvalidOperationException(exMsg);

        var callerFilePath = callerFrame.GetFileName();

        var callerInputFilePath = callerFilePath.Remove(callerFilePath.LastIndexOf("/")) + "/input.txt";

        return callerInputFilePath;
    }

    /// <summary>
    /// Fetches input from over http. Will try to infer year/day from directory / file name if not provided.
    /// Requires the session token, which it receives from reading user secrets and env var for "AOC_SESSION_TOKEN".
    /// </summary>
    /// <returns></returns>
    public static string ImportHttp(int? year, int? day)
    {

    }

    private static string InferYearAndDayFromCaller()
    {

    }
}