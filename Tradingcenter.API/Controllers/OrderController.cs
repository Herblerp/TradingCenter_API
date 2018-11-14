using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainingcenter.Domain.DTOs.OrderDTO_s;
using Trainingcenter.Domain.Services.OrderServices;
using Trainingcenter.Domain.Services.PortfolioServices;

namespace Tradingcenter.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        private readonly IPortfolioServices _portfolioServices;

        public OrderController(IOrderServices orderService, IPortfolioServices portfolioServices)
        {
            _orderServices = orderService;
            _portfolioServices = portfolioServices;
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
        [HttpGet("refresh")]
        public async Task<IActionResult> RefreshOrders()
        {
            int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _orderServices.RefreshAllOrders(userId);
            return StatusCode(200);
        }
    }
}