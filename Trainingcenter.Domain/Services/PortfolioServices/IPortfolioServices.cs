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
        Task<PortfolioDTO> CreateDefaultPortfolio(int userId);
        Task<PortfolioDTO> UpdatePortfolio(PortfolioToUpdateDTO portfolioToUpdate, int userId);
        Task<bool> PortfolioExists(string name, int userId);
    }
}
