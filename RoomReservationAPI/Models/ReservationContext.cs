using Microsoft.EntityFrameworkCore;

namespace RoomReservationAPI.Models
{
    public class ReservationContext : DbContext
    {
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public ReservationContext(DbContextOptions options) : base(options)
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        {

        }

        public DbSet<Reservation> ReservationInformation { get; set; }
    }
}
