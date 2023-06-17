using Roy.Enterprise.API.Authorization;
using Roy.Enterprise.API.Data;
using Roy.Enterprise.API.Entities;
using Roy.Enterprise.API.Helpers;
using Roy.Enterprise.API.Models;

namespace Roy.Enterprise.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private readonly IJwtUtils _jwtUtils;

        public UserService(IJwtUtils jwtUtils)
        {
            _jwtUtils = jwtUtils;
        }

        public User Authenticate(AuthenticateRequest model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                return null;

            var user = _userRepository.GetUserByUserName(model.Username);

            // return null if user not found
            if (user == null) return null;

            // check if password is correct
            if (user.PasswordHash != null && user.PasswordSalt != null &&
                VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return user;

            return null;
        }

        public IEnumerable<User> GetAll()
        {
            return null;
        }

        public User GetById(int id)
        {
            return null;//_users.FirstOrDefault(x => x.Id == id);
        }

        public Guid RegisterUser(UserModel model)
        {
            var userDetails = _userRepository.GetUserByUserMobileNo(model.Username);
            if (userDetails != null)
                throw new AppException("Mobile is already exist");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Photo = model.Photo,
                EmailId = model.EmailId,
                MobileNo = model.MobileNo,
                Address = model.Address,
                Status = true,
                CreatedDate = DateTime.Now,
                CreatedBy = "ADMIN",
            };

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            var userid = _userRepository.CreateUser(user);
            return userid;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");
            using var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            if (computedHash.Where((t, i) => t != storedHash[i]).Any())
            {
                return false;
            }
            return true;
        }
    }
}
