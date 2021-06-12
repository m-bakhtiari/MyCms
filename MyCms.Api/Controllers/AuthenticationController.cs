using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MyCms.Core.Extensions;
using MyCms.Core.Interfaces;
using MyCms.Core.Services;
using MyCms.Core.ViewModels;
using MyCms.Extensions.Consts;

namespace MyCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginViewModel user)
        {
            if (user.Email.IsNullOrWhiteSpace() || user.Password.IsNullOrWhiteSpace())
            {
                return BadRequest("The Model Is Not Valid");
            }

            var login = await _userService.LoginUser(user);
            if (login == null)
            {
                return Unauthorized();
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.VerifyCodeJwt));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOption = new JwtSecurityToken(
                issuer: Const.SiteUrl,
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name,login.Email),
                    new Claim(ClaimTypes.NameIdentifier,login.UserId.ToString()),
                    new Claim(ClaimTypes.Role,login.UserRoles.FirstOrDefault().Role.Title)
                },
                expires: DateTime.Now.AddDays(7),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);

            return Ok(new AccessToken() { Token = tokenString });
        }


    }

    public class AccessToken
    {
        public string Token { get; set; }
    }
}
