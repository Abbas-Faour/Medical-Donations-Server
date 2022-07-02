using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class MedicineToAddDto
    {
        [Required,MaxLength(225)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        
        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string UserEmail { get; set; }

    }
}