using System.ComponentModel.DataAnnotations;

namespace Core.Entites.JWT
{
    public class RegisterModel
    {
         [Required,MaxLength(225)]
        public string FullName { get; set; }

        [Required,MaxLength(100)]
        public string Email { get; set; }

        [Required,MaxLength(225)]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required,MaxLength(12)]
        public string PhoneNumber { get; set; }

        [Required,MaxLength(100)]
        public string Password { get; set; }

    }
}