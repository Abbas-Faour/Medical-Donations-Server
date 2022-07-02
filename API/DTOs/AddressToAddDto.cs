using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class AddressToAddDto
    {
        [Required,MaxLength(225)]
        public string FullName { get; set; }

        [Required,MaxLength(225)]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required,MaxLength(225)]
        public string Email { get; set; }
    }
}