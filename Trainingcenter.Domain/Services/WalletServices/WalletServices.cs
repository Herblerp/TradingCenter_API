using BitMEX_API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.WalletDTOs;
using Trainingcenter.Domain.Repositories;

namespace Trainingcenter.Domain.Services.WalletServices
{
    public class WalletServices : IWalletServices
    {
        private readonly IGenericRepository _genericRepo;
        private readonly IWalletRepository _transactionRepo;
        private readonly IExchangeKeyRepository _keyRepo;

        public WalletServices(IWalletRepository transactionRepo, IExchangeKeyRepository keyRepo, IGenericRepository genericRepo)
        {
            _transactionRepo = transactionRepo;
            _keyRepo = keyRepo;
            _genericRepo = genericRepo;
        }

        public async System.Threading.Tasks.Task<List<WalletDTO>> GetBitmexTransactions(int userId)
        {
            var keyList = await _keyRepo.GetKeysFromNameAsync("BitMEX", userId);
            var walletHistory = new List<Wallet>();

            foreach(ExchangeKey key in keyList) {

                BitMEXApi bitMEXApi = new BitMEXApi(key.Key, key.Secret);

                string temp = bitMEXApi.GetWalletHistory();

                var walletHistoryTemp = JsonConvert.DeserializeObject<List<BitMEXWalletHistory>>(temp);

                foreach (BitMEXWalletHistory transaction in walletHistoryTemp)
                {
                    Wallet tempTransaction = ConvertBitmexTransaction(transaction, key.ExchangeKeyId);

                    if (!await WalletHistoryExists(tempTransaction.ExchangeTransactionId))
                    {
                        await _genericRepo.AddAsync(tempTransaction);
                        walletHistory.Add(tempTransaction);
                    }
                }
            }

            var walletHistoryDTO = new List<WalletDTO>();

            foreach(Wallet transaction in walletHistory)
            {
                walletHistoryDTO.Add(ConvertTransaction(transaction));
            }
            return walletHistoryDTO;

        }

        public async System.Threading.Tasks.Task<List<WalletDTO>> GetTransactionsFromKeyAsync(int exchangeKeyId)
        {
            var walletHistory = await _transactionRepo.GetTransactionsFromKey(exchangeKeyId);

            var walletHistoryDTO = new List<WalletDTO>();

            foreach(Wallet wallet in walletHistory)
            {
                walletHistoryDTO.Add(ConvertTransaction(wallet));
            }
            return walletHistoryDTO;
        }

        public async System.Threading.Tasks.Task<List<WalletDTO>> GetTransactionsFromUserId(int userId)
        {
            var keylist = await _keyRepo.GetKeysFromUserIdAsync(userId);
            var walletHistoryDTO = new List<WalletDTO>();

            foreach (ExchangeKey key in keylist) {

                var walletHistory = await _transactionRepo.GetTransactionsFromKey(key.ExchangeKeyId);

                foreach (Wallet wallet in walletHistory)
                {
                    walletHistoryDTO.Add(ConvertTransaction(wallet));
                }
            }
            return walletHistoryDTO;
        }

        private async Task<bool> WalletHistoryExists(string transId)
        {
            var transaction = await _transactionRepo.TransactionExists(transId);
            if (transaction == null)
            {
                return false;
            }
            return true;
            
        }

        private WalletDTO ConvertTransaction(Wallet x)
        {
            WalletDTO transaction = new WalletDTO
            {
                ExchangeKeyId = x.ExchangeKeyId,
                ExchangeTransactionId = x.ExchangeTransactionId,
                Currency = x.Currency,
                TransactionType = x.TransactionType,
                Amount = x.Amount,
                Timestamp = x.Timestamp.ToString("dd/MM/yyyy hh:mm:ss"),
                Walletbalance = x.Walletbalance
            };

            return transaction;
        }

        private Wallet ConvertBitmexTransaction(BitMEXWalletHistory x, int exchangeKeyId)
        {
            Wallet transaction = new Wallet
            {
                ExchangeKeyId = exchangeKeyId,
                ExchangeTransactionId = x.transactID,
                Currency = x.currency,
                Amount = x.amount,
                Timestamp = DateTime.Parse(x.timestamp),
                Walletbalance = x.walletBalance,
                TransactionType = x.transactType
            };

            if(x.fee == null)
            {
                transaction.Fee = 0;
            }
            else
            {
                transaction.Fee = Int32.Parse(x.fee);
            }

            return transaction;
        }
    }
}
