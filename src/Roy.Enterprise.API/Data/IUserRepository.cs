using Roy.Enterprise.API.Entities;

namespace Roy.Enterprise.API.Data
{
    public interface IUserRepository
    {
        Guid CreateUser(User user);
        Guid DeleteUser(User user);
        Guid UpdateUser(User user);
        User GetUserByUserName(string username);
        User GetUserByUserMobileNo(string mobileNo);
    }
}
