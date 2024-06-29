using System.ComponentModel.DataAnnotations;

namespace UserRegistrationAPI.Models.DTO
{
    public class UserRegisterDTO : User
    {
        [Required]
        [MinLength(8)]

        public string PasswordClear { get; set; }

    }
}
