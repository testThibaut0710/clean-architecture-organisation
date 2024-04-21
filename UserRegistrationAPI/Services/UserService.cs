using System.Security.Cryptography;
using System.Text;
using UserRegistrationAPI.Interfaces;
using UserRegistrationAPI.Models;
using UserRegistrationAPI.Models.DTO;

namespace UserRegistrationAPI.Services
{
    public class UserService : IService<UserRegisterDTO, UserDTO>
    {
        private readonly IUserRepo<User, string> _userRepo;
        private readonly ITokenGenerate<UserDTO, string> _tokenGenerate;

        public UserService(IUserRepo<User,string> userRepo,
                           ITokenGenerate<UserDTO,string> tokenGenerate)
        {
            _userRepo = userRepo;
            _tokenGenerate = tokenGenerate;
        }
        public UserDTO Login(UserDTO userDTO)
        {
            UserDTO user = null;
            var userData = _userRepo.Get(userDTO.UserName);
            if (userData != null)
            {
                var hmac = new HMACSHA512(userData.HashKey);
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < userPass.Length; i++)
                {
                    if (userPass[i] != userData.Password[i])
                        return null;
                }
                user = new UserDTO();
                user.UserName = userData.UserName;
                user.Role = userData.Role;
                user.Token = _tokenGenerate.GenerateToken(user);
            }
            return user;
        }

        public UserDTO Register(UserRegisterDTO userDTO)
        {
            UserDTO user = null;
            var hmac = new HMACSHA512();
            userDTO.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.PasswordClear));
            userDTO.HashKey = hmac.Key;
            var resultUser = _userRepo.Add(userDTO);
            if (resultUser != null)
            {
                user = new UserDTO();
                user.UserName = resultUser.UserName;
                user.Role = resultUser.Role;
                user.Token = _tokenGenerate.GenerateToken(user);
            }
            return user;
        }
    }
}
