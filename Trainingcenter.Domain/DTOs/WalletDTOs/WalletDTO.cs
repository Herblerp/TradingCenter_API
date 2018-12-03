using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.Domain.DTOs.WalletDTOs
{
    public class WalletDTO
    {
        public int TransactionId { get; set; }
        public int ExchangeKeyId { get; set; }
        public string ExchangeTransactionId { get; set; }
        public string Currency { get; set; }
        public string TransactionType { get; set; }
        public int Amount { get; set; }
        public string Timestamp { get; set; }
        public int Walletbalance { get; set; }
    }
}
