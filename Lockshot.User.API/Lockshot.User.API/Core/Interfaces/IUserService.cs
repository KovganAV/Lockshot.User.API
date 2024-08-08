﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Lockshot.User.API.Class;

namespace Lockshot.User.API.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Lockshot.User.API.Class.User>> GetAllUsersAsync();
    }
}
