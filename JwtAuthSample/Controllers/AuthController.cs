using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;

namespace JwtAuthSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Models.JwtSetting _jwtSetting;

        public AuthController(IOptions<Models.JwtSetting> jwtSetting)
        {
            _jwtSetting = jwtSetting.Value;
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if(model.UserName == "jesse" && model.Password == "123123")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,"jesse"),
                    new Claim(ClaimTypes.Role,"admin")
                };
                var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey)), SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(issuer: _jwtSetting.Issuer, audience: _jwtSetting.Audience, claims: claims, notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(10), signingCredentials: signingCredentials);
                var str = tokenHandler.WriteToken(token);
                return Ok(new { token = str });
            }
            return BadRequest();
        }
    }
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
