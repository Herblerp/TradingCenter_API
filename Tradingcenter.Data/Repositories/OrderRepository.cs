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
    class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllFromPortfolioIdAsync(int portfolioId)
        {
            var orderList = await _context.Orders.Where(x => x.PortfolioId == portfolioId).ToListAsync();
            orderList = orderList.OrderByDescending(x => x.Timestamp).ToList();

            return orderList;
        }

        public async Task<List<Order>> GetAllFromUserIdAsync(int userId)
        {
            var orderList = await _context.Orders.Where(x => x.UserId == userId).ToListAsync();
            orderList = orderList.OrderByDescending(x => x.Timestamp).ToList();

            return orderList;
        }

        public async Task<List<Order>> GetAllFromDatePortfolioIdAsync(int portfolioId, DateTime dateFrom, DateTime dateTo)
        {
            var orderList = await _context.Orders.Where(x =>
                x.PortfolioId == portfolioId &&
                x.Timestamp > dateFrom &&
                x.Timestamp < dateTo).ToListAsync();

            orderList = orderList.OrderByDescending(x => x.Timestamp).ToList();

            return orderList;
        }

        public async Task<List<Order>> GetAllFromDateUserIdAsync(int userId, DateTime dateFrom, DateTime dateTo)
        {
            var orderList = await _context.Orders.Where(x =>
                x.PortfolioId == userId &&
                x.Timestamp > dateFrom &&
                x.Timestamp < dateTo).ToListAsync();

            orderList = orderList.OrderByDescending(x => x.Timestamp).ToList();

            return orderList;
        }

    }
}
