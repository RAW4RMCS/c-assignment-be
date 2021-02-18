using AccountApi.Authentication;
using AccountApi.Authentication.Dto;
using AccountApi.Dtos;
using AccountApi.Logics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        ///  Login
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AccountDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("login", Name = "login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var user = await userManager.FindByNameAsync(login.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, login.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim("userId", user.Id),
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                var response = new JwtResponseDto()
                {
                    Payload = new JwtSecurityTokenHandler().WriteToken(token)
                };

                return Ok(response);
            }
            return Unauthorized();
        }

        /// <summary>
        ///  Register a new user
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            var userExists = await userManager.FindByNameAsync(register.Username);

            if (userExists != null)
                return BadRequest("Something went wrong!"); // User already exists

            var user = ApplicationUsersLogic.MapRegisterToApplicationUser(register);
            var result = await userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
                return BadRequest("Something went wrong!");

            return Ok();
        }
    }
}
