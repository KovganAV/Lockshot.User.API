using Lockshot.User.API.Class;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lockshot.User.API.Data.Interfaces
{
    public interface IHitRepository
    {
        Task SaveHitAsync(Hit hit);
        Task<IEnumerable<Hit>> GetHitsByUserAsync(int userId, bool sortDescending = false); 
    }
}
