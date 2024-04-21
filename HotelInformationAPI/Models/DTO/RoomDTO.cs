using System.ComponentModel.DataAnnotations;

namespace HotelInformationAPI.Models.DTO
{
    public class RoomDTO
    {
        [Required]
        public int HotelID { get; set; }
        
        [Required]
        public int RoomID { get; set; }
    }
}
