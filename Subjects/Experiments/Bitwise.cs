namespace Subjects.Experiments;

public class Bitwise
{

}

public static class BinaryExtensions
{
    public static string ToBinaryString(this int n)
    {
        return Convert.ToString(n, 2);
    }

    public static string ToBinaryString(this int n, int padding)
    {
        return n.ToBinaryString().PadLeft(padding, '0');
    }
}