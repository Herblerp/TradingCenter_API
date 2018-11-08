using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Binance_API
{
    public class AllTradesInfo
    {
        [JsonProperty("symbol")]
        public static string Symbol { get; set; }

       //[JsonProperty("orderId")]
        //public long OrderId { get; set; }

        //[JsonProperty("clientOrderId")]
        //public string ClientOrderId { get; set; }

        //[JsonProperty("price")]
        //public decimal Price { get; set; }

        [JsonProperty("origQty")]
        public static decimal OriginalQuantity { get; set; }

        [JsonProperty("executedQty")]
        public static decimal ExecutedQuantity { get; set; }

        //[JsonProperty("status")]
        //public OrderStatus Status { get; set; }

        //[JsonProperty("timeInForce")]
        //public TimeInForce TimeInForce { get; set; }

        //[JsonProperty("type")]
        //public OrderType OrderType { get; set; }

        [JsonProperty("side")]
        public static OrderSide OrderSide { get; set; }

        //[JsonProperty("stopPrice")]
        //public decimal StopPrice { get; set; }

        //[JsonProperty("icebergQty")]
        //public decimal IcebergQuantity { get; set; }

        [JsonProperty("time")]
        public static long Time { get; set; }
    }
}
