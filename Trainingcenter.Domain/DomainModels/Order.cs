using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.Domain.DomainModels
{
    public class Order
    {
        public string OrderId { get; set; }
        public int UserId { get; set; }
        public int PortfolioId { get; set; }

        public string Exchange { get; set; }
        public string ExchangeOrderId { get; set; }
        public string Symbol { get; set; }
        public string Side { get; set; }
        public double OrderQty { get; set; }
        public string Currency { get; set; }
        public double Price { get; set; }
        public DateTime Timestamp { get; set; }
        
        public User User { get; set; }
        public Portfolio Portfolio { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
