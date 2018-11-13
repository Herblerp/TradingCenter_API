using BitMEX_API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.OrderDTO_s;
using Trainingcenter.Domain.Repositories;

namespace Trainingcenter.Domain.Services.OrderServices
{
    public class OrderServices : IOrderServices
    {
        #region DependencyInjection

        private readonly IOrderRepository _orderRepo;
        private readonly IExchangeKeyRepository _keyRepo;
        private readonly IPortfolioRepository _portfolioRepo;

        public OrderServices(IOrderRepository orderRepo, IExchangeKeyRepository keyRepo, IGenericRepository genericRepo, IPortfolioRepository portfolioRepo)
        {
            _orderRepo = orderRepo;
            _keyRepo = keyRepo;
            _portfolioRepo = portfolioRepo;
        }

        #endregion

        #region ExchangeMethods

        //Get all orders from all exchanges from a given user
        public async Task<List<Order>> GetAllOrders(int userId)
        {
            var orderList = new List<Order>();
            orderList.Concat(await GetBitMEXOrdersFromUserId(userId));
            //orderList.Concat(await GetBinanceOrdersFromUserId(userId));

            var savedOrders = await _orderRepo.SaveOrdersAsync(orderList);

            return savedOrders;
        }

        //Checks all exchanges for new orders from a given user
        public async Task<List<Order>> RefreshAllOrders(int userId)
        {
            //Get the current time
            DateTime time = DateTime.Now;

            //Get the keys for the exchanges
            var BitMEXKey = await _keyRepo.GetFromNameAsync("BitMEX", userId);
            var BinanceKey = await _keyRepo.GetFromNameAsync("Binance", userId);

            //Fill the list with orders from the exchanges
            var orderList = new List<Order>();
            orderList.Concat(await GetBitMEXOrdersFromUserId(userId, BitMEXKey.LastRefresh.Date));
            orderList.Concat(await GetBinanceOrdersFromUserId(userId, BinanceKey.LastId));

            //Save the orders
            var savedOrders = await _orderRepo.SaveOrdersAsync(orderList);

            return savedOrders;
        }

        //Get all orders from the Binance exchange from a given user
        public Task<List<Order>> GetBinanceOrdersFromUserId(int userId)
        {
            throw new NotImplementedException();
        }

        //Get all orders from the Binance exchange from a given user
        public Task<List<Order>> GetBinanceOrdersFromUserId(int userId, int lastOrderId)
        {
            throw new NotImplementedException();
        }

        //Get all orders from the BitMEX exchange from a given user
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

        //Get all orders from the BitMEX exchange after a given date from a given user
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

        public async Task<List<OrderDTO>> GetOrders(int userId, int portfolioId, int amount, DateTime dateFrom, DateTime dateTo)
        {
            if(amount > 200)
            {
                return null;
            }
            if (amount == 0)
            {
                amount = 200;
            }

            var portfolio = await _portfolioRepo.GetFromIdAsync(portfolioId);
            if (portfolioId != 0)
            {
                if (portfolio == null || portfolio.UserId != userId)
                {
                    return null;
                }
            }

            var orderList = new List<Order>();
            if (dateFrom == DateTime.MinValue)
            {
                if(portfolioId == 0)
                {
                    orderList = await _orderRepo.GetOrdersFromUserIdAsync(userId);
                }
                orderList = await _orderRepo.GetOrdersFromPortfolioIdAsync(portfolioId);
            }
            else
            {
                if(dateTo == DateTime.MinValue)
                {
                    dateTo = DateTime.Now;
                }
                if (portfolioId == 0)
                {
                    orderList = await _orderRepo.GetOrdersFromUserIdAsync(userId, dateFrom, dateTo);
                }
                orderList = await _orderRepo.GetOrdersFromPortfolioIdAsync(portfolioId, dateFrom, dateTo);
            }
            orderList = orderList.OrderByDescending(x => x.Timestamp).ToList();
            orderList = orderList.Take(amount).ToList();

            var orderDTOList = new List<OrderDTO>();
            foreach(Order order in orderList)
            {
                orderDTOList.Add(ConvertOrder(order));
            }
            return orderDTOList;
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

        private OrderDTO ConvertOrder(Order order)
        {

            //TODO: Add checks for double values

            var orderDTO = new OrderDTO
            {
                PortfolioId = order.PortfolioId,
                Exchange = order.Exchange,
                ExchangeOrderId = order.ExchangeOrderId,
                Symbol = order.Symbol,
                OrderQty = order.OrderQty,
                Currency = order.Currency,
                Price = order.Price,
                Timestamp = order.Timestamp
            };
            return orderDTO;
        }

        #endregion
    }
}
