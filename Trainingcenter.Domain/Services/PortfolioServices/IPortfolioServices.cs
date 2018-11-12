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
        Task<PortfolioDTO> CreatePortfolio(PortfolioToCreateDTO portfolioToCreate, int userId);
        Task<PortfolioDTO> UpdatePortfolio(Portfolio portfolioToUpdate, PortfolioDTO portfolioForUpdate);
    }
}
