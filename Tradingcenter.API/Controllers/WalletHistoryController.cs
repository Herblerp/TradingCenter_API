using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainingcenter.Domain.DTOs.WalletDTOs;
using Trainingcenter.Domain.Services.WalletServices;

namespace Tradingcenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalletHistoryController : ControllerBase
    {
        private readonly IWalletServices _walletServices;
        public WalletHistoryController (IWalletServices walletServices)
        {
            _walletServices = walletServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int keyId)
        {
            int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (keyId == 0)
            {
                return StatusCode(200, await _walletServices.GetTransactionsFromUserId(userId));
            }
            return StatusCode(200,await _walletServices.GetTransactionsFromKeyAsync(keyId));
        }
        [HttpGet("Refresh")]
        public async Task<IActionResult> Refresh()
        {
            int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _walletServices.GetBitmexTransactions(userId);

            return StatusCode(200);
        }
    }
}