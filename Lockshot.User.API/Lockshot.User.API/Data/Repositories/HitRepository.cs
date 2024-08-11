using Lockshot.User.API.Class;
using Lockshot.User.API.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lockshot.User.API.Data.Repositories
{
    public class HitRepository : IHitRepository
    {
        private readonly ApplicationDbContext _context;

        public HitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveHitAsync(Hit hit)
        {
            _context.Hits.Add(hit);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Hit>> GetHitsByUserAsync(int userId)
        {
            return await _context.Hits.Where(h => h.UserId == userId).ToListAsync();
        }
    }
}
