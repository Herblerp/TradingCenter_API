using System;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.PortfolioDTO_s;
using Trainingcenter.Domain.Repositories;

namespace Trainingcenter.Domain.Services.PortfolioServices
{
    public class PortfolioServices : IPortfolioServices
    {
        #region DependencyInjection

        private readonly IGenericRepository _genericRepo;
        private readonly IPortfolioRepository _portfolioRepo;

        public PortfolioServices(IGenericRepository genericRepo, IPortfolioRepository portfolioRepo)
        {
            _genericRepo = genericRepo;
            _portfolioRepo = portfolioRepo;
        }

        #endregion

        #region Services

        public async Task<PortfolioDTO> CreatePortfolio(PortfolioToCreateDTO portfolioToCreate, int userId)
        {
            var portfolio = new Portfolio
            {
                UserId = userId,
                Name = portfolioToCreate.Name,
                Description = portfolioToCreate.Description,
                Goal = portfolioToCreate.Goal
            };

            await _genericRepo.AddAsync(portfolio);
            return Convert(portfolioToCreate);
        }

        public async Task<PortfolioDTO> UpdatePortfolio(PortfolioToUpdateDTO portfolioToUpdate, int userId)
        {
            var portfolio = await _portfolioRepo.GetFromNameAsync(portfolioToUpdate.Name, userId);

            portfolio.Name = portfolioToUpdate.Name;
            portfolio.Description = portfolioToUpdate.Description;
            portfolio.Goal = portfolioToUpdate.Goal;

            await _genericRepo.UpdateAsync(portfolio);

            return Convert(portfolio);
        }

        public async Task<bool> PortfolioExists(string name, int userId)
        {
            var portfolio = await _portfolioRepo.GetFromNameAsync(name, userId);
            if (portfolio == null)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Helpers

        #endregion

        #region Converters

        private PortfolioDTO Convert(Portfolio portfolio)
        {
            var portfolioDTO = new PortfolioDTO
            {
                Name = portfolio.Name,
                Description = portfolio.Description,
                Goal = portfolio.Goal
            };
            return portfolioDTO;
        }

        private PortfolioDTO Convert(PortfolioToCreateDTO portfolioToCreate)
        {
            var portfolio = new PortfolioDTO
            {
                Name = portfolioToCreate.Name,
                Description = portfolioToCreate.Description,
                Goal = portfolioToCreate.Goal
            };
            return portfolio;
        }

        #endregion



    }
}
