using System.ComponentModel.DataAnnotations;

namespace UserRegistrationAPI.Models.DTO
{
    public class UserDTO
    {
        [Required]
        [MaxLength(25, ErrorMessage = "Must Not Exceed More Than 25 Characters")]
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public string UserName { get; set; }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.

        [Required]
        [MinLength(8)]
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public string Password { get; set; }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public string? Token { get; set; }
        public string? Role { get; set; }
    }
}
