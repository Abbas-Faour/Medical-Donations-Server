using Core.Interfaces;
using Core.Entites;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class FeedbacksService : IFeedbackRepo
    {
        private readonly ApplicationDbContext _context;
        public FeedbacksService(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task AddFeedAsync(Feedback f)
        {
            await _context.Feedbacks.AddAsync(f);
        }

        public async Task DeleteFeedAsync(int id)
        {
            var feed = await GetFeedbackByIdAsync(id);
            _context.Feedbacks.Remove(feed);
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int id)
        {
            return await _context.Feedbacks.FindAsync(id);
        }

        public async Task<IEnumerable<Feedback>> GetListAsync()
        {
            return await _context.Feedbacks.ToListAsync();
        }
    }
}