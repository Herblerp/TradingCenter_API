using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.Repositories;

namespace Tradingcenter.Data.Repositories
{
    class GenericRepository : IGenericRepository
    {
        public Task AddAsync<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
