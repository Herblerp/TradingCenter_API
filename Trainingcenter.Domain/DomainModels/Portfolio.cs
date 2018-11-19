using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DomainModels
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Goal { get; set; }
        public bool IsDefault { get; set; }

        public ICollection<Note> Notes { get; set; }
        public ICollection<PortfolioOrder> PortfolioOrders { get; set; }

        public User User { get; set; }
    }
}
