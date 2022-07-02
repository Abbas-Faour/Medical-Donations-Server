using Core.Entites;
using Core.Entites.Identity;

namespace Core.Interfaces
{
    public interface IAdminRepo
    {
        Task<QueryResult<ApplicationUser>> GetUsers(Query queryObj);
        Task<bool> DeactivateUser(string userEmail);
        Task<bool> ReactivateUser(string userEmail);
        Task<bool> DeleteUser(string userEmail);
    }
}