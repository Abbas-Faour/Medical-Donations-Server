using Core.Interfaces;
using Core.Entites.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UserService : IUserRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public UserService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public Address UserAddress(string email)
        {
            var user = _userManager.Users
            .Where(e => e.Email.Contains(email))
            .Include(u => u.Address)
            .SingleOrDefault();
            return user.Address;
        }

        public async Task<UserProfile> UserProfile(string email)
        {
            var requests = await _context.Medicines
            .Where(e => e.ApplicationUser.Email.Contains(email)
             && e.Category.Name.Contains("request"))
            .CountAsync();

            var offers = await _context.Medicines
            .Where(e => e.ApplicationUser.Email.Contains(email)
             && e.Category.Name.Contains("offer"))
            .CountAsync();

            var profile = new UserProfile
            {
                NumberOfRequests = requests,
                NumberOfOffers = offers
            };

            return profile;
        }

    }

}