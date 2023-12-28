using Utils.Numbers;


namespace NUnitTests;

public class Test_NumberHelpers
{
    [Test]
    public static void TestLCM()
    {
        List<int> case1 = [555, 444, 333];
        Assert.That(NumberHelpers.GetLCM(case1), Is.EqualTo(6660));

        List<int> case2 = [2, 3];
        Assert.That(NumberHelpers.GetLCM(case2), Is.EqualTo(6));

        List<long> case3 = [12169, 20093, 20659, 22357, 13301, 18961];
        Assert.That(NumberHelpers.GetLCM(case3), Is.EqualTo(15690466351717));
    }
}