using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.Repositories;

namespace Tradingcenter.Data.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        #region DependecyInjection

        private readonly DataContext _context;

        public PortfolioRepository (DataContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<Portfolio> GetPortfolioByIdAsync(int portfolioId)
        {
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(x => x.PortfolioId == portfolioId);
            return portfolio;
        }

        public async Task<Portfolio> GetDefaultPortfolioAsync(int userId)
        {
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(x => x.UserId == userId && x.IsDefault == true);

            return portfolio;
        }

        public async Task<List<Portfolio>> GetAllPortfolioByUserIdAsync(int userId)
        {
            var portfolios = await _context.Portfolios.Where(x => x.UserId == userId).ToListAsync();
            return portfolios;
        }

        public async Task<bool> PortfolioOrderExists(int orderId, int portfolioId)
        {
            var op = await _context.OrderPortolios.FirstOrDefaultAsync(x => x.OrderId == orderId && x.PortfolioId == portfolioId);

            if (op == null)
            {
                return false;
            }
            return true;
        }

        public async Task<PortfolioOrder> GetPortfolioOrder(int orderId, int portfolioId)
        {
            PortfolioOrder po = await _context.OrderPortolios.FirstOrDefaultAsync(x => x.OrderId == orderId && x.PortfolioId == portfolioId);
            return po;
        }
    }
}
