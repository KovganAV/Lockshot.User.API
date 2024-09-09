using Lockshot.User.API.Class;
using Lockshot.User.API.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lockshot.User.API.Core.Interfaces
{
    public interface IHitService
    {
        Task SaveHitAsync(HitDto hitDto);
        Task<IEnumerable<HitDto>> GetMostHits(int userId, double Distance);
        Task<IEnumerable<HitDto>> GetMostHitsByMetrics(int userId, double Metrics);
        Task<IEnumerable<HitDto>> GetMostHitsByScore(string userId, int Score);
    }
}
