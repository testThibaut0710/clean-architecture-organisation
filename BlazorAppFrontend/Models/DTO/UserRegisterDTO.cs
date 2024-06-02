namespace BlazorAppFrontend.Models.DTO
{
    public class UserRegisterDTO
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] HashKey { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
        public string PasswordClear { get; set; }
    }
}
