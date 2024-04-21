using Microsoft.EntityFrameworkCore;

namespace RoomReservationAPI.Models
{
    public class ReservationContext:DbContext
    {
        public ReservationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Reservation> ReservationInformation { get; set; }
    }
}
