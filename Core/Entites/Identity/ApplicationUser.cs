using System.ComponentModel.DataAnnotations;
using Core.Entites.JWT;
using Microsoft.AspNetCore.Identity;

namespace Core.Entites.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required,MaxLength(225)]
        public string FullName { get; set; }
        public Address Address { get; set; }
         public List<Medicine> Medicines { get; set; } = new List<Medicine>();

          public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

         public DateTime AddedDate { get; set; } = DateTime.UtcNow;
         public DateTime LastSeen { get; set; } = DateTime.UtcNow;
         public bool? IsActive { get; set; } = true;
    }
}