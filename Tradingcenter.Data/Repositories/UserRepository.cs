using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.Repositories;

namespace Tradingcenter.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region DependecyInjection

        private readonly DataContext _context;

        public UserRepository (DataContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<User> GetFromIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<User> GetFromUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> ValidateUser(string key)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.VerificationKey == key);
            return user;
        }
    }
}
