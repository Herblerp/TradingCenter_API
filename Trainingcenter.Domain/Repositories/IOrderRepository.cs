using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllFromUserIdAsync(int userId);
        Task<List<Order>> GetAllFromPortfolioIdAsync(int portfolioId);
    }
}
