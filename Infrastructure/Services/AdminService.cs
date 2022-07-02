using System.Linq.Expressions;
using Core.Interfaces;
using Core.Entites;
using Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class AdminService : IAdminRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> DeactivateUser(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user is null)
                return false;

            user.IsActive = false;
            await _userManager.UpdateAsync(user);
            return true;
        }
        public async Task<bool> ReactivateUser(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user is null)
                return false;

            user.IsActive = true;
            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteUser(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user is null)
                return false;

            await _userManager.DeleteAsync(user);
            return true;
        }

        public async Task<QueryResult<ApplicationUser>> GetUsers(Query queryObj)
        {

            var result = new QueryResult<ApplicationUser>();

            var query = _userManager.Users.AsQueryable();

            if (!String.IsNullOrEmpty(queryObj.Search))
            {
                query = query.Where(b => b.Email.Contains(queryObj.Search)
                 || b.FullName.Contains(queryObj.Search));
            }

            // Sorting

            var columnsMap = new Dictionary<string, Expression<Func<ApplicationUser, object>>>()
            {
                ["addedDate"] = b => b.AddedDate,
                ["lastSeen"] = b => b.LastSeen
            };

            if (String.IsNullOrWhiteSpace(queryObj.SortBy) || !columnsMap.ContainsKey(queryObj.SortBy))
                queryObj.SortBy = "addedDate";

            if (queryObj.IsSortAscending)
                query = query.OrderBy(columnsMap[queryObj.SortBy]);
            else
                query = query.OrderByDescending(columnsMap[queryObj.SortBy]);

            // Pagination              

            if (queryObj.Page <= 0)
                queryObj.Page = 1;

            if (queryObj.PageSize <= 0)
                queryObj.PageSize = 5;

            query = query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);

            result.TotalItems = await query.CountAsync();

            result.Items = await query.ToListAsync();

            return result;

        }
    }
}