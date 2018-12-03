using BitMEX_API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Repositories
{
    public interface IWalletRepository
    {
        Task<List<Wallet>> GetTransactionsFromKey(int keyId);
        Task<Wallet> TransactionExists(string transId);
    }
}
