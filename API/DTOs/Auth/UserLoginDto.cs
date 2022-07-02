using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Auth
{
    public class UserLoginDto
    {
         [Required,StringLength(100)]
        public string Email { get; set; }

        [Required,StringLength(100)]
        public string Password { get; set; }
    }
}