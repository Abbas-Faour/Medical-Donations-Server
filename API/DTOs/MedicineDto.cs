namespace API.DTOs
{
    public class MedicineDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        public string ImageUrl { get; set; }
        
        public DateTime PostedAt { get; set; } = DateTime.UtcNow;
        public KeyValuePairDto Category { get; set; }
        public UserDto User { get; set; }
    }
}