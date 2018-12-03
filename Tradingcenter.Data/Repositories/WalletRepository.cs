using BitMEX_API;
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
    public class WalletRepository : IWalletRepository
    {
        private readonly DataContext _context;

        public WalletRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Wallet>> GetTransactionsFromKey(int keyId)
        {
            return await _context.Transactions.Where(x => x.ExchangeKeyId == keyId).ToListAsync();
        }

        public async Task<Wallet> TransactionExists(string transId)
        {
            return await _context.Transactions.FirstOrDefaultAsync(x => x.ExchangeTransactionId == transId);
        }
    }
}
