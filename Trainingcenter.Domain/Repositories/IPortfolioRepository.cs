using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Repositories
{
    public interface IPortfolioRepository
    {
        Task<Portfolio> GetPortfolioByIdAsync(int portfolioId);
        Task<Portfolio> GetDefaultPortfolioAsync(int userId);
        Task<List<Portfolio>> GetAllPortfolioByUserIdAsync(int userId);

        Task<bool> PortfolioOrderExists(int orderId, int portfolioId);
        Task<PortfolioOrder> GetPortfolioOrder(int orderId, int portfolioId);
    }
}
