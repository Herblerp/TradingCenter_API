using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Repositories
{
    public interface IPortfolioRepository
    {
        Task<Portfolio> GetFromIdAsync(int portfolioId);
        Task<Portfolio> GetFromNameAsync(string name, int userId);

        Task<List<Portfolio>> GetAllFromUserIdAsync(int userId);
    }
}
