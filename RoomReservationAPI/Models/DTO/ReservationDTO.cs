using System.ComponentModel.DataAnnotations;

namespace RoomReservationAPI.Models.DTO
{
    public class ReservationDTO
    {
        [Required]
        public int HotelID { get;set; }

        [Required]
        public int RoomID { get; set; }

    }
}
