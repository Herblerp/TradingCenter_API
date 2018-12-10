using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainingcenter.Domain.DTOs.NoteDTOs;
using Trainingcenter.Domain.Services.NoteServices;
using Trainingcenter.Domain.Services.PortfolioServices;

namespace Tradingcenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteServices _noteServices;
        private readonly IPortfolioServices _portfolioServices;
        private HtmlEncoder _htmlEncoder;
        private JavaScriptEncoder _javaScriptEncoder;

        public NoteController(  INoteServices noteServices, 
                                IPortfolioServices portfolioServices, 
                                HtmlEncoder htmlEncoder,
                                JavaScriptEncoder javascriptEncoder)
        {
            _noteServices = noteServices;
            _portfolioServices = portfolioServices;
            _htmlEncoder = htmlEncoder;
            _javaScriptEncoder = javascriptEncoder;
        }

        [HttpPut]
        public async Task<IActionResult> Put(NoteToCreateDTO noteToCreate)
        {
            try
            {
                noteToCreate.Message = _htmlEncoder.Encode(_javaScriptEncoder.Encode(noteToCreate.Message));
                int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var portfolio = await _portfolioServices.GetPortfolioByIdAsync(noteToCreate.PortfolioId);

                if(portfolio == null)
                {
                    return StatusCode(400, "Portfolio with id " + noteToCreate.PortfolioId + " does not exist");
                }
                if (portfolio.UserId == userId)
                { 
                    await _noteServices.CreateNote(noteToCreate);
                    return StatusCode(201);
                }
                return StatusCode(401);
            }
            catch
            {
                return StatusCode(500, "Something went wrong while attempting to create a note");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(NoteToUpdateDTO noteToUpdate)
        {
            try
            {
                noteToUpdate.message = _htmlEncoder.Encode(_javaScriptEncoder.Encode(noteToUpdate.message));
                int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var note = await _noteServices.GetNoteById(noteToUpdate.noteId);

                if (note == null)
                {
                    return StatusCode(400, "Note with id " + noteToUpdate.noteId + " was not found.");
                }

                var portfolio = await _portfolioServices.GetPortfolioByIdAsync(note.PortfolioId);

                if (portfolio == null)
                {
                    return StatusCode(500, "Portfolio was not found");
                }

                if(note.PortfolioId != portfolio.PortfolioId)
                {
                    return StatusCode(400, "Note with id " + note.NoteId + " does not belong in portfolio with id " + portfolio.PortfolioId);
                }

                if (portfolio.UserId == userId)
                {
                    await _noteServices.UpdateNote(noteToUpdate);
                    return StatusCode(200);
                }
                return StatusCode(401);
            }
            catch
            {
                return StatusCode(500, "Somthing went wrong while attempting to update note");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([Required]int noteId)
        {
            try
            {
                int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var note = await _noteServices.GetNoteById(noteId);

                if(note == null)
                {
                    return StatusCode(400, "Note with id " + noteId + " was not found.");
                }

                var portfolio = await _portfolioServices.GetPortfolioByIdAsync(note.PortfolioId);

                if (portfolio == null)
                {
                    return StatusCode(500, "Portfolio was not found");
                }
                if (portfolio.UserId == userId)
                {
                    await _noteServices.DeleteNote(noteId);
                    return StatusCode(200);
                }
                return StatusCode(401);
            }
            catch
            {
                return StatusCode(500, "Somthing went wrong while attempting to delete note");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([Required]int portfolioId)
        {
            try
            {
                int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var portfolio = await _portfolioServices.GetPortfolioByIdAsync(portfolioId);

                if (portfolio == null)
                {
                    return StatusCode(400, "Portfolio with id " + portfolioId + " does not exist");
                }
                if (portfolio.UserId == userId)
                {
                    var notes = await _noteServices.GetAllNoteByPortfolioId(portfolioId);
                    return StatusCode(200, notes);
                }
                return StatusCode(401);
            }
            catch
            {
                return StatusCode(500, "Somthing went wrong while attempting to get notes");
            }
        }
    }
}