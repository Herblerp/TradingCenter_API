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
    public class PurchasedPortfolioRepository : IPurchasedPortfolioRepository
    {
        private readonly DataContext _context;

        public PurchasedPortfolioRepository (DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Exists(int userId, int portfolioId)
        {
            var purchase = await _context.PurchasedPortfolios.FirstOrDefaultAsync(x => x.UserId == userId && x.PortfolioId == portfolioId);

            if(purchase == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<PurchasedPortfolio>> GetPurchasedPortfoliosByUserId(int userId)
        {
            var purchaseList = await _context.PurchasedPortfolios.Where(x => x.UserId == userId).ToListAsync();

            return purchaseList;
        }

        public async Task<PurchasedPortfolio> GetPurchasedPortfolio(int userId, int portfolioId)
        {
            var purchase = await _context.PurchasedPortfolios.FirstOrDefaultAsync(x => x.UserId == userId && x.PortfolioId == portfolioId);
            return purchase;
        }
    }
}
