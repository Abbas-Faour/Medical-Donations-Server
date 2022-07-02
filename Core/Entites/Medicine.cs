using System.ComponentModel.DataAnnotations;
using Core.Entites.Identity;
using Core.EntitesCore.Entites;

namespace Core.Entites
{
    public class Medicine : BaseEntity
    {
        [Required,MaxLength(225)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.UtcNow;

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}