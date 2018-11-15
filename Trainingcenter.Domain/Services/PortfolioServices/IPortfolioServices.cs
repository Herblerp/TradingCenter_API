using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.PortfolioDTO_s;

namespace Trainingcenter.Domain.Services.PortfolioServices
{
    public interface IPortfolioServices
    {
        Task<List<PortfolioDTO>> GetAllPortfolioByUserIdAsync(int userId);
        Task<PortfolioDTO> CreatePortfolioAsync(PortfolioToCreateDTO portfolioToCreate, int userId);
        Task<PortfolioDTO> UpdatePortfolioAsync(PortfolioDTO portfolioToUpdate);
        Task<bool> DeletePortfolioAsync(int portfolioId);
        Task<PortfolioOrder> AddOrderById(int portfolioId, int orderId);
    }
}
