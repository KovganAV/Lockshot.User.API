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


        public async Task<Lockshot.User.API.Class.User> CreateUserAsync(Lockshot.User.API.Class.User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Lockshot.User.API.Class.User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
