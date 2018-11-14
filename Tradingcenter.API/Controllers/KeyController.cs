using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trainingcenter.Domain.DTOs.ExchangeKeyDTOs;
using Trainingcenter.Domain.Services.ExchangeKeyServices;

namespace Tradingcenter.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KeyController : ControllerBase
    {
        private readonly IExchangeKeyServices _keyService;

        public KeyController(IExchangeKeyServices keyService)
        {
            _keyService = keyService;
        }

        // GET: api/Key
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try {
                int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var keyList = await _keyService.GetExchangeKeys(userId);

                if (keyList == null)
                {
                    return StatusCode(204, "No keys were found");
                }
                return StatusCode(200, keyList);
            }
            catch
            {
                return StatusCode(500, "Something went wrong while attempting to get keys");
            }
        }

        // POST: api/Key
        [HttpPost]
        public async Task<IActionResult> Post(ExchangeKeyToCreateDTO key)
        {
            int userId = Int32.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _keyService.CreateExchangeKey(key, userId);

            return StatusCode(200,"All gud");
        }

        // PUT: api/Key/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
