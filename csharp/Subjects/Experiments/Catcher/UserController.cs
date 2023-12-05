using static ControllerCatcher;

public class UserController
{
    public int GetUser(string id) => Catcher(() =>
    {
        var user = new UserService();
        user.GetUserProfileById(id);
        return 200;
    });

    // public int UpdateUser(string id)
    // {
    //     try
    //     {
    //         var user = new UserService();
    //         user.GetUserProfileById(id);
    //         return 200;
    //     }
    //     catch (Exception e)
    //     {
    //         return 500;
    //     }
    // }
}