using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.OrderDTO_s;

namespace Trainingcenter.Domain.Services.OrderServices
{
    public interface IOrderServices
    {
        //Service methods
        Task<List<OrderDTO>> GetOrders(int userId, int portfolioId, int amount, string dateFrom, string dateTo);

        //Exchange methods
        Task<List<Order>> GetAllOrders(int userId);
        Task<List<Order>> RefreshAllOrders(int userId);
        Task<List<Order>> GetBitMEXOrdersFromUserId(int userId);
        Task<List<Order>> GetBitMEXOrdersFromUserId(int userId, DateTime dateFrom);
        Task<List<Order>> GetBinanceOrdersFromUserId(int userId);
        Task<List<Order>> GetBinanceOrdersFromUserId(int userId, int lastOrderId);
    }
}
