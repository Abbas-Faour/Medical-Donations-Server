using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class FeedToAddDto
    {

        [Required,MaxLength(225)]
        public string Name { get; set; }

        [Required,MaxLength(225)]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}