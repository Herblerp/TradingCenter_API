﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Binance_API
{
    public class OrderStatus
        {
            public string Value { get; }
            private OrderStatus(string value) { Value = value; }

            public static OrderStatus New => new OrderStatus("NEW");
            public static OrderStatus PartiallyFilled => new OrderStatus("PARTIALLY_FILLED");
            public static OrderStatus Filled => new OrderStatus("FILLED");
            public static OrderStatus Canceled => new OrderStatus("CANCELED");
            public static OrderStatus PendingCancel => new OrderStatus("PENDING_CANCEL");
            public static OrderStatus Rejected => new OrderStatus("REJECTED");
            public static OrderStatus Expired => new OrderStatus("EXPIRED");

            public static implicit operator string(OrderStatus orderStatus) => orderStatus.Value;
            public static implicit operator OrderStatus(string text) => new OrderStatus(text);
            public override string ToString() => Value;
        }
}