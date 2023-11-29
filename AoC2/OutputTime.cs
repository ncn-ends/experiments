using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Serialization;


namespace AoC;

[PSerializable]
public class OutputTimeAttribute : MethodInterceptionAspect
{
    public override void OnInvoke(MethodInterceptionArgs args)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        args.Proceed();

        Console.WriteLine("-----");
        Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms");
        stopwatch.Stop();
    }
}