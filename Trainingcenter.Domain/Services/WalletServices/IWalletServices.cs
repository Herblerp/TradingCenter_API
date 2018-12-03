using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DTOs.WalletDTOs;

namespace Trainingcenter.Domain.Services.WalletServices
{
    public interface IWalletServices
    {
        Task<List<WalletDTO>> GetTransactionsFromUserId(int userId);
        Task<List<WalletDTO>> GetTransactionsFromKeyAsync(int exchangeKeyId);

        Task<List<WalletDTO>> GetBitmexTransactions(int userId);
    }
}
