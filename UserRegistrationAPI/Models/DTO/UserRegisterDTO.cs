using System.ComponentModel.DataAnnotations;

namespace UserRegistrationAPI.Models.DTO
{
    public class UserRegisterDTO : User
    {
        [Required]
        [MinLength(8)]
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public string PasswordClear { get; set; }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
    }
}
