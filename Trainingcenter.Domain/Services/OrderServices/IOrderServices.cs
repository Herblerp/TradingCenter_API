using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Services.OrderServices
{
    interface IOrderServices
    {
        Task<List<Order>> GetOrdersFromUserId(int userId, DateTime dateFrom, DateTime dateTo);
        Task<List<Order>> ResfreshOrdersFromUserId(int userId);
    }
}
