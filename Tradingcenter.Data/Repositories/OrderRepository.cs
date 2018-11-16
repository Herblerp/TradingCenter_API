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

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);
        }

        public async Task<List<Order>> SaveOrdersAsync(List<Order> orders)
        {
            foreach(var order in orders)
            {
                var orderFromDB = await _context.Orders.FirstOrDefaultAsync(x => x.UserId == order.UserId&&x.ExchangeOrderId == order.ExchangeOrderId && x.Exchange == order.Exchange);
                if(orderFromDB == null)
                {
                    await _context.Orders.AddAsync(order);
                }
                await _context.SaveChangesAsync();
            }
            return orders;
        }

        public async Task<List<Order>> GetOrdersFromUserIdAsync(int userId)
        {
            var orderList = await _context.Orders.Where(x => x.UserId == userId).ToListAsync();

            return orderList;
        }

        public async Task<List<Order>> GetOrdersFromPortfolioIdAsync(int portfolioId)
        {
            var orderIdList = await _context.OrderPortolios.Where(x => x.PortfolioId == portfolioId).ToListAsync();
            var orderList = new List<Order>();

            foreach(PortfolioOrder op in orderIdList)
            {
                orderList.Add(await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == op.OrderId));
            }

            return orderList;
        }

        public async Task<List<Order>> GetOrdersFromUserIdAsync(int userId, DateTime dateFrom, DateTime dateTo)
        {
            var orderList = await _context.Orders.Where(x =>
                x.UserId == userId &&
                x.Timestamp.Date >= dateFrom.Date &&
                x.Timestamp.Date <= dateTo.Date).ToListAsync();

            return orderList;
        }

        public async Task<List<Order>> GetOrdersFromPortfolioIdAsync(int portfolioId, DateTime dateFrom, DateTime dateTo)
        {
            var orderIdList = await _context.OrderPortolios.Where(x => x.PortfolioId == portfolioId).ToListAsync();
            var orderList = new List<Order>();

            foreach (PortfolioOrder op in orderIdList)
            {
                var order = await _context.Orders.FirstOrDefaultAsync(x =>
                    x.OrderId == op.OrderId &&
                    x.Timestamp.Date >= dateFrom.Date &&
                    x.Timestamp.Date <= dateTo.Date);

                if (order != null)
                { 
                    orderList.Add(order);
                }
            }

            return orderList;
        }
    }
}
