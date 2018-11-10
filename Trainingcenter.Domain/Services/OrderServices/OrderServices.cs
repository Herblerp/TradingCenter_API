using BitMEX_API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.Repositories;

namespace Trainingcenter.Domain.Services.OrderServices
{
    class OrderServices : IOrderServices
    {
        #region DependencyInjection
        private readonly IOrderRepository _orderRepo;
        private readonly IExchangeKeyRepository _keyRepo;

        public OrderServices(IOrderRepository orderRepo, IExchangeKeyRepository keyRepo)
        {
            _orderRepo = orderRepo;
            _keyRepo = keyRepo;
        }
        #endregion

        #region ExchangeMethods
        public Task<List<Order>> GetAllOrders(int userId)
        {
            throw new NotImplementedException();
        }
        public Task<List<Order>> RefreshAllOrders(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetBinanceOrdersFromUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetBitMEXOrdersFromUserId(int userId)
        {
            var exchangeKey = await _keyRepo.GetFromNameAsync("BitMEX", userId);

            BitMEXApi bitmex = new BitMEXApi(exchangeKey.Key, exchangeKey.Secret);

            string temp = bitmex.GetOrders();
            var BitMEXOrderList = JsonConvert.DeserializeObject<List<BitMEXOrder>>(temp);
            var orderList = new List<Order>();

            foreach(var BitMEXOrder in BitMEXOrderList)
            {
                Order order = ConvertBitMEXOrder(BitMEXOrder, userId);
                orderList.Add(order);
            }
            return orderList;
        }

        public async Task<List<Order>> GetBitMEXOrdersFromUserId(int userId, DateTime dateFrom)
        {
            var exchangeKey = await _keyRepo.GetFromNameAsync("BitMEX", userId);

            BitMEXApi bitmex = new BitMEXApi(exchangeKey.Key, exchangeKey.Secret);

            string date = dateFrom.ToString("yyyy-MM-dd hh:mm:ss.fff");

            string temp = bitmex.GetOrders(date);
            var BitMEXOrderList = JsonConvert.DeserializeObject<List<BitMEXOrder>>(temp);
            var orderList = new List<Order>();

            foreach (var BitMEXOrder in BitMEXOrderList)
            {
                Order order = ConvertBitMEXOrder(BitMEXOrder, userId);
                orderList.Add(order);
            }
            return orderList;
        }
        #endregion

        #region Services
        public async Task<List<Order>> GetOrdersFromUserId(int userId, DateTime dateFrom, DateTime dateTo)
        {
            var orderList = await _orderRepo.GetAllFromDateUserIdAsync(userId, dateFrom, dateTo);
            orderList = orderList.OrderByDescending(x => x.Timestamp).ToList();

            return orderList;
        }

        public async Task<List<Order>> GetOrdersFromPortfolioId(int portfolioId, DateTime dateFrom, DateTime dateTo)
        {
            var orderList = await _orderRepo.GetAllFromDateUserIdAsync(portfolioId, dateFrom, dateTo);
            orderList = orderList.OrderByDescending(x => x.Timestamp).ToList();

            return orderList;
        }
        #endregion

        #region Converters

        private Order ConvertBitMEXOrder(BitMEXOrder bitMEXOrder, int userId)
        {

            //TODO: Add checks for double values

            var order = new Order
            {
                UserId = userId,
                Exchange = "BitMEX",
                ExchangeOrderId = bitMEXOrder.orderID,
                Symbol = bitMEXOrder.symbol.Substring(0, 3),
                OrderQty = (double)bitMEXOrder.orderQty,
                Currency = bitMEXOrder.currency,
                Price = (double)bitMEXOrder.price,
                Timestamp = DateTime.Parse(bitMEXOrder.timestamp)
            };
            return order;
        }

        #endregion
    }
}
