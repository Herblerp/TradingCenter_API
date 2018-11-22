using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.NoteDTOs;

namespace Trainingcenter.Domain.Services.NoteServices
{
    public interface INoteServices
    {
        Task<Note> CreateNote(NoteToCreateDTO note);
        Task<Note> UpdateNote(NoteToUpdateDTO note);
        Task<Note> DeleteNote(int noteId);

        Task<NoteDTO> GetNoteById(int noteId);
        Task<List<NoteDTO>> GetAllNoteByPortfolioId(int portfolioId);

        Task<bool> NoteExists(int noteId);
    }
}
