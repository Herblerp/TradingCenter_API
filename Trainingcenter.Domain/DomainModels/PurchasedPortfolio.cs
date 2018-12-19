using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.Domain.DomainModels
{
    public class PurchasedPortfolio
    {
        public int PurchasedPortfolioId { get; set; }

        public int PortfolioId { get; set; }
        public int UserId { get; set; }

        public DateTime PurchasedOn { get; set; }

        public Portfolio Portfolio {get; set;}
        public User User { get; set; }
    }
}
