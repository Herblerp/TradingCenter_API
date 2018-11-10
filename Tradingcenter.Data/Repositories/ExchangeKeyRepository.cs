using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.Repositories;

namespace Tradingcenter.Data.Repositories
{
    class ExchangeKeyRepository : IExchangeKeyRepository
    {
        private readonly DataContext _context;

        public ExchangeKeyRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ExchangeKey> GetFromNameAsync(string name, int userId)
        {
            var exchangeKey = await _context.ExchangeKeys.FirstOrDefaultAsync(x => x.Name == name && x.UserId == userId);
            return exchangeKey;
        }

        public async Task<List<ExchangeKey>> GetAllFromUserIdAsync(int userId)
        {
            var exchangeKeyList = await _context.ExchangeKeys.Where(x => x.UserId == userId).ToListAsync();
            return exchangeKeyList;
        }
    }
}
