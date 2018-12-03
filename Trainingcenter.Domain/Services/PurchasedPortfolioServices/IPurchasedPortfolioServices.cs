using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.PurchasedPortfolioDTOs;

namespace Trainingcenter.Domain.Services.PurchasedPortfolioServices
{
    public interface IPurchasedPortfolioServices
    {
        Task<PurchasedPortfolioDTO> AddPurchasedPortfolio(PurchasedPortfolioDTO pp);
        Task<PurchasedPortfolioDTO> RemovePurchasedPortfolio(PurchasedPortfolioDTO pp);
        Task<bool> Exists(int userId, int portfolioId);
        Task<List<PurchasedPortfolioDTO>> GetPurchasedPortfoliosById(int userId);
    }
}
