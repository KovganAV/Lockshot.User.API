using Lockshot.User.API.Class;
using Lockshot.User.API.Core.Interfaces;
using Lockshot.User.API.Data.Repositories;

namespace Lockshot.User.API.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Lockshot.User.API.Class.User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }
    }
}
