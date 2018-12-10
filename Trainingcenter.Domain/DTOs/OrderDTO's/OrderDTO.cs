using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.Domain.DTOs.OrderDTO_s
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string Exchange { get; set; }
        public string ExchangeOrderId { get; set; }
        public string Symbol { get; set; }
        public string Side { get; set; }
        public double OrderQty { get; set; }
        public string Currency { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImgURL { get; set; }
        public bool IsSold { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
