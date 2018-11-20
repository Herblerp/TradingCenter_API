using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.NoteDTOs;
using Trainingcenter.Domain.Repositories;

namespace Trainingcenter.Domain.Services.NoteServices
{
    public class NoteServices : INoteServices
    {
        #region DependencyInjection

        private readonly IGenericRepository _genericRepo;
        private readonly INoteRepository _noteRepo;

        public NoteServices(IGenericRepository genericRepo, INoteRepository noteRepo)
        {
            _genericRepo = genericRepo;
            _noteRepo = noteRepo;
        }
        #endregion

        public async Task<Note> CreateNote(NoteToCreateDTO noteToCreate)
        {
            Note note = ConvertNoteToCreateDTO(noteToCreate);
            return await _genericRepo.AddAsync(note);
        }

        public async Task<Note> DeleteNote(int noteId)
        {
            Note note = await _noteRepo.GetFromIdAsync(noteId);
            return await _genericRepo.DeleteAsync(note);
        }

        public async Task<Note> UpdateNote(NoteToUpdateDTO noteToUpdate)
        {
            Note note = await _noteRepo.GetFromIdAsync(noteToUpdate.noteId);
            note.Message = noteToUpdate.message;

            return await _genericRepo.UpdateAsync(note);
        }

        public async Task<List<NoteDTO>> GetAllNoteByPortfolioId(int portfolioId)
        {
            var noteList = await _noteRepo.GetAllFromPortfolioIdAsync(portfolioId);
            var noteDTOList = new List<NoteDTO>();

            foreach(Note note in noteList)
            {
                noteDTOList.Add(ConvertNote(note));
            }
            return noteDTOList;
        }

        public async Task<NoteDTO> GetNoteById(int noteId)
        {
            Note note = await _noteRepo.GetFromIdAsync(noteId);
            NoteDTO noteDTO = ConvertNote(note);
            return noteDTO;
        }

        public async Task<bool> NoteExists(int noteId)
        {
            Note note = await _noteRepo.GetFromIdAsync(noteId);
            if(note != null)
            {
                return true;
            }
            return false;
        }

        #region Converters

        private NoteDTO ConvertNote(Note note)
        {
            if (note == null)
                return null;

            NoteDTO noteDTO = new NoteDTO
            {
                NoteId = note.NoteId,
                PortfolioId = note.PortfolioId,
                Message = note.Message
            };
            return noteDTO;
        }

        private Note ConvertNoteToCreateDTO(NoteToCreateDTO noteToCreate)
        {
            if (noteToCreate == null)
                return null;

            Note note = new Note
            {
                Message = noteToCreate.Message,
                PortfolioId = noteToCreate.PortfolioId
            };
            return note;
        }

        private Note ConvertNoteToUpdateDTO(NoteToUpdateDTO noteToUpdate)
        {
            if (noteToUpdate == null)
                return null;

            Note note = new Note
            {
                NoteId = noteToUpdate.noteId,
                Message = noteToUpdate.message
            };
            return note;
        }

        #endregion
    }
}
