using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace JwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSetting _jwtSetting;
        public AuthController(IOptions<JwtSetting> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _jwtSetting = options.Value;
        }

        [HttpPost]
        public IActionResult GetToken()
        {
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.secretKey)), SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,"jesse"),
                new Claim(ClaimTypes.Role,"admin")
            };
            var token = new JwtSecurityToken(_jwtSetting.issuer, _jwtSetting.audience, claims, DateTime.Now, DateTime.Now.AddMinutes(10), signingCredentials);
            var str = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = str });
        }
    }
}