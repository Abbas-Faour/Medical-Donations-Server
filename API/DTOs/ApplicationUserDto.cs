namespace API.DTOs
{
        public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastSeen { get; set; }
    }
}