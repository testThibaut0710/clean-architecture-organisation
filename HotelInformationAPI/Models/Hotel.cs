using System.ComponentModel.DataAnnotations;

namespace HotelInformationAPI.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est requis")]
        [MaxLength(25, ErrorMessage = "Ne doit pas dépasser 25 caractères")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "La description est requise")]
        [MinLength(25, ErrorMessage = "Doit contenir au moins 25 caractères")]
        public string Description { get; set; }

        [Required(ErrorMessage = "L'adresse est requise")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Le numéro de contact est requis")]
        [MinLength(10, ErrorMessage = "Le numéro de contact doit contenir 10 caractères")]
        public string ContactNumber { get; set; }
        
        [Required(ErrorMessage = "La ville est requise")]
        public string City { get; set; }

        [Required(ErrorMessage = "Le pays est requis")]
        public string Country { get; set; }

        [Required(ErrorMessage = "La note moyenne est requise")]
        public double AverageRating { get; set; }

        [Required(ErrorMessage = "Le nombre de chambres est requis")]
        public int NumberOfRooms { get; set; }

        [Required(ErrorMessage = "Le prix est requis")]
        public double Price { get; set; }
    }
}
