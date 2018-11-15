using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.Domain.DomainModels
{
    public class PortfolioOrder
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }
    }
}
