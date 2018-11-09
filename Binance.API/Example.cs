using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance_API
{
    class Example
    {
        static void Main(string[] args)
        {

            SignedEndPoint();
        }

        static void SignedEndPoint()
        {
            var RestClient = new BinanceClient("", "");
            var accountInfo = RestClient.GetAccountInfo(true).Result;
            foreach (var balanceItem in accountInfo.Balances)
            {
                Console.WriteLine($"{balanceItem.Asset} Total Amount: {balanceItem.Free + balanceItem.Locked} /n");
            }

            foreach (var asset in accountInfo.Balances)
            {
                BalanceItem B = new BalanceItem();
                string symbol = B.Asset;
                if (symbol == "BTC")
                {
                    var allOrders = RestClient.ListAllOrders(symbol + "USDT").Result;
                    foreach (var orderInfo in allOrders)
                    {
                        Console.WriteLine($"Symbol: {AllTradesInfo.Symbol} /n Side: {AllTradesInfo.OrderSide} /n Quantity: {AllTradesInfo.ExecutedQuantity} /n Time: {AllTradesInfo.Time} /n");
                    }
                }
                else
                {
                    var allOrders = RestClient.ListAllOrders(symbol + "BTC").Result;
                    if (allOrders == null)
                    {
                        allOrders = RestClient.ListAllOrders(symbol + "ETH").Result;
                    }
                    else if (allOrders == null)
                    {
                        allOrders = RestClient.ListAllOrders(symbol + "BNB").Result;
                    }
                        foreach (var orderInfo in allOrders)
                        {
                            Console.WriteLine($"Symbol: {AllTradesInfo.Symbol} /n Side: {AllTradesInfo.OrderSide} /n Quantity: {AllTradesInfo.ExecutedQuantity} /n Time: {AllTradesInfo.Time} /n");
                        }
                }
            }
        }
    }

}
