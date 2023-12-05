using OneOf;
using OneOf.Types;

namespace Subjects.OneOf;

public record Status
{
    public string StatusCode = "400";
}

public class ReturnObject
{
    public OneOf<Status, Error, None> GetResult(bool isOk)
    {
        if (isOk) return new Status();
        return new None();

    }
}