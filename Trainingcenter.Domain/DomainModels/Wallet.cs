using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.Domain.DomainModels
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public int ExchangeKeyId { get; set; }
        public string ExchangeTransactionId { get; set; }
        public string Currency { get; set; }
        public string TransactionType { get; set;}
        public int Amount { get; set; }
        public int Fee { get; set; }
        public DateTime Timestamp { get; set; }
        public int Walletbalance { get; set; }

        public ExchangeKey ExchangeKey { get; set; }
    }
}
