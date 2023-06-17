using Roy.Enterprise.API.Entities;
using Roy.Enterprise.API.Helpers;

namespace Roy.Enterprise.API.Data
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public Guid CreateUser(User user)
        {
            this.Create<User>(user);
            return user.Id;
        }

        public Guid DeleteUser(User user)
        {
            this.Remove<User>(user);
            return user.Id;
        }

        public Guid UpdateUser(User user)
        {
            this.Update<User>(user);
            return user.Id;
        }
        public User GetUserByUserName(string username)
        {
            User userDetails = null;
            DatabaseAccess(context =>
            {
                userDetails = context.Users.FirstOrDefault(x => x.Username.Equals(username));
            });
            return userDetails;
        }

        public User GetUserByUserMobileNo(string mobileNo)
        {
            User userDetails = null;
            DatabaseAccess(context =>
            {
                userDetails = context.Users.FirstOrDefault(x => x.MobileNo.Equals(mobileNo));
            });
            if (userDetails == null) return null;
            var user = new User
            {
                Id = userDetails.Id,
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                Username = userDetails.Username,
                EmailId = userDetails.EmailId,
            };
            return user;
        }
    }
}
