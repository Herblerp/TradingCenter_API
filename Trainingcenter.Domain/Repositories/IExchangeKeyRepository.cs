using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Repositories
{
    interface IExchangeKeyRepository
    {
        Task<ExchangeKey> GetFromIdAsync(int exchangeKeyId);

        Task<List<ExchangeKey>> GetAllFromUserIdAsync(int userId);
    }
}
