using HotelInformationAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomReservationAPI.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public string UserName { get; set; }
        
        [Required]
        public int HotelId { get; set; }

        [Required]
        public int RoomId { get;set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime CheckInDate { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime CheckOutDate { get; set; }
    }
}
