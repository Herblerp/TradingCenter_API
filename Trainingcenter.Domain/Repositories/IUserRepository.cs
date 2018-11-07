using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Repositories
{
    interface IUserRepository
    {
        Task<User> GetFromIdAsync(int userId);
        Task<User> GetFromUsernameAsync(string username);
    }
}
