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
    public class OrderRepository : IOrderRepository
    {
        #region DependecyInjection

        private readonly DataContext _context;
        private readonly IGenericRepository _genericRepo;

        public OrderRepository(DataContext context, IGenericRepository genericRepo)
        {
            _context = context;
            _genericRepo = genericRepo;
        }
        #endregion

        public async Task<List<Order>> SaveOrdersAsync(List<Order> orders)
        {
            var savedOrders = new List<Order>();

            foreach(var order in orders)
            {
                var orderFromDB = await _context.Orders.FirstOrDefaultAsync(x => x.ExchangeOrderId == order.ExchangeOrderId && x.Exchange == order.Exchange);
                if(orderFromDB == null)
                {
                    await _genericRepo.AddAsync(order);
                    savedOrders.Add(order);
                }
            }
            return savedOrders;
        }

        public async Task<List<Order>> GetOrdersFromUserIdAsync(int userId)
        {
            var orderList = await _context.Orders.Where(x => x.UserId == userId).ToListAsync();

            return orderList;
        }

        public async Task<List<Order>> GetOrdersFromPortfolioIdAsync(int portfolioId)
        {
            var orderList = await _context.Orders.Where(x => x.PortfolioId == portfolioId).ToListAsync();

            return orderList;
        }

        public async Task<List<Order>> GetOrdersFromUserIdAsync(int userId, DateTime dateFrom, DateTime dateTo)
        {
            var orderList = await _context.Orders.Where(x =>
                x.UserId == userId &&
                x.Timestamp > dateFrom &&
                x.Timestamp < dateTo).ToListAsync();

            return orderList;
        }

        public async Task<List<Order>> GetOrdersFromPortfolioIdAsync(int portfolioId, DateTime dateFrom, DateTime dateTo)
        {
            var orderList = await _context.Orders.Where(x =>
                x.PortfolioId == portfolioId &&
                x.Timestamp > dateFrom &&
                x.Timestamp < dateTo).ToListAsync();

            return orderList;
        }
    }
}
