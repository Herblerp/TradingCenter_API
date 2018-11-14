using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.ExchangeKeyDTOs;
using Trainingcenter.Domain.Repositories;

namespace Trainingcenter.Domain.Services.ExchangeKeyServices
{
    public class ExchangeKeyServices : IExchangeKeyServices
    {

        #region DependencyInjection

        private readonly IExchangeKeyRepository _keyRepo;
        private readonly IGenericRepository _genericRepo;

        public ExchangeKeyServices(IExchangeKeyRepository keyRepo, IGenericRepository genericRepo)
        {
            _keyRepo = keyRepo;
            _genericRepo = genericRepo;
        }

        #endregion

        public async Task<List<ExchangeKeyDTO>> GetExchangeKeys(int userId)
        {
            var keyList = await _keyRepo.GetAllFromUserIdAsync(userId);
            var keyDTOList = new List<ExchangeKeyDTO>();

            foreach(ExchangeKey key in keyList)
            {
                keyDTOList.Add(ConvertExchangeKey(key));
            }

            return keyDTOList;
        }

        public async Task<ExchangeKeyDTO> CreateExchangeKey(ExchangeKeyToCreateDTO key, int userId)
        {
            //CHeck exchange name

            var keyToCreate = ConvertExchangeKeyToCreate(key);
            keyToCreate.UserId = userId;

            await _genericRepo.AddAsync(keyToCreate);

            return ConvertExchangeKey(keyToCreate);
        }

        #region Converters

        private ExchangeKeyDTO ConvertExchangeKey(ExchangeKey dto)
        {
            var key = new ExchangeKeyDTO
            {
                ExchangeKeyId = dto.ExchangeKeyId,
                Name = dto.Name,
                Key = dto.Key,
                Secret = dto.Secret
            };

            return key;
        }
        private ExchangeKey ConvertExchangeKeyToCreate(ExchangeKeyToCreateDTO dto)
        {
            var key = new ExchangeKey
            {
                Name = dto.Name,
                Key = dto.Key,
                Secret = dto.Secret
            };
            return key;
        }
        #endregion

    }
}
