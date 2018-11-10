using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Services.OrderServices
{
    interface IOrderServices
    {
        //Get orders from exchanges
        Task<List<Order>> GetBitMEXOrdersFromUserId(int userId);
        Task<List<Order>> GetBinanceOrdersFromUserId(int userId);
    }
}
