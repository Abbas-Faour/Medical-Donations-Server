using Core.Entites;

namespace Core.Interfaces
{
    public interface IMedicineRepo
    {
        Task<QueryResult<Medicine>> getAllAsync(Query queryObj);
        Task<Medicine> getByIdAsync(int id);
        Task addAsync(Medicine m);
        Task deleteAsync(int id);
    }
}