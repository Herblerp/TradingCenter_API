using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMEX_API
{
    class BitMEXPosition
    {
        public float? account { get; set; }
        public string symbol { get; set; }
        public string currency { get; set; }
        public string underlying { get; set; }
        public string quoteCurrency { get; set; }
        public float? commission { get; set; }
        public float? initMarginReq { get; set; }
        public float? maintMarginReq { get; set; }
        public float? riskLimit { get; set; }
        public float? leverage { get; set; }
        public bool? crossMargin { get; set; }
        public float? deleveragePercentile { get; set; }
        public float? rebalancedPnl { get; set; }
        public float? prevRealisedPnl { get; set; }
        public float? prevUnrealisedPnl { get; set; }
        public float? prevClosePrice { get; set; }
        public string openingTimestamp { get; set; }
        public float? openingQty { get; set; }
        public float? openingCost { get; set; }
        public float? openingComm { get; set; }
        public float? openOrderBuyQty { get; set; }
        public float? openOrderBuyCost { get; set; }
        public float? openOrderBuyPremium { get; set; }
        public float? openOrderSellQty { get; set; }
        public float? openOrderSellCost { get; set; }
        public float? openOrderSellPremium { get; set; }
        public float? execBuyQty { get; set; }
        public float? execBuyCost { get; set; }
        public float? execSellQty { get; set; }
        public float? execSellCost { get; set; }
        public float? execQty { get; set; }
        public float? execCost { get; set; }
        public float? execComm { get; set; }
        public string currentTimestamp { get; set; }
        public float? currentQty { get; set; }
        public float? currentCost { get; set; }
        public float? currentComm { get; set; }
        public float? realisedCost { get; set; }
        public float? unrealisedCost { get; set; }
        public float? grossOpenCost { get; set; }
        public float? grossOpenPremium { get; set; }
        public float? grossExecCost { get; set; }
        public bool? isOpen { get; set; }
        public float? markPrice { get; set; }
        public float? markValue { get; set; }
        public float? riskValue { get; set; }
        public float? homeNotional { get; set; }
        public float? foreignNotional { get; set; }
        public string posState { get; set; }
        public float? posCost { get; set; }
        public float? posCost2 { get; set; }
        public float? posCross { get; set; }
        public float? posInit { get; set; }
        public float? posComm { get; set; }
        public float? posLoss { get; set; }
        public float? posMargin { get; set; }
        public float? posMaint { get; set; }
        public float? posAllowance { get; set; }
        public float? taxableMargin { get; set; }
        public float? initMargin { get; set; }
        public float? maintMargin { get; set; }
        public float? sessionMargin { get; set; }
        public float? targetExcessMargin { get; set; }
        public float? varMargin { get; set; }
        public float? realisedGrossPnl { get; set; }
        public float? realisedTax { get; set; }
        public float? realisedPnl { get; set; }
        public float? unrealisedGrossPnl { get; set; }
        public float? longBankrupt { get; set; }
        public float? shortBankrupt { get; set; }
        public float? taxBase { get; set; }
        public float? indicativeTaxRate { get; set; }
        public float? indicativeTax { get; set; }
        public float? unrealisedTax { get; set; }
        public float? unrealisedPnl { get; set; }
        public float? unrealisedPnlPcnt { get; set; }
        public float? unrealisedRoePcnt { get; set; }
        public float? simpleQty { get; set; }
        public float? simpleCost { get; set; }
        public float? simpleValue { get; set; }
        public float? simplePnl { get; set; }
        public float? simplePnlPcnt { get; set; }
        public float? avgCostPrice { get; set; }
        public float? avgEntryPrice { get; set; }
        public float? breakEvenPrice { get; set; }
        public float? marginCallPrice { get; set; }
        public float? liquidationPrice { get; set; }
        public float? bankruptPrice { get; set; }
        public string timestamp { get; set; }
        public float? lastPrice { get; set; }
        public float? lastValue { get; set; }

        public BitMEXPosition(float? account, string symbol, string currency, string underlying, string quoteCurrency, float? commission, float? initMarginReq, float? maintMarginReq, float? riskLimit, float? leverage, bool? crossMargin, float? deleveragePercentile, float? rebalancedPnl, float? prevRealisedPnl, float? prevUnrealisedPnl, float? prevClosePrice, string openingTimestamp, float? openingQty, float? openingCost, float? openingComm, float? openOrderBuyQty, float? openOrderBuyCost, float? openOrderBuyPremium, float? openOrderSellQty, float? openOrderSellCost, float? openOrderSellPremium, float? execBuyQty, float? execBuyCost, float? execSellQty, float? execSellCost, float? execQty, float? execCost, float? execComm, string currentTimestamp, float? currentQty, float? currentCost, float? currentComm, float? realisedCost, float? unrealisedCost, float? grossOpenCost, float? grossOpenPremium, float? grossExecCost, bool? isOpen, float? markPrice, float? markValue, float? riskValue, float? homeNotional, float? foreignNotional, string posState, float? posCost, float? posCost2, float? posCross, float? posInit, float? posComm, float? posLoss, float? posMargin, float? posMaint, float? posAllowance, float? taxableMargin, float? initMargin, float? maintMargin, float? sessionMargin, float? targetExcessMargin, float? varMargin, float? realisedGrossPnl, float? realisedTax, float? realisedPnl, float? unrealisedGrossPnl, float? longBankrupt, float? shortBankrupt, float? taxBase, float? indicativeTaxRate, float? indicativeTax, float? unrealisedTax, float? unrealisedPnl, float? unrealisedPnlPcnt, float? unrealisedRoePcnt, float? simpleQty, float? simpleCost, float? simpleValue, float? simplePnl, float? simplePnlPcnt, float? avgCostPrice, float? avgEntryPrice, float? breakEvenPrice, float? marginCallPrice, float? liquidationPrice, float? bankruptPrice, string timestamp, float? lastPrice, float? lastValue)
        {
            this.account = account;
            this.symbol = symbol;
            this.currency = currency;
            this.underlying = underlying;
            this.quoteCurrency = quoteCurrency;
            this.commission = commission;
            this.initMarginReq = initMarginReq;
            this.maintMarginReq = maintMarginReq;
            this.riskLimit = riskLimit;
            this.leverage = leverage;
            this.crossMargin = crossMargin;
            this.deleveragePercentile = deleveragePercentile;
            this.rebalancedPnl = rebalancedPnl;
            this.prevRealisedPnl = prevRealisedPnl;
            this.prevUnrealisedPnl = prevUnrealisedPnl;
            this.prevClosePrice = prevClosePrice;
            this.openingTimestamp = openingTimestamp;
            this.openingQty = openingQty;
            this.openingCost = openingCost;
            this.openingComm = openingComm;
            this.openOrderBuyQty = openOrderBuyQty;
            this.openOrderBuyCost = openOrderBuyCost;
            this.openOrderBuyPremium = openOrderBuyPremium;
            this.openOrderSellQty = openOrderSellQty;
            this.openOrderSellCost = openOrderSellCost;
            this.openOrderSellPremium = openOrderSellPremium;
            this.execBuyQty = execBuyQty;
            this.execBuyCost = execBuyCost;
            this.execSellQty = execSellQty;
            this.execSellCost = execSellCost;
            this.execQty = execQty;
            this.execCost = execCost;
            this.execComm = execComm;
            this.currentTimestamp = currentTimestamp;
            this.currentQty = currentQty;
            this.currentCost = currentCost;
            this.currentComm = currentComm;
            this.realisedCost = realisedCost;
            this.unrealisedCost = unrealisedCost;
            this.grossOpenCost = grossOpenCost;
            this.grossOpenPremium = grossOpenPremium;
            this.grossExecCost = grossExecCost;
            this.isOpen = isOpen;
            this.markPrice = markPrice;
            this.markValue = markValue;
            this.riskValue = riskValue;
            this.homeNotional = homeNotional;
            this.foreignNotional = foreignNotional;
            this.posState = posState;
            this.posCost = posCost;
            this.posCost2 = posCost2;
            this.posCross = posCross;
            this.posInit = posInit;
            this.posComm = posComm;
            this.posLoss = posLoss;
            this.posMargin = posMargin;
            this.posMaint = posMaint;
            this.posAllowance = posAllowance;
            this.taxableMargin = taxableMargin;
            this.initMargin = initMargin;
            this.maintMargin = maintMargin;
            this.sessionMargin = sessionMargin;
            this.targetExcessMargin = targetExcessMargin;
            this.varMargin = varMargin;
            this.realisedGrossPnl = realisedGrossPnl;
            this.realisedTax = realisedTax;
            this.realisedPnl = realisedPnl;
            this.unrealisedGrossPnl = unrealisedGrossPnl;
            this.longBankrupt = longBankrupt;
            this.shortBankrupt = shortBankrupt;
            this.taxBase = taxBase;
            this.indicativeTaxRate = indicativeTaxRate;
            this.indicativeTax = indicativeTax;
            this.unrealisedTax = unrealisedTax;
            this.unrealisedPnl = unrealisedPnl;
            this.unrealisedPnlPcnt = unrealisedPnlPcnt;
            this.unrealisedRoePcnt = unrealisedRoePcnt;
            this.simpleQty = simpleQty;
            this.simpleCost = simpleCost;
            this.simpleValue = simpleValue;
            this.simplePnl = simplePnl;
            this.simplePnlPcnt = simplePnlPcnt;
            this.avgCostPrice = avgCostPrice;
            this.avgEntryPrice = avgEntryPrice;
            this.breakEvenPrice = breakEvenPrice;
            this.marginCallPrice = marginCallPrice;
            this.liquidationPrice = liquidationPrice;
            this.bankruptPrice = bankruptPrice;
            this.timestamp = timestamp;
            this.lastPrice = lastPrice;
            this.lastValue = lastValue;
        }
    }

}
