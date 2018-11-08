using System;
using System.Collections.Generic;
using System.Text;

namespace Binance_API
{
    public class OrderType
    {
        private string Value { get; }
        private OrderType(string value) { Value = value; }

        public static OrderType Limit => new OrderType("LIMIT");
        public static OrderType Market => new OrderType("MARKET");

        public static implicit operator string(OrderType orderType) => orderType.Value;
        public static implicit operator OrderType(string text) => new OrderType(text);
        public override string ToString() => Value;
    }
}
