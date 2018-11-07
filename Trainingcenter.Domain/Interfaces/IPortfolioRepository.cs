using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Interfaces
{
    interface IPortfolioRepository
    {
        Task<Portfolio> GetFromIdAsync(int portfolioId);

        Task<List<Portfolio>> GetAllFromUserIdAsync(int userId);
    }
}
