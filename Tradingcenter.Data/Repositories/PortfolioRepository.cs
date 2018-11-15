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
        private readonly DataContext _context;

        public PortfolioRepository (DataContext context)
        {
            _context = context;
        }

        public async Task<List<Portfolio>> GetAllPortfolioByUserIdAsync(int userId)
        {
            var portfolios = await _context.Portfolios.Where(x => x.UserId == userId).ToListAsync();
            return portfolios;
        }

        public async Task<Portfolio> GetPortfolioByIdAsync(int portfolioId)
        {
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(x => x.PortfolioId == portfolioId);
            return portfolio;
        }

        public async Task<Portfolio> GetFromNameAsync(string name, int userId)
        {
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(x => x.Name == name && x.UserId == userId);

            return portfolio;
        }
    }
}
