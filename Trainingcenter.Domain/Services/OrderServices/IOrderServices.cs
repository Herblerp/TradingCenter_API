using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Services.OrderServices
{
    interface IOrderServices
    {
        //Get orders from database
        Task<List<Order>> GetOrdersFromUserId(int userId);
        Task<List<Order>> GetOrdersFromPortfolioId(int userId);
        Task<List<Order>> GetOrdersFromUserId(int userId, DateTime dateFrom, DateTime dateTo);
        Task<List<Order>> GetOrdersFromPortfolioId(int userId, DateTime dateFrom, DateTime dateTo);

        //Get orders from exchanges
        Task<List<Order>> GetAllOrders(int userId);
        Task<List<Order>> RefreshAllOrders(int userId);
        Task<List<Order>> GetBitMEXOrdersFromUserId(int userId);
        Task<List<Order>> GetBitMEXOrdersFromUserId(int userId, DateTime dateFrom);
        Task<List<Order>> GetBinanceOrdersFromUserId(int userId);
        Task<List<Order>> GetBinanceOrdersFromUserId(int userId, int lastOrderId);
    }
}
