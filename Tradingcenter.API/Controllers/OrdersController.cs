using System;
using System.Collections.Generic;
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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        private readonly IPortfolioServices _portfolioServices;

        public OrdersController(IOrderServices orderService, IPortfolioServices portfolioServices)
        {
            _orderServices = orderService;
            _portfolioServices = portfolioServices;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetOrders(int portfolioId, int amount, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                    int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                    var orderList = await _orderServices.GetOrders(userId, portfolioId, amount, dateFrom, dateTo);

                    return StatusCode(200, orderList);
            }
            catch
            {
                return StatusCode(500, "Something went wrong while attempting to get orders");
            }
        }
    }
}