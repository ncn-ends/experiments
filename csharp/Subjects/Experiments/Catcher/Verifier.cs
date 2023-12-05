namespace Subjects.Experiments.Catcher;

public static class Verifier
{
    public static void Verify(string id)
    {
        if (id.Contains("asd")) throw new ArgumentException("Cannot contain 'asd'");
    }

    public static void Verify(int count)
    {
        if (count < 20)
        {
            throw new ArgumentException("Cannot be a negative number.");
        }
    }
}