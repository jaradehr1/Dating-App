using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Data.Dtos.Auth
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "You must specify password between 4 and 8 characters!")]
        public string Password { get; set; }
    }
}