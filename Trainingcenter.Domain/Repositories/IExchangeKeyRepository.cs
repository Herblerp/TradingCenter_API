using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Repositories
{
    public interface IExchangeKeyRepository
    {
        Task<ExchangeKey> GetKeyFromKeyStrAsync(string name, string keyStr, int userId);
        Task<List<ExchangeKey>> GetKeysFromNameAsync(string name, int userId);
        Task<List<ExchangeKey>> GetKeysFromUserIdAsync(int userId);
    }
}
