namespace Subjects.Experiments.Span;

public class StringExample
{
    public (int day, int month, int year) ParseDateAsSubstring(string date)
    {
        string monthAsString = date.Substring(0, 2);
        string dayAsString = date.Substring(3, 2);
        string yearAsString = date.Substring(6);
        int month = int.Parse(monthAsString);
        int day = int.Parse(dayAsString);
        int year = int.Parse(yearAsString);

        return (day, month, year);
    }

    public (int day, int month, int year) ParseDateAsSpan(string date)
    {
        ReadOnlySpan<char> spanDate = date;

        var monthAsSpan = spanDate.Slice(0, 2);
        var dayAsSpan = spanDate.Slice(3, 2);
        var yearAsSpan = spanDate.Slice(6);
        int month = int.Parse(monthAsSpan);
        int day = int.Parse(dayAsSpan);
        int year = int.Parse(yearAsSpan);
        

        return (day, month, year);
    }
    
}