using Core.Entites.Identity;

namespace Core.Interfaces
{
    public interface IUserRepo
    {
        Task<UserProfile> UserProfile(string email);
        Address UserAddress(string email);
    }
}