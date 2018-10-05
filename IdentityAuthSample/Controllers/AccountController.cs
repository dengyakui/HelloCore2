using IdentityAuthSample.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace IdentityAuthSample.Controllers
{
    public class AccountController : Controller
    {
        private readonly TestUserStore _testUserStore;

        public AccountController(TestUserStore testUserStore)
        {
            _testUserStore = testUserStore;
        }
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        public IActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return Redirect("/home/index");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel Input, string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            ViewBag.ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var user = _testUserStore.FindByUsername(Input.UserName);
                if (user == null)
                {
                    ModelState.AddModelError(nameof(Input.UserName), "user name not exists");
                }
                else
                {
                    if (!_testUserStore.ValidateCredentials(Input.UserName, Input.Password))
                    {
                        ModelState.AddModelError(nameof(Input.Password), "invalid password");
                    }
                    else
                    {
                        var props = new AuthenticationProperties()
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                        };
                        await HttpContext.SignInAsync(user.SubjectId, user.Username, props);
                        return RedirectToLocal(returnUrl);
                    }

                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                //var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                //if (result.Succeeded)
                //{
                //    _logger.LogInformation("User logged in.");
                //    return LocalRedirect(returnUrl);
                //}
                //if (result.RequiresTwoFactor)
                //{
                //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                //}
                //if (result.IsLockedOut)
                //{
                //    _logger.LogWarning("User account locked out.");
                //    return RedirectToPage("./Lockout");
                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                //    return Page();
                //}
            }
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}