using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainingcenter.Domain.DTOs.CommentDTOs;
using Trainingcenter.Domain.Services;
using Trainingcenter.Domain.Services.CommentServices;
using Trainingcenter.Domain.Services.PortfolioServices;

namespace Tradingcenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentServices _commentServices;
        private readonly IUserServices _userServices;
        private readonly IPortfolioServices _portfolioServices;

        public CommentController(ICommentServices commentServices, IUserServices userServices, IPortfolioServices portfolioServices)
        {
            _commentServices = commentServices;
            _userServices = userServices;
            _portfolioServices = portfolioServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var comments = await _commentServices.GetAllCommentByUserId(userId);
            return StatusCode(200, comments);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var comment = await _commentServices.GetCommentById(id);

            if (comment == null)
            {
                return StatusCode(400, "Comment with id " + id + " was not found.");
            }
            return StatusCode(200, comment);
        }
        [HttpPut]
        public async Task<IActionResult> Put(CommentToCreateDTO comment)
        {
            var portfolio = await _portfolioServices.GetPortfolioByIdAsync(comment.PortfolioId);

            if(portfolio == null)
            {
                return StatusCode(400, "Portfolio with id " + comment.PortfolioId + " was not found.");
            }
            comment.UserId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var createdComment = await _commentServices.CreateComment(comment);
            return StatusCode(200);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CommentToUpdateDTO comment)
        {
            if(await _commentServices.GetCommentById(comment.CommentId) == null)
            {
                return StatusCode(400, "Comment with id " + comment.CommentId + " was not found.");
            }
            await _commentServices.UpdateComment(comment);
            return StatusCode(200);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int commentId)
        {
            var userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var comment = await _commentServices.GetCommentById(commentId);
            if (comment == null)
            {
                return StatusCode(400, "Comment with id " + comment.CommentId + " was not found.");
            }
            if(comment.UserId != userId)
            {
                return StatusCode(401);
            }
            await _commentServices.DeleteComment(commentId);
            return StatusCode(200);
        }
    }
}