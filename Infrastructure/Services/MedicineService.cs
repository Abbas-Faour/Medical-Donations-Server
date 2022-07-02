using System.Linq.Expressions;
using Core.Interfaces;
using Core.Entites;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class MedicineService : IMedicineRepo
    {
        public ApplicationDbContext _context { get; }
        public MedicineService(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<QueryResult<Medicine>> getAllAsync(Query queryObj)
        {

            var result = new QueryResult<Medicine>();


            var query = _context.Medicines
             .Include(c => c.Category)
             .Include(u => u.ApplicationUser)
                 .ThenInclude(a => a.Address)
             .AsQueryable();

            // Filtering

            if (!String.IsNullOrWhiteSpace(queryObj.Category))
            {
                query = query.Where(b => b.Category.Name.Contains(queryObj.Category))
                .AsQueryable();
            }

            if (!String.IsNullOrWhiteSpace(queryObj.Search))
            {
                query = query.Where(
                b => b.Name.Contains(queryObj.Search)
                || b.Description.Contains(queryObj.Search)
                || b.ApplicationUser.Address.City.Contains(queryObj.Search))
            .AsQueryable();
            }

            if (!String.IsNullOrWhiteSpace(queryObj.UserEmail))
            {
                query = query.Where(b => b.ApplicationUser.Email.Contains(queryObj.UserEmail))
                .AsQueryable();
            }

            // Sorting

            var columnsMap = new Dictionary<string, Expression<Func<Medicine, object>>>()
            {
                ["name"] = b => b.Name,
                ["quantity"] = b => b.Quantity,
                ["AddedDate"] = b => b.AddedDate
            };

            if (String.IsNullOrWhiteSpace(queryObj.SortBy) || !columnsMap.ContainsKey(queryObj.SortBy))
                queryObj.SortBy = "AddedDate";

            if (queryObj.IsSortAscending)
                query = query.OrderBy(columnsMap[queryObj.SortBy]);
            else
                query = query.OrderByDescending(columnsMap[queryObj.SortBy]);

            // Pagination              

            if (queryObj.Page <= 0)
                queryObj.Page = 1;

            if (queryObj.PageSize <= 0)
                queryObj.PageSize = 8;

            result.TotalItems = await query.CountAsync();

            query = query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);


            result.Items = await query.ToListAsync();

            return result;
        }


        public async Task<Medicine> getByIdAsync(int id)
        {
            return await _context.Medicines
           .Include(c => c.Category)
           .Include(u => u.ApplicationUser)
               .ThenInclude(a => a.Address)
           .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task addAsync(Medicine m)
        {
            await _context.Medicines.AddAsync(m);
        }

        public async Task deleteAsync(int id)
        {
            var m = await getByIdAsync(id);
            _context.Medicines.Remove(m);
        }

    }
}