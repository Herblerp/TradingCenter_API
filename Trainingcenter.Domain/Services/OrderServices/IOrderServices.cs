using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Services.OrderServices
{
    interface IOrderServices
    {
        //Methods for use in controllers
        Task<List<Order>> GetOrdersFromUserId(int userId, DateTime dateFrom, DateTime dateTo);
        Task<List<Order>> ResfreshOrdersFromUserId(int userId);

        //Get orders from exchanges
        Task<List<Order>> GetBitMEXOrdersFromUserId(int userId);
        Task<List<Order>> GetBinanceOrdersFromUserId(int userId);
    }
}
