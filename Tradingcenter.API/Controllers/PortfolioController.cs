using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainingcenter.Domain.DTOs.OrderDTO_s;
using Trainingcenter.Domain.DTOs.PortfolioDTO_s;
using Trainingcenter.Domain.DTOs.PortfolioOrderDTOs;
using Trainingcenter.Domain.Services.CommentServices;
using Trainingcenter.Domain.Services.OrderServices;
using Trainingcenter.Domain.Services.PortfolioServices;
using Trainingcenter.Domain.Services.PurchasedPortfolioServices;

namespace Tradingcenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PortfolioController : ControllerBase
    {
        private readonly ICommentServices _commentServices;
        private readonly IPortfolioServices _portfolioServices;
        private readonly IOrderServices _orderServices;
        private readonly IPurchasedPortfolioServices _ppServices;
        private HtmlEncoder _htmlEncoder;
        private JavaScriptEncoder _javaScriptEncoder;

        public PortfolioController( IPortfolioServices portfolioServices, 
                                    IOrderServices orderServices, 
                                    ICommentServices commentServices,
                                    IPurchasedPortfolioServices ppServices,
                                    HtmlEncoder htmlEncoder,
                                    JavaScriptEncoder javascriptEncoder)
        {
            _portfolioServices = portfolioServices;
            _orderServices = orderServices;
            _commentServices = commentServices;
            _htmlEncoder = htmlEncoder;
            _javaScriptEncoder = javascriptEncoder;
            _ppServices = ppServices;
        }

        [HttpGet("forsale")]
        public async Task<IActionResult> GetForSale()
        {
            var userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var list = await _portfolioServices.GetAllForSalePortfolios(userId);

            return StatusCode(200,list);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int userId, int portfolioId, bool soldOnly)
        {
            try
            {
                bool isCurrentUser = true;

                if (userId == 0)
                {
                    userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                }
                else
                {
                    isCurrentUser = false;
                }

                if (portfolioId == 0)
                {
                    var portfolios = await _portfolioServices.GetAllPortfolioByUserIdAsync(userId);

                    if (soldOnly == true || isCurrentUser == false)
                    {
                        var soldPortfolios = new List<PortfolioDTO>();

                        foreach(PortfolioDTO p in portfolios)
                        {
                            if(p.IsForSale == true)
                            {
                                soldPortfolios.Add(p);
                            }
                        }
                        return StatusCode(200, soldPortfolios);
                    }

                    return StatusCode(200, portfolios);
                }

                var portfolio = await _portfolioServices.GetPortfolioByIdAsync(portfolioId);

                if (portfolio.UserId == userId || portfolio.IsForSale == true)
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

        [HttpGet("sold")]
        public async Task<IActionResult> GetSoldPerMonth(int portfolioId)
        {
            var userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var portfolio = await _portfolioServices.GetPortfolioByIdAsync(portfolioId);

            if(portfolio == null)
            {
                return StatusCode(400, "Portfolio with id " + portfolioId + " was not found.");
            }
            if(portfolio.UserId != userId)
            {
                return StatusCode(401);
            }
            return StatusCode(200, await _ppServices.GetSoldPerMonth(portfolioId));

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
            var comments = await _commentServices.GetAllPortfolioCommentByPortfolioId(portfolioId);
            return StatusCode(200, comments);
        }

        [HttpPut]
        public async Task<IActionResult> Put(PortfolioToCreateDTO portfolio)
        {
            try
            {
                portfolio.Description = _htmlEncoder.Encode(_javaScriptEncoder.Encode(portfolio.Description));
                portfolio.Goal = _htmlEncoder.Encode(_javaScriptEncoder.Encode(portfolio.Goal));
                portfolio.ImgURL = _javaScriptEncoder.Encode(portfolio.ImgURL);
                portfolio.Name = _htmlEncoder.Encode(_javaScriptEncoder.Encode(portfolio.Name));

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
                return StatusCode(401, "You can not delete orders from the default portfolio");
            }
            if (!await _portfolioServices.PortfolioOrderExists(orderId, portfolioId))
            {
                return StatusCode(400, "Portfolio with Id " + portfolioId + " does not contain order with id " + orderId);
            }
            if (portfolio.IsDefault)
            {
                return StatusCode(401, "You can not delete orders from the default portfolio.");
            }
            await _portfolioServices.RemoveOrderById(orderId, portfolioId);
            return StatusCode(200);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PortfolioDTO portfolioToUpdate)
        {
            try
            {
                if(portfolioToUpdate.Description != null)
                    portfolioToUpdate.Description = _htmlEncoder.Encode(_javaScriptEncoder.Encode(portfolioToUpdate.Description));
                if(portfolioToUpdate.Goal != null)
                    portfolioToUpdate.Goal = _htmlEncoder.Encode(_javaScriptEncoder.Encode(portfolioToUpdate.Goal));
                if(portfolioToUpdate.ImgURL != null)
                    portfolioToUpdate.ImgURL = _javaScriptEncoder.Encode(portfolioToUpdate.ImgURL);
                if(portfolioToUpdate.Name != null)
                    portfolioToUpdate.Name = _htmlEncoder.Encode(_javaScriptEncoder.Encode(portfolioToUpdate.Name));

                int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var portfolio = await _portfolioServices.GetPortfolioByIdAsync(portfolioToUpdate.PortfolioId);

                if(portfolio == null)
                {
                    return StatusCode(400, "Portfolio with id " + portfolioToUpdate.PortfolioId + " was not found.");
                }
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