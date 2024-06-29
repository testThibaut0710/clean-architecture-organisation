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

        public UserService(IUserRepo<User, string> userRepo,
                           ITokenGenerate<UserDTO, string> tokenGenerate)
        {
            _userRepo = userRepo;
            _tokenGenerate = tokenGenerate;
        }
        public UserDTO Login(UserDTO userDTO)
        {
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
            UserDTO user = null;
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
            var userData = _userRepo.Get(userDTO.UserName);
            if (userData != null)
            {
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
                var hmac = new HMACSHA512(userData.HashKey);
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < userPass.Length; i++)
                {
#pragma warning disable CS8602 // Déréférencement d'une éventuelle référence null.
                    if (userPass[i] != userData.Password[i])
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
                        return null;
#pragma warning restore CS8602 // Déréférencement d'une éventuelle référence null.
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
                }
#pragma warning disable CS8601 // Existence possible d'une assignation de référence null.
                user = new UserDTO
                {
                    UserName = userData.UserName,
                    Role = userData.Role
                };
#pragma warning restore CS8601 // Existence possible d'une assignation de référence null.
                user.Token = _tokenGenerate.GenerateToken(user);
            }
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
            return user;
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
        }

        public UserDTO Register(UserRegisterDTO userDTO)
        {
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
            UserDTO user = null;
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
            var hmac = new HMACSHA512();
            userDTO.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.PasswordClear));
            userDTO.HashKey = hmac.Key;
            var resultUser = _userRepo.Add(userDTO);
            if (resultUser != null)
            {
#pragma warning disable CS8601 // Existence possible d'une assignation de référence null.
                user = new UserDTO
                {
                    UserName = resultUser.UserName,
                    Role = resultUser.Role
                };
#pragma warning restore CS8601 // Existence possible d'une assignation de référence null.
                user.Token = _tokenGenerate.GenerateToken(user);
            }
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
            return user;
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
        }
    }
}
