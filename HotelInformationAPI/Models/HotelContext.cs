using Microsoft.EntityFrameworkCore;

namespace HotelInformationAPI.Models
{
    public class HotelContext : DbContext
    {
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public HotelContext(DbContextOptions options) : base(options)
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        {

        }

        public DbSet<Hotel> InformationsHotel { get; set; }
        public DbSet<Room> InformationsChambre { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().ToTable("DetailsHotel");
            modelBuilder.Entity<Room>().ToTable("DetailsChambre");

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 101,
                    Name = "Grand Hotel Luxueux",
                    Description = "Un hôtel luxueux offrant un séjour mémorable.",
                    Address = "123 Rue test",
                    ContactNumber = "0467000000",
                    City = "Montpellier",
                    Country = "FRANCE",
                    AverageRating = 1,
                    NumberOfRooms = 10,
                    Price = 50,
                }
        );
        }
    }
}
