
using static Subjects.Experiments.Catcher.Verifier;

public class UserService
{
    public string GetUserProfileById(string id)
    {
        Verify(20);
        return id;
    }
}