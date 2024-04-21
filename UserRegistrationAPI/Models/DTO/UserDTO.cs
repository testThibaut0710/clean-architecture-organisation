using System.ComponentModel.DataAnnotations;

namespace UserRegistrationAPI.Models.DTO
{
    public class UserDTO
    {
        [Required]
        [MaxLength(25,ErrorMessage = "Must Not Exceed More Than 25 Characters")]
        public string UserName { get; set; }
        
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
    }
}
