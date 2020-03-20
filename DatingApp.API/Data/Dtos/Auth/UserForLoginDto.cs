using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Data.Dtos.Auth
{
    public class UserForLoginDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}