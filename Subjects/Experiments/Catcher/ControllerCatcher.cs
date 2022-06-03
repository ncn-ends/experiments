public static class ControllerCatcher
{
    public static int Catcher(Func<int> func)
    {
        try
        {
            return func();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return 500;
        }
    }
}