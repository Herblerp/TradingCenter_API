using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainingcenter.Domain.DTOs.PortfolioDTO_s;
using Trainingcenter.Domain.DTOs.PortfolioOrderDTOs;
using Trainingcenter.Domain.Services.OrderServices;
using Trainingcenter.Domain.Services.PortfolioServices;

namespace Tradingcenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioServices _portfolioServices;
        private readonly IOrderServices _orderServices;

        public PortfolioController(IPortfolioServices portfolioServices, IOrderServices orderServices)
        {
            _portfolioServices = portfolioServices;
            _orderServices = orderServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            return StatusCode(200, await _portfolioServices.GetAllPortfolioByUserIdAsync(userId));
        }

        [HttpPut]
        public async Task<IActionResult> Put(PortfolioToCreateDTO portfolio)
        {
            int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _portfolioServices.CreatePortfolioAsync(portfolio, userId);

            return StatusCode(201);

        }

        [HttpPut("order")]
        public async Task<IActionResult> PutOrder(PortfolioOrderDTO po)
        {
            int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var portfolio = await _portfolioServices.GetPortfolioByIdAsync(po.PortfolioId);
            var order = await _orderServices.GetOrderById(po.OrderId);

            if(portfolio == null)
            {
                return StatusCode(400, "Portfolio with Id " + po.PortfolioId + " was not found.");
            }
            if(order == null)
            {
                return StatusCode(400, "Order with Id " + po.OrderId + " was not found.");
            }
            if (portfolio.UserId != userId)
            {
                return StatusCode(401);
            }
            if(order.UserId != userId)
            {
                return StatusCode(401);
            }
            if (!await _portfolioServices.IsOrderInPortfolio(po.OrderId, po.PortfolioId))
            {
                await _portfolioServices.AddOrderById(po);
                return StatusCode(201);
            }
            return StatusCode(400);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PortfolioDTO portfolioToUpdate)
        {
            int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var portfolio = await _portfolioServices.GetPortfolioByIdAsync(portfolioToUpdate.PortfolioId);

            if (portfolio.UserId == userId)
            {
                await _portfolioServices.UpdatePortfolioAsync(portfolioToUpdate);
                return StatusCode(200);
            }
            return StatusCode(401);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int portfolioId)
        {
            int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var portfolio = await _portfolioServices.GetPortfolioByIdAsync(portfolioId);

            if (portfolio.UserId == userId)
            {
                await _portfolioServices.DeletePortfolioAsync(portfolioId);
                return StatusCode(200);
            }
            return StatusCode(401);
        }

    }
}