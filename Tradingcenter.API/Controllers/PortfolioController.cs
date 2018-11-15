using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainingcenter.Domain.Services.PortfolioServices;

namespace Tradingcenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioServices _portfolioServices;

        public PortfolioController(IPortfolioServices portfolioServices)
        {
            _portfolioServices = portfolioServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _portfolioServices.
        }

    }
}