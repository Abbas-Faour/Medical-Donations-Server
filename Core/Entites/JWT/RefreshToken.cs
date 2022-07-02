using Core.Entites.Identity;

namespace Core.Entites.JWT
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired  => DateTime.UtcNow >= ExpiresOn;
        public DateTime CreatedOn { get; set; }
        public bool IsActive => !IsExpired;

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}