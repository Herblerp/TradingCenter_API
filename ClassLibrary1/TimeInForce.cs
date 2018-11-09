using System;
using System.Collections.Generic;
using System.Text;

namespace Binance_API
{
    public class TimeInForce
    {
        private string Value { get; }
        private TimeInForce(string value) { Value = value; }

        public static TimeInForce ImmediateOrCancel => new TimeInForce("IOC");
        public static TimeInForce GoodUntilCanceled => new TimeInForce("GTC");

        public static implicit operator string(TimeInForce timeInForce) => timeInForce.Value;
        public static implicit operator TimeInForce(string text) => new TimeInForce(text);
        public override string ToString() => Value;
    }
}
