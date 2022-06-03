class Program
{
    static void Main(string[] args)
    {
        // Benchmarks.Main.RunAllBenchmarks();

        var asd = new UserController();
        var qwe = asd.GetUser("abc123");
        Console.WriteLine(qwe);
    }
}