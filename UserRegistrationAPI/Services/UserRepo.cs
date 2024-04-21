using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UserRegistrationAPI.Interfaces;
using UserRegistrationAPI.Models;

namespace UserRegistrationAPI.Services
{
    public class UserRepo : IUserRepo<User,string>
    {
        private readonly UserContext _userContext;

        public UserRepo(UserContext userContext)
        {
            _userContext = userContext;
        }
        public User Add(User item)
        {
            try
            {
                _userContext.UserInformation.Add(item);
                _userContext.SaveChanges();
                return item;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(item);
            }
            return null;
        }

        public User Get(string key)
        {
            var user = _userContext.UserInformation.FirstOrDefault(u => u.UserName == key);
            return user;
        }
    }
}
