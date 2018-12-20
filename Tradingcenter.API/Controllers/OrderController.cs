using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.OrderDTO_s;
using Trainingcenter.Domain.Services.OrderServices;
using Trainingcenter.Domain.Services.PortfolioServices;
using Trainingcenter.Domain.Services.CommentServices;
using Trainingcenter.Domain.Services.PurchasedPortfolioServices;

namespace Tradingcenter.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        private readonly IPortfolioServices _portfolioServices;
        private readonly IPurchasedPortfolioServices _ppServices;
        private readonly ICommentServices _commentServices;
        private HtmlEncoder _htmlEncoder;
        private JavaScriptEncoder _javaScriptEncoder;
        private UrlEncoder _urlEncoder;

        public OrderController( IOrderServices orderService, 
                                IPortfolioServices portfolioServices,
                                ICommentServices commentServices,
                                IPurchasedPortfolioServices ppservices,
                                HtmlEncoder htmlEncoder,
                                JavaScriptEncoder javascriptEncoder,
                                UrlEncoder urlEncoder)
        {
            _orderServices = orderService;
            _portfolioServices = portfolioServices;
            _commentServices = commentServices;
            _ppServices = ppservices;
            _htmlEncoder = htmlEncoder;
            _javaScriptEncoder = javascriptEncoder;
            _urlEncoder = urlEncoder;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetOrders(int portfolioId, int amount, string dateFrom, string dateTo)
        {
            try
            {
                int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var orderList = await _orderServices.GetOrders(userId, portfolioId, amount, dateFrom, dateTo);
                if (orderList == null)
                {
                    return StatusCode(401, "Invalid input");
                }
                return StatusCode(200, orderList);
            }
            catch
            {
                return StatusCode(500, "Something went wrong while attempting to get orders");
            }
        }
        [HttpGet("getFromSold")]
        public async Task<IActionResult> GetFromSold(int portfolioId)
        {
            int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var portfolio = await _portfolioServices.GetPortfolioByIdAsync(portfolioId);
            var orders = await _orderServices.GetOrders(portfolio.UserId, portfolioId, 0, null, null);

            if (await _ppServices.Exists(userId, portfolioId) || portfolio.UserId == userId)
            {
                return StatusCode(200, orders);
            }
            else
            {
                orders = orders.OrderBy(x => x.Timestamp).ToList();
                orders = orders.Take(3).ToList();
                return StatusCode(200,orders);
            }
        }

        [HttpGet("Comment")]
        public async Task<IActionResult> GetComments(int orderId)
        {
            if (await _orderServices.GetOrderById(orderId) == null)
            {
                return StatusCode(400, "Portfolio with id " + orderId + " was not found.");
            }
            var comments = await _commentServices.GetAllOrderCommentByOrderId(orderId);
            return StatusCode(200, comments);
        }

        [HttpGet("getNotInPortfolio")]
        public async Task<IActionResult> GetOrdertsNotInPortfolio(int portfolioId)
        {
            int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var orderList = await _orderServices.GetOrders(userId, 0, 50, null, null);
            var portfolioOrderList = await _orderServices.GetOrders(userId, portfolioId, 199, null, null);

            for (int i = 0; i<orderList.Count(); i++)
            {
                var order = orderList[i];
                foreach(OrderDTO pOrderd in portfolioOrderList)
                {
                    if(order.OrderId == pOrderd.OrderId)
                    {
                        orderList.Remove(order);
                    }
                }
            }

            return StatusCode(200, orderList);
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> RefreshOrders()
        {
            try
            {
                int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var orderlist = await _orderServices.RefreshAllOrders(userId);
                if (orderlist.Count == 0)
                {
                    return StatusCode(200, "No new orders were found. Make sure your keys are valid.");
                }
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500, "Something went wrong while fetching orders. Make sure your API key and secret are valid.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder(OrderDTO orderToUpdate)
        {
            if(orderToUpdate.Description != null)
                orderToUpdate.Description = _htmlEncoder.Encode(_javaScriptEncoder.Encode(orderToUpdate.Description));

            var order = await _orderServices.UpdateOrder(orderToUpdate);

            return StatusCode(200, order);
        }
    }
}
