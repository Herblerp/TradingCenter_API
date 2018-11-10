using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Repositories
{
    public interface IExchangeKeyRepository
    {
        Task<ExchangeKey> GetFromNameAsync(string name, int userId);
        Task<List<ExchangeKey>> GetAllFromUserIdAsync(int userId);
    }
}
