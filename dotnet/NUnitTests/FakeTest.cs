namespace NUnitTests;

public class FakeTest
{
    [Test]
    public static void Test()
    {
        Assert.AreEqual(ProjectEuler.Problem1.f(10), 23);
        Assert.AreEqual(ProjectEuler.Problem1.f(1000), 233168);
    }
}