using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.Entities;

namespace Trainingcenter.Domain.Repositories
{
    public interface IGenericRepository
    {
        Task AddAsync<T>(T entity) where T: class;
        Task DeleteAsync<T>(T entity) where T : class;
    }
}
