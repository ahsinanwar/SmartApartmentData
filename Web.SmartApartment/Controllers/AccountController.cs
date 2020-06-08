using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.SmartApartment.Controllers
{
    /// <summary>
    /// This is Account Controller
    /// </summary>
    /// <remarks>This controller handle Login and Logout function and send asyn request to Auth0 domain for authentication</remarks>
    public class AccountController : Controller
    {
        /// <summary>
        /// This function perform Login  operation through Auth0
        /// </summary>
        /// <returns></returns>
        /// <remarks>This function call ChallengeAsync and pass "Auth0" as the authentication scheme. This will invoke the 
        /// OIDC(OpenID Connect) authentication handler which we have registered in the ConfigureServices method.
        /// </remarks>
        public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Claims()
        {
            var test = HttpContext.User;
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return View();
        }
        /// <summary>
		/// This function will perform logout operation through Auth0
		/// </summary>
		/// <returns>It returns its response to view</returns>
		/// <remarks>This function call SignOutAsy and pass Auth0 as authentication scheme and Indicate where Auth0 should 
        /// redirect the user after a logout.</remarks>
        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index", "Home")
            });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}