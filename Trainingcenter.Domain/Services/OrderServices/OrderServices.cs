using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.Repositories;

namespace Trainingcenter.Domain.Services.OrderServices
{
    class OrderServices : IOrderServices
    {
        #region DependencyInjection
        private readonly IOrderRepository _orderRepo;

        public OrderServices(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }
        #endregion

        public Task<List<Order>> GetOrdersFromUserId(int userId, DateTime dateFrom, DateTime dateTo)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> ResfreshOrdersFromUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetBinanceOrdersFromUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetBitMEXOrdersFromUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
