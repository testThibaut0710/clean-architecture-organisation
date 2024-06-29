using Microsoft.EntityFrameworkCore;

namespace UserRegistrationAPI.Models
{
    public class UserContext : DbContext
    {
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public UserContext(DbContextOptions options) : base(options)
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        {

        }
        public DbSet<User> UserInformation { get; set; }
    }
}
