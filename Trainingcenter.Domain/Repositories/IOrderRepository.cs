using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> SaveOrders(List<Order> orders);

        Task<List<Order>> GetOrdersFromUserIdAsync(int userId);
        Task<List<Order>> GetOrdersFromPortfolioIdAsync(int portfolioId);

        Task<List<Order>> GetOrdersFromUserIdAsync(int userId, DateTime dateFrom, DateTime dateTo);
        Task<List<Order>> GetOrdersFromPortfolioIdAsync(int portfolioId, DateTime dateFrom, DateTime dateTo);
    }
}
