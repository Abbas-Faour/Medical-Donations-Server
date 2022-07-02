using System.ComponentModel.DataAnnotations;

namespace Core.Entites
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required,MaxLength(225)]
        public string Name { get; set; }

        [Required,MaxLength(225)]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
    }
}