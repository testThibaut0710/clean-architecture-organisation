using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserRegistrationAPI.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "Must Not Exceed More Than 25 Characters")]
        public string UserName { get; set; }
        public byte[]? Password { get; set; }
        public byte[]? HashKey { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(10,ErrorMessage = "Phone Number Must Contain 10 Characters")]
        public string PhoneNumber { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
       
        [Required]
        public string Role { get; set; }
    }
}
