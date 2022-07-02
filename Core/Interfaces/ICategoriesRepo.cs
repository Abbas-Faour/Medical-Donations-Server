using Core.Entites;

namespace Core.Interfaces
{
    public interface ICategoriesRepo
    {
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetListAsync();
    }
}