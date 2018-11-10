using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMEX_API
{
    public class BitMEXOrder
    {
        public string orderID { get; set; }
        public string clOrdID { get; set; }
        public string clOrdLinkID { get; set; }
        public float? account { get; set; }
        public string symbol { get; set; }
        public string side { get; set; }
        public float? simpleOrderQty { get; set; }
        public float? orderQty { get; set; }
        public float? price { get; set; }
        public float? displayQty { get; set; }
        public float? stopPx { get; set; }
        public float? pegOffsetValue { get; set; }
        public string pegPriceType { get; set; }
        public string currency { get; set; }
        public string settlCurrency { get; set; }
        public string ordType { get; set; }
        public string timeInForce { get; set; }
        public string execInst { get; set; }
        public string contingencyType { get; set; }
        public string exDestination { get; set; }
        public string ordStatus { get; set; }
        public string triggered { get; set; }
        public bool? workingIndicator { get; set; }
        public string ordRejReason { get; set; }
        public float? simpleLeavesQty { get; set; }
        public float? leavesQty { get; set; }
        public float? simpleCumQty { get; set; }
        public float? cumQty { get; set; }
        public float? avgPx { get; set; }
        public string multiLegReportingype { get; set; }
        public string text { get; set; }
        public string transactTime { get; set; }
        public string timestamp { get; set; }

        public BitMEXOrder(string orderID, string clOrdID, string clOrdLinkID, float? account, string symbol, string side, float? simpleOrderQty, float? orderQty, float? price, float? displayQty, float? stopPx, float? pegOffsetValue, string pegPriceType, string currency, string settlCurrency, string ordType, string timeInForce, string execInst, string contingencyType, string exDestination, string ordStatus, string triggered, bool? workingIndicator, string ordRejReason, float? simpleLeavesQty, float? leavesQty, float? simpleCumQty, float? cumQty, float? avgPx, string multiLegReportingype, string text, string transactTime, string timestamp)
        {

            this.orderID = orderID;
            this.clOrdID = clOrdID;
            this.clOrdLinkID = clOrdLinkID;
            this.account = account;
            this.symbol = symbol;
            this.side = side;
            this.simpleOrderQty = simpleOrderQty;
            this.orderQty = orderQty;
            this.price = price;
            this.displayQty = displayQty;
            this.stopPx = stopPx;
            this.pegOffsetValue = pegOffsetValue;
            this.pegPriceType = pegPriceType;
            this.currency = currency;
            this.settlCurrency = settlCurrency;
            this.ordType = ordType;
            this.timeInForce = timeInForce;
            this.execInst = execInst;
            this.contingencyType = contingencyType;
            this.exDestination = exDestination;
            this.ordStatus = ordStatus;
            this.triggered = triggered;
            this.workingIndicator = workingIndicator;
            this.ordRejReason = ordRejReason;
            this.simpleLeavesQty = simpleLeavesQty;
            this.leavesQty = leavesQty;
            this.simpleCumQty = simpleCumQty;
            this.cumQty = cumQty;
            this.avgPx = avgPx;
            this.multiLegReportingype = multiLegReportingype;
            this.text = text;
            this.transactTime = transactTime;
            this.timestamp = timestamp;
        }
    }
}
