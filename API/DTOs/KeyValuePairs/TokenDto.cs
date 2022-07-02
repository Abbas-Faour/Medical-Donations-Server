using System.ComponentModel.DataAnnotations;

namespace API.DTOs.KeyValuePairs
{
    public class TokenDto
    {

        [Required]
        public string Token { get; set; }
    }
}