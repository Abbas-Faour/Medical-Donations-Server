using System.ComponentModel.DataAnnotations;

namespace Core.Entites.JWT
{
    public class LoginModel
    {
        [Required,StringLength(100)]
        public string Email { get; set; }

        [Required,StringLength(100)]
        public string Password { get; set; }
    }
}