using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Repositories
{
    public interface IPurchasedPortfolioRepository
    {
        Task<List<PurchasedPortfolio>> GetPurchasedPortfoliosByUserId(int userId);
        Task<List<PurchasedPortfolio>> GetPortfolioPurchases(int portfolioId);
        Task<PurchasedPortfolio> GetPurchasedPortfolio(int userId, int portfolioId);
        Task<bool> Exists(int userId, int portfolioId);
    }
}
