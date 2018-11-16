using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.PortfolioDTO_s;
using Trainingcenter.Domain.DTOs.PortfolioOrderDTOs;
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

        public async Task<Portfolio> GetPortfolioByIdAsync(int portfolioId)
        {
            return await _portfolioRepo.GetPortfolioByIdAsync(portfolioId);
        }

        public async Task<List<PortfolioDTO>> GetAllPortfolioByUserIdAsync(int userId)
        {
            var portfolioList = await _portfolioRepo.GetAllPortfolioByUserIdAsync(userId);
            var portfolioDTOList = new List<PortfolioDTO>();

            foreach(Portfolio portfolio in portfolioList)
            {
                portfolioDTOList.Add(ConvertPortfolio(portfolio));
            }
            return portfolioDTOList;
        }

        public async Task<PortfolioDTO> CreatePortfolioAsync(PortfolioToCreateDTO portfolioToCreate, int userId)
        {
            var portfolio = new Portfolio
            {
                UserId = userId,
                Name = portfolioToCreate.Name,
                Description = portfolioToCreate.Description,
                Goal = portfolioToCreate.Goal
            };
            return ConvertPortfolio(await _genericRepo.AddAsync(portfolio));
        }

        public async Task<PortfolioDTO> UpdatePortfolioAsync(PortfolioDTO portfolioToUpdate)
        {
            var portfolio = await _portfolioRepo.GetPortfolioByIdAsync(portfolioToUpdate.PortfolioId);

            portfolio.Name = portfolioToUpdate.Name;
            portfolio.Description = portfolioToUpdate.Description;
            portfolio.Goal = portfolioToUpdate.Goal;

            return ConvertPortfolio(await _genericRepo.UpdateAsync(portfolio));
        }

        public async Task<bool> DeletePortfolioAsync(int portfolioId)
        {
            var portfolio = await _portfolioRepo.GetPortfolioByIdAsync(portfolioId);

            try
            {
                await _genericRepo.DeleteAsync(portfolio);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<PortfolioOrder> AddOrderById(PortfolioOrderDTO po)
        {
            var portfolioOrder = new PortfolioOrder
            {
                OrderId = po.OrderId,
                PortfolioId = po.PortfolioId
            };
            return await _genericRepo.AddAsync(portfolioOrder);
        }

        public async Task<bool> PortfolioOrderExists(int orderId, int portfolioId)
        {
            return (await _portfolioRepo.PortfolioOrderExists(orderId, portfolioId));
        }

        #endregion

        #region Converters

        private PortfolioDTO ConvertPortfolio(Portfolio portfolio)
        {
            var portfolioDTO = new PortfolioDTO
            {
                Name = portfolio.Name,
                Description = portfolio.Description,
                Goal = portfolio.Goal
            };
            return portfolioDTO;
        }

        #endregion
    }
}
