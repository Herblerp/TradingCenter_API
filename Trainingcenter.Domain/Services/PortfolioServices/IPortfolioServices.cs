using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.PortfolioDTO_s;
using Trainingcenter.Domain.DTOs.PortfolioOrderDTOs;

namespace Trainingcenter.Domain.Services.PortfolioServices
{
    public interface IPortfolioServices
    {
        Task<Portfolio> GetPortfolioByIdAsync(int portfolioId);
        Task<List<PortfolioDTO>> GetAllPortfolioByUserIdAsync(int userId);
        Task<PortfolioDTO> CreatePortfolioAsync(PortfolioToCreateDTO portfolioToCreate, int userId);
        Task<PortfolioDTO> UpdatePortfolioAsync(PortfolioDTO portfolioToUpdate);
        Task<bool> DeletePortfolioAsync(int portfolioId);
        Task<PortfolioOrder> AddOrderById(PortfolioOrderDTO po);
        Task<bool> IsOrderInPortfolio(int orderId, int portfolioId);
    }
}
