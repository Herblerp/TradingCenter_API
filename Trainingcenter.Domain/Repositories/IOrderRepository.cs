using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Repositories
{
    interface IOrderRepository
    {
        Task<Order> GetFromIdAsync(int id);

        Task<List<Order>> GetAllFromUserIdAsync(int userId);
        Task<List<Order>> GetAllFromPortfolioIdAsync(int portfolioId);
    }
}
