using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.Domain.DomainModels
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string ExchangeTransactionId { get; set; }
        public string Currency { get; set; }
        public string TransactionType { get; set;}
        public int Amount { get; set; }
        public DateTime TransactionTime { get; set; }
        public int Walletbalance { get; set; }


    }
}
