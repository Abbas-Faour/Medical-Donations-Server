using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Services
{
    public class UnitofWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}