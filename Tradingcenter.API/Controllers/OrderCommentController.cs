using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainingcenter.Domain.DTOs.OrderCommentDTOs;
using Trainingcenter.Domain.Services;
using Trainingcenter.Domain.Services.CommentServices;
using Trainingcenter.Domain.Services.OrderServices;

namespace Tradingcenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderCommentController : ControllerBase
    {
        private readonly ICommentServices _commentServices;
        private readonly IUserServices _userServices;
        private readonly IOrderServices _orderServices;
        private HtmlEncoder _htmlEncoder;
        private JavaScriptEncoder _javaScriptEncoder;
        private UrlEncoder _urlEncoder;

        public OrderCommentController(ICommentServices commentServices,
                                    IUserServices userServices,
                                    IOrderServices orderServices,
                                    HtmlEncoder htmlEncoder,
                                    JavaScriptEncoder javascriptEncoder,
                                    UrlEncoder urlEncoder)
        {
            _commentServices = commentServices;
            _userServices = userServices;
            _orderServices = orderServices;
            _htmlEncoder = htmlEncoder;
            _javaScriptEncoder = javascriptEncoder;
            _urlEncoder = urlEncoder;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var comments = await _commentServices.GetAllOrderCommentByUserId(userId);
            return StatusCode(200, comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var comment = await _commentServices.GetOrderCommentById(id);

            if (comment == null)
            {
                return StatusCode(400, "Comment with id " + id + " was not found.");
            }
            return StatusCode(200, comment);
        }

        [HttpPut]
        public async Task<IActionResult> Put(OrderCommentToCreateDTO comment)
        {
            var portfolio = await _orderServices.GetOrderById(comment.OrderId);

            if (portfolio == null)
            {
                return StatusCode(400, "Portfolio with id " + comment.OrderId + " was not found.");
            }

            comment.Message = _javaScriptEncoder.Encode(_htmlEncoder.Encode(comment.Message));

            comment.UserId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var createdComment = await _commentServices.CreateOrderComment(comment);
            return StatusCode(200);
        }

        [HttpPost]
        public async Task<IActionResult> Post(OrderCommentToUpdateDTO comment)
        {

            comment.Message = _javaScriptEncoder.Encode(_htmlEncoder.Encode(comment.Message));

            if (await _commentServices.GetOrderCommentById(comment.CommentId) == null)
            {
                return StatusCode(400, "Comment with id " + comment.CommentId + " was not found.");
            }
            await _commentServices.UpdateOrderComment(comment);
            return StatusCode(200);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int commentId)
        {
            var userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var comment = await _commentServices.GetOrderCommentById(commentId);
            if (comment == null)
            {
                return StatusCode(400, "Comment with id " + comment.CommentId + " was not found.");
            }
            if (comment.UserId != userId)
            {
                return StatusCode(401);
            }
            await _commentServices.DeleteOrderComment(commentId);
            return StatusCode(200);
        }



    }
}