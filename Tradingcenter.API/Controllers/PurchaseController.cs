using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainingcenter.Domain.DTOs.PurchasedPortfolioDTOs;
using Trainingcenter.Domain.Repositories;
using Trainingcenter.Domain.Services.PurchasedPortfolioServices;

namespace Tradingcenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchasedPortfolioServices _ppServices;

        public PurchaseController(IPurchasedPortfolioServices ppServices)
        {
            _ppServices = ppServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var purchases = await _ppServices.GetPurchasedPortfoliosById(userId);

            return StatusCode(200, purchases);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PurchasedPortfolioDTO ppDTO)
        {
            if(await _ppServices.Exists(ppDTO.UserId, ppDTO.PortfolioId))
            {
                return StatusCode(400, "Entry already exists");
            }

            if(ppDTO.PortfolioId == 0 || ppDTO.UserId == 0)
            {
                return StatusCode(400, "portfolioId and userId can not be 0");
            }
            var createdPurchase = await _ppServices.AddPurchasedPortfolio(ppDTO);

            return StatusCode(200);
        }
    }
}