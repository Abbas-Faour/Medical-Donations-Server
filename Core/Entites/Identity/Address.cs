using System.ComponentModel.DataAnnotations;

namespace Core.Entites.Identity
{
    public class Address
    {
         public int Id { get; set; }

         [Required,MaxLength(225)]
        public string FullName{ get; set; }

        [Required]
        public string Street { get; set; }

        [Required,MaxLength(225)]
        public string City { get; set; }
        
        [Required,MaxLength(8)]
        public string PhoneNumber { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}