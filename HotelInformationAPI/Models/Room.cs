using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelInformationAPI.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Hotel")]
        [Column("HotelId")]
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        [Required]

        public string RoomType { get; set; }


        [Required]
        public double Price { get; set; }

        [Required]
        public bool Available { get; set; }
    }
}
