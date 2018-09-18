using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace MvcAuthSample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("home");
        }

        public async Task<IActionResult> TestLogin()
        {
            var identity =  new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, "jesse"),
                new Claim(ClaimTypes.Role, "admin"),
            },CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return Ok();
        }
    }
}