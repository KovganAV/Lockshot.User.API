using Lockshot.User.API.Class;

namespace Lockshot.User.API.Core.Interfaces
{
    public interface IHitService
    {
        Task SaveHitAsync(HitDto hitDto);
        Task<IEnumerable<HitDto>> GetHitsByUserAsync(int userId);
    }
}
