using BitMEX_API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly IGenericRepository _genericRepo;
        private readonly IUserRepository _userRepo;

        public OrderServices(IOrderRepository orderRepo, IExchangeKeyRepository keyRepo, IGenericRepository genericRepo, IPortfolioRepository portfolioRepo)
        {
            _orderRepo = orderRepo;
            _keyRepo = keyRepo;
            _portfolioRepo = portfolioRepo;
            _genericRepo = genericRepo;
    }

        #endregion

        #region ExchangeMethods

        //Get all orders from all exchanges from a given user
        public async Task<List<Order>> GetAllOrders(int userId)
        {
            var portfolio = await _portfolioRepo.GetFromNameAsync("default", userId);
            int portfolioId = portfolio.PortfolioId;

            var orderList = new List<Order>();
            orderList.Concat(await GetBitMEXOrdersFromUserId(userId, portfolioId));
            //orderList.Concat(await GetBinanceOrdersFromUserId(userId));

            var savedOrders = await _orderRepo.SaveOrdersAsync(orderList);

            return savedOrders;
        }

        //Checks all exchanges for new orders from a given user
        public async Task<List<Order>> RefreshAllOrders(int userId)
        {
            //Get the default portfolio
            var portfolio = await _portfolioRepo.GetFromNameAsync("default", userId);
            int portfolioId = portfolio.PortfolioId;

            //Get the current time
            DateTime time = DateTime.Now;
            time = time.AddDays(-1);

            //Get the keys for the exchanges
            var BitMEXKey = await _keyRepo.GetKeysFromNameAsync("BitMEX", userId);
            var BinanceKey = await _keyRepo.GetKeysFromNameAsync("Binance", userId);

            //Fill the list with orders from the exchanges
            var orderList = new List<Order>();

            foreach (ExchangeKey key in BitMEXKey)
            {
                orderList.AddRange(await GetBitMEXOrdersFromUserId(userId, portfolioId, key.LastRefresh.Date));
                key.LastRefresh = time;
                await _genericRepo.UpdateAsync(key);
            }
            foreach (ExchangeKey key in BinanceKey)
            {
                orderList.AddRange(await GetBinanceOrdersFromUserId(userId, portfolioId, key.LastId));
            }

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
        public Task<List<Order>> GetBinanceOrdersFromUserId(int userId, int portfolioId, int lastOrderId)
        {
            throw new NotImplementedException();
        }

        //Get all orders from the BitMEX exchange from a given user
        public async Task<List<Order>> GetBitMEXOrdersFromUserId(int userId, int portfolioId)
        {
            var exchangeKey = await _keyRepo.GetKeysFromNameAsync("BitMEX", userId);
            var orderList = new List<Order>();

            foreach (ExchangeKey key in exchangeKey)
            {
                BitMEXApi bitmex = new BitMEXApi(key.Key, key.Secret);

                string temp = bitmex.GetOrders();
                var BitMEXOrderList = JsonConvert.DeserializeObject<List<BitMEXOrder>>(temp);

                foreach (var BitMEXOrder in BitMEXOrderList)
                {
                    Order order = ConvertBitMEXOrder(BitMEXOrder, userId, portfolioId);
                    orderList.Add(order);
                }
            }
            return orderList;
        }

        //Get all orders from the BitMEX exchange after a given date from a given user
        public async Task<List<Order>> GetBitMEXOrdersFromUserId(int userId, int portfolioId, DateTime dateFrom)
        {
            var exchangeKey = await _keyRepo.GetKeysFromNameAsync("BitMEX", userId);
            var orderList = new List<Order>();

            foreach (ExchangeKey key in exchangeKey)
            {
                BitMEXApi bitmex = new BitMEXApi(key.Key, key.Secret);

                string date = dateFrom.ToString("yyyy-MM-dd hh:mm:ss.fff");

                string temp = bitmex.GetOrders(date);

                try
                {
                    var BitMEXOrderList = JsonConvert.DeserializeObject<List<BitMEXOrder>>(temp);
                    foreach (var BitMEXOrder in BitMEXOrderList)
                    {
                        Order order = ConvertBitMEXOrder(BitMEXOrder, userId, portfolioId);
                        orderList.Add(order);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return orderList;
        }

        #endregion

        #region Services

        public async Task<List<OrderDTO>> GetOrders(int userId, int portfolioId, int amount, string dateFrom, string dateTo)
        {
            DateTime _dateFrom;
            DateTime _dateTo;

            if (amount > 200)
            {
                return null;
            }
            if (amount == 0)
            {
                amount = 200;
            }

            if (portfolioId != 0)
            {
                var portfolio = await _portfolioRepo.GetFromIdAsync(portfolioId);
                if (portfolio == null || portfolio.UserId != userId)
                {
                    return null;
                }
            }

            var orderList = new List<Order>();
            if (dateFrom == null)
            {
                if(portfolioId == 0)
                {
                    orderList = await _orderRepo.GetOrdersFromUserIdAsync(userId);
                }
                else
                {
                    orderList = await _orderRepo.GetOrdersFromPortfolioIdAsync(portfolioId);
                }
            }
            else
            {
                try
                {
                    _dateFrom = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    return null;
                }

                if (dateTo == null)
                {
                    _dateTo = DateTime.Now;
                }
                else
                {
                    try
                    {
                        _dateTo = DateTime.ParseExact(dateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        return null;
                    }
                }

                if (portfolioId == 0)
                {
                    orderList = await _orderRepo.GetOrdersFromUserIdAsync(userId, _dateFrom, _dateTo);
                }
                else
                {
                    orderList = await _orderRepo.GetOrdersFromPortfolioIdAsync(portfolioId, _dateFrom, _dateTo);
                }
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

        public async Task<List<ProfitPerDayDTO>> GetProfitPerDayFromPortfolio(int userId, int portfolioId)
        {

            if (userId != 0)
            {
                var user = await _userRepo.GetFromIdAsync(userId);
                if (user == null || user.UserId != userId)
                {
                    return null;
                }
            }
            if (portfolioId != 0)
            {
                var portfolio = await _portfolioRepo.GetFromIdAsync(portfolioId);
                if (portfolio == null || portfolio.UserId != userId)
                {
                    return null;
                }
            }
            var orderList = new List<Order>();
            orderList = await _orderRepo.GetOrdersFromPortfolioIdAsync(portfolioId);
            orderList = orderList.OrderByDescending(x => x.Timestamp).ToList();

            var ProfitPerDayDTOList = new List<ProfitPerDayDTO>();
            
            foreach (Order order in orderList)
            {
                double profit = 0;
                if (order.Side.Equals("Buy"))
                {
                    profit = order.Price;
                }
                else
                {
                    profit = order.Price * -1;
                }

                bool dayAlreadyInList = false;

                foreach (ProfitPerDayDTO profitPerDay in ProfitPerDayDTOList)
                { 
                    if (order.Timestamp.Date.Equals(profitPerDay.Day.Date))
                    {
                        profitPerDay.Profit = profitPerDay.Profit + profit;
                        dayAlreadyInList = true;
                    }
                }


                if (!dayAlreadyInList)
                {

                    ProfitPerDayDTOList.Add(new ProfitPerDayDTO { Profit = profit, Day = order.Timestamp.Date });
                }
            }


            return ProfitPerDayDTOList;
        }



        public async Task<List<ProfitPerDayDTO>> GetProfitPerDayFromUser(int userId)
        {
            if (userId != 0)
            {
                var usertest = await _userRepo.GetFromIdAsync(userId);
                if (usertest == null || usertest.UserId != userId)
                {
                    return null;
                }
            }
           
            User user = await _userRepo.GetFromIdAsync(userId);

            var ProfitPerDayDTOList = new List<ProfitPerDayDTO>();

            foreach (Portfolio portfolio in user.Portfolios)
            {
                foreach (Order order in portfolio.Orders)
                {
                    double profit = 0;
                    if (order.Side.Equals("Buy"))
                    {
                        profit = order.Price;
                    }
                    else
                    {
                        profit = order.Price * -1;
                    }

                    bool dayAlreadyInList = false;

                    foreach (ProfitPerDayDTO profitPerDay in ProfitPerDayDTOList)
                    {
                        if (order.Timestamp.Date.Equals(profitPerDay.Day.Date))
                        {
                            profitPerDay.Profit = profitPerDay.Profit + profit;
                            dayAlreadyInList = true;
                        }
                    }


                    if (!dayAlreadyInList)
                    {

                        ProfitPerDayDTOList.Add(new ProfitPerDayDTO { Profit = profit, Day = order.Timestamp.Date });
                    }
                }
            }

            return ProfitPerDayDTOList;
        }



        #endregion

        #region Converters

        private Order ConvertBitMEXOrder(BitMEXOrder bitMEXOrder, int userId, int portfolioId)
        {

            //TODO: Add checks for double values

            var order = new Order
            {
                UserId = userId,
                PortfolioId = portfolioId,
                Exchange = "BitMEX",
                ExchangeOrderId = bitMEXOrder.orderID,
                Symbol = bitMEXOrder.symbol.Substring(0, 3),
                OrderQty = (double)bitMEXOrder.orderQty,
                Currency = bitMEXOrder.currency,
                Price = (double)bitMEXOrder.price,
                Timestamp = DateTime.Parse(bitMEXOrder.timestamp),
                Side = bitMEXOrder.side
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
                Side = order.Side,
                OrderQty = order.OrderQty,
                Currency = order.Currency,
                Price = order.Price,
                Timestamp = order.Timestamp
            };
            return orderDTO;
        }
        /*
        private ProfitPerDayDTO ConvertProfitPerDay(double profit, DateTime day)
        {
            var profitPerDayDTO = new ProfitPerDayDTO
            {
                Profit = profit,
                Day = day
            };
            return profitPerDayDTO;
        }
        */
        #endregion
    }
}
