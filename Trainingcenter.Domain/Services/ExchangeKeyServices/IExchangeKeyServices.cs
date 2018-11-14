using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DTOs.ExchangeKeyDTOs;

namespace Trainingcenter.Domain.Services.ExchangeKeyServices
{
    public interface IExchangeKeyServices
    {
        Task<List<ExchangeKeyDTO>> GetExchangeKeys(int userId);
        Task<ExchangeKeyDTO> CreateExchangeKey(ExchangeKeyToCreateDTO key, int userId);
    }
}
