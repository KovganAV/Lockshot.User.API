using Lockshot.User.API.Class;
using Lockshot.User.API.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lockshot.User.API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lockshot.User.API.Class.User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
