namespace Subjects.Experiments.Reflection;

public class FirstExample
{
    public static void Do()
    {
        int i = 42;
        Type type = i.GetType();
        Console.WriteLine(type);
    }
    
}

public struct Person
{
    public string Name { get; init; }
}