using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.Repositories;
using Trainingcenter.Domain.Services.ExchangeKeyServices;

namespace Tradingcenter.Data.Repositories
{
    public class ExchangeKeyRepository : IExchangeKeyRepository
    {
        #region DependecyInjection

        private readonly DataContext _context;

        public ExchangeKeyRepository(DataContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<List<ExchangeKey>> GetKeysFromNameAsync(string name, int userId)
        {
            var exchangeKeyList = await _context.ExchangeKeys.Where(x => x.UserId == userId && x.Name == name).ToListAsync();
            return exchangeKeyList;
        }

        public async Task<List<ExchangeKey>> GetKeysFromUserIdAsync(int userId)
        {
            var exchangeKeyList = await _context.ExchangeKeys.Where(x => x.UserId == userId).ToListAsync();
            return exchangeKeyList;
        }

        public async Task<ExchangeKey> GetKeyFromKeyStrAsync(string name, string keyStr, int userId)
        {
            var exchangeKey = await _context.ExchangeKeys.FirstOrDefaultAsync(x => x.Name == name && x.Key == keyStr && x.UserId == userId);
            return exchangeKey;
        }

    }
}
