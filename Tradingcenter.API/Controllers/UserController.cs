using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Trainingcenter.Domain.DTOs.UserDTOs;
using Trainingcenter.Domain.Services;

//DONE
namespace Tradingcenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region DependecyInjection

        private readonly IUserServices _userService;
        public IConfiguration _config;

       public UserController(IUserServices userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        #endregion

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserToLoginDTO userToLogin)
        {
            try
            {
                var user = await _userService.Login(userToLogin);

                if (user == null)
                {
                    //Unauthorized
                    return StatusCode(401, "Bad login");
                }
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                    //More claims here
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("Appsettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenhandler = new JwtSecurityTokenHandler();

                var token = tokenhandler.CreateToken(tokenDescriptor);

                //Ok
                return StatusCode(200, new { token = tokenhandler.WriteToken(token) });
            }
            catch
            {
                return StatusCode(500, "Something went wrong while attempting to log in, please try again in a few moments.");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserToRegisterDTO userToRegister)
        {
            try
            {
                var user = await _userService.Register(userToRegister);

                if (await _userService.UserExists(userToRegister.Username))
                {
                    return StatusCode(201, "Username already taken");
                }
                if (_userService.IsValidEmail(userToRegister.Email))
                {
                    return StatusCode(201, "Invalid email");
                }
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500, "Something went wrong while attempting to register, please try again in a few moments.");
            }
        }
    }
}