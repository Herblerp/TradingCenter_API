using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DTOs.PurchasedPortfolioDTOs
{
    public class PurchasedPortfolioDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int PortfolioId { get; set; }
    }
}
