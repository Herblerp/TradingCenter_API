using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.Repositories;

namespace Tradingcenter.Data.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly DataContext _context;

        public NoteRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Note>> GetAllFromPortfolioIdAsync(int portfolioId)
        {
            var noteList = await _context.Notes.Where(x => x.PortfolioId == portfolioId).ToListAsync();
            return noteList;
        }

        public async Task<Note> GetFromIdAsync(int noteId)
        {
            Note note = await _context.Notes.FirstOrDefaultAsync(x => x.NoteId == noteId);
            return note;
        }
    }
}
