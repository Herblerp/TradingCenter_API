﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainingcenter.Domain.DTOs.OrderDTO_s;
using Trainingcenter.Domain.DTOs.PortfolioDTO_s;
using Trainingcenter.Domain.DTOs.PortfolioOrderDTOs;
using Trainingcenter.Domain.Services.CommentServices;
using Trainingcenter.Domain.Services.OrderServices;
using Trainingcenter.Domain.Services.PortfolioServices;

namespace Tradingcenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly ICommentServices _commentServices;
        private readonly IPortfolioServices _portfolioServices;
        private readonly IOrderServices _orderServices;

        public PortfolioController(IPortfolioServices portfolioServices, IOrderServices orderServices, ICommentServices commentServices)
        {
            _portfolioServices = portfolioServices;
            _orderServices = orderServices;
            _commentServices = commentServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int portfolioId)
        {
            try
            {
                int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (portfolioId == 0)
                {
                    return StatusCode(200, await _portfolioServices.GetAllPortfolioByUserIdAsync(userId));
                }
                var portfolio = await _portfolioServices.GetPortfolioByIdAsync(portfolioId);

                if (portfolio.UserId == userId)
                {
                    return StatusCode(200, portfolio);
                }
                return StatusCode(401);
            }
            catch
            {
                return StatusCode(500, "Something went wrong while attempting to get portfolios");
            }
        }

        [HttpGet("profit")]
        public async Task<IActionResult> GetProfit(int portfolioId)
        {
            try
            {
                int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var ppdList = new List<ProfitPerDayDTO>();

                if (portfolioId == 0)
                {
                    ppdList = await _orderServices.GetProfitPerDayFromUser(userId);

                    if (ppdList != null)
                    {
                        return StatusCode(200, ppdList);
                    }
                    return StatusCode(400);
                }
                ppdList = await _orderServices.GetProfitPerDayFromPortfolio(userId, portfolioId);

                if (ppdList != null)
                {
                    return StatusCode(200, ppdList);
                }
                return StatusCode(400);
            }
            catch
            {
                return StatusCode(500, "Something went wrong while attempting to get profit per day.");
            }
        }

        [HttpGet("Comment")]
        public async Task<IActionResult> GetComments(int portfolioId)
        {
            if(await _portfolioServices.GetPortfolioByIdAsync(portfolioId) == null)
            {
                return StatusCode(400, "Portfolio with id " + portfolioId + " was not found.");
            }
            var comments = await _commentServices.GetAllCommentByPortfolioId(portfolioId);
            return StatusCode(200, comments);
        }

        [HttpPut]
        public async Task<IActionResult> Put(PortfolioToCreateDTO portfolio)
        {
            try
            {
                int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _portfolioServices.CreatePortfolioAsync(portfolio, userId, false);

                return StatusCode(201);
            }
            catch
            {
                return StatusCode(500, "Something went wrong while attempting to create a new portfolio");
            }
        }

        [HttpPut("order")]
        public async Task<IActionResult> PutOrder(PortfolioOrderDTO po)
        {
            try
            {
                int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var portfolio = await _portfolioServices.GetPortfolioByIdAsync(po.PortfolioId);
                var order = await _orderServices.GetOrderById(po.OrderId);

                if (portfolio == null)
                {
                    return StatusCode(400, "Portfolio with Id " + po.PortfolioId + " was not found.");
                }
                if (order == null)
                {
                    return StatusCode(400, "Order with Id " + po.OrderId + " was not found.");
                }
                if (portfolio.UserId != userId)
                {
                    return StatusCode(401);
                }
                if (order.UserId != userId)
                {
                    return StatusCode(401);
                }
                if (await _portfolioServices.PortfolioOrderExists(po.OrderId, po.PortfolioId))
                {
                    return StatusCode(400, "Portfolio with id " + po.PortfolioId + " already contains order with id " + po.OrderId);
                }
                await _portfolioServices.AddOrderById(po);
                return StatusCode(201);
            }
            catch
            {
                return StatusCode(500, "Something went wrong while attempting to add order with id " + po.OrderId + " to portfolio with id " + po.PortfolioId);
            }
        }

        [HttpDelete("order")]
        public async Task<IActionResult> DeleteOrder(int orderId, int portfolioId)
        {
            int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var order = await _orderServices.GetOrderById(orderId);
            var portfolio = await _portfolioServices.GetPortfolioByIdAsync(portfolioId);

            if(order == null)
            {
                return StatusCode(400, "Order with id " + orderId + " was not found.");
            }
            if (portfolio == null)
            {
                return StatusCode(400, "Portfolio with id " + portfolioId + " was not found.");
            }
            if (order.UserId != userId || portfolio.UserId != userId)
            {
                return StatusCode(401);
            }
            if (!await _portfolioServices.PortfolioOrderExists(orderId, portfolioId))
            {
                return StatusCode(400, "Portfolio with Id " + portfolioId + " does not contain order with id " + orderId);
            }
            await _portfolioServices.RemoveOrderById(orderId, portfolioId);
            return StatusCode(200);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PortfolioDTO portfolioToUpdate)
        {
            try
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
            catch
            {
                return StatusCode(500, "Something went wrong while attempting to update portfolio with id " + portfolioToUpdate.PortfolioId);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int portfolioId)
        {
            try
            {
                int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var portfolio = await _portfolioServices.GetPortfolioByIdAsync(portfolioId);

                if (portfolio.UserId == userId && portfolio.IsDefault == false)
                {
                    await _portfolioServices.DeletePortfolioAsync(portfolioId);
                    return StatusCode(200);
                }
                return StatusCode(401);
            }
            catch
            {
                return StatusCode(500, "Something went wrong while attempting to delete portfolio with id " + portfolioId);
            }
        }

    }
}