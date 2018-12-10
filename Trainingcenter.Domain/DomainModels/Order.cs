using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DomainModels
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ExchangeKeyId { get; set; }
        public string Description { get; set; }
        public string ImgURL { get; set; }
        public bool IsSold { get; set; }

        [Required]
        public string Exchange { get; set; }
        [Required]
        public string ExchangeOrderId { get; set; }
        [Required]
        public string Symbol { get; set; }
        [Required]
        public string Side { get; set; }
        [Required]
        public double OrderQty { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public double Price { get; set; }
        public DateTime Timestamp { get; set; }

        public User User { get; set; }
        public ExchangeKey Exchangekey { get; set; }

        public ICollection<PortfolioOrder> OrderPortfolios { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
