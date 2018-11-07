using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;

namespace Trainingcenter.Domain.Repositories
{
    interface INoteRepository
    {
        Task<Note> GetFromIdAsync(int noteId);

        Task<List<Note>> GetAllFromPortfolioIdAsync(int portfolioId);
    }
}
