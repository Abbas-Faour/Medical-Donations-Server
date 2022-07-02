using System.ComponentModel.DataAnnotations;

namespace API.DTOs.KeyValuePairs
{
    public class EmailDto
    {

        [Required,MaxLength(225)]
        public string Email { get; set; }
    }
}