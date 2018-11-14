using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.ExchangeKeyDTOs;

namespace Trainingcenter.Domain.Services.ExchangeKeyServices
{
    public interface IExchangeKeyServices
    {
        Task<List<ExchangeKeyDTO>> GetExchangeKeys(string name, int userId);
        Task<bool> CreateExchangeKey(ExchangeKeyToCreateDTO key, int userId);
        Task<bool> KeyExists(string name, string keyStr, int userId);
    }
}
