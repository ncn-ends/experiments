namespace NUnitTests;

public static class AssertExtensions
{
    public static void HasLength<T>(this Assert assert, ICollection<T> collection, int length)
    {
        Assert.That(length, Is.EqualTo(collection.Count));
    }
}