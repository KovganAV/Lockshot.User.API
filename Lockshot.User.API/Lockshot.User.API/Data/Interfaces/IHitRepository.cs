using Lockshot.User.API.Class;

namespace Lockshot.User.API.Data.Interfaces
{
    public interface IHitRepository
    {
        Task SaveHitAsync(Hit hit);
        Task<IEnumerable<Hit>> GetHitsByUserAsync(int userId);
    }
}
