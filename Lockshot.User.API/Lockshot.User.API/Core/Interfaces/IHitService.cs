using Lockshot.User.API.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lockshot.User.API.Core.Interfaces
{
    public interface IHitService
    {
        Task SaveHitAsync(HitDto hitDto);
        Task<IEnumerable<HitDto>> GetHitsByUserAsync(int userId);
    }
}
