using Microsoft.EntityFrameworkCore;

namespace UserRegistrationAPI.Models
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> UserInformation { get; set; }
    }
}
