using Roy.Enterprise.API.Entities;
using Roy.Enterprise.API.Models;

namespace Roy.Enterprise.API.Services
{

    public interface IUserService
    {
        User Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        Guid RegisterUser(UserModel model);
    }
}
