using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.Domain.DTOs.PortfolioDTO_s
{
    public class PortfolioDTO
    {
        public int PortfoliId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Goal { get; set; }
    }
}
