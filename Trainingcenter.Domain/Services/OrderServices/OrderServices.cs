using System;
using System.Collections.Generic;
using System.Text;
using Trainingcenter.Domain.Repositories;

namespace Trainingcenter.Domain.Services.OrderServices
{
    class OrderServices
    {
        private readonly IOrderRepository _orderRepo;

        public OrderServices(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

    }
}
