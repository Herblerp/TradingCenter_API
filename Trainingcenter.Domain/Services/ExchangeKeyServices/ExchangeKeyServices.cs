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

        #region Services

        public async Task<List<ExchangeKeyDTO>> GetExchangeKeys(string name, int userId)
        {
            var keyList = new List<ExchangeKey>();

            if (name == null)
            {
                keyList = await _keyRepo.GetKeysFromUserIdAsync(userId);
            }
            else
            {
                keyList = await _keyRepo.GetKeysFromNameAsync(name, userId);
            }
            var keyDTOList = new List<ExchangeKeyDTO>();

            foreach (ExchangeKey key in keyList)
            {
                keyDTOList.Add(ConvertExchangeKey(key));
            }

            return keyDTOList;
        }

        public async Task<bool> CreateExchangeKey(ExchangeKeyToCreateDTO key, int userId)
        {
            try
            {
                var keyToCreate = ConvertExchangeKeyToCreate(key);
                keyToCreate.UserId = userId;

                await _genericRepo.AddAsync(keyToCreate);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> KeyExists(string name, string keyStr, int userId)
        {
            var key = await _keyRepo.GetKeyFromKeyStrAsync(name, keyStr, userId);
            if (key == null)
            {
                return false;
            }
            return true;
        }

        #endregion

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
