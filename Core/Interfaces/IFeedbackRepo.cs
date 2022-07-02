using Core.Entites;

namespace Core.Interfaces
{
    public interface IFeedbackRepo
    {
        Task<Feedback> GetFeedbackByIdAsync(int id);
        Task<IEnumerable<Feedback>> GetListAsync();
        Task AddFeedAsync(Feedback f);
        Task DeleteFeedAsync(int id);
    }
}