using System.Diagnostics;
using System.Reflection;

namespace Utils;

public static class AOCInput
{
    public static List<int> Import(string? path = null)
    {
        path ??= GetPairedInputFile();
        
        var textLines = File.ReadAllLines(path);

        var toReturn = new List<int>();
        
        foreach (var line in textLines)
        {
            int number;

            if (int.TryParse(line, out number))
            {
                toReturn.Add(number);
            }
            else
            {
                Console.WriteLine($"Attempted conversion of '{line}' failed.");
            }
        }

        return toReturn;
    }

    public static string GetPairedInputFile()
    {
        
        var st = new StackTrace(true);
        
        var exMsg = "Something went wrong when trying to find the file name in the stack trace.";
        StackFrame callerFrame  = st.GetFrame(2) ?? throw new InvalidOperationException(exMsg);

        var callerFilePath = callerFrame.GetFileName();

        var callerInputFilePath = callerFilePath.Remove(callerFilePath.LastIndexOf("/")) + "/input.txt";

        return callerInputFilePath;
    }
}