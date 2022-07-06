using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using SharedObjects.ValueObjects;
using SharedObjects.ViewModels;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JTSMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IConfiguration configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            this.accountService = accountService;
            this.configuration = configuration;
        }
        bool IsValidCredential(string userName, string password)
        {
            bool valid = false;
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                valid = context.ValidateCredentials(userName, password, ContextOptions.Negotiate);
                //var userPrincipal = UserPrincipal.FindByIdentity(context, userName);
                //var t = userPrincipal.EmailAddress;

            }
            return valid;
        }

        ClaimsPrincipal ValidateToken(string token)
        {
            IdentityModelEventSource.ShowPII = true;
            SecurityToken validateToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.ValidateLifetime = true;
            validationParameters.ValidAudience = configuration["Jwt:Audience"];
            validationParameters.ValidIssuer = configuration["Jwt:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out validateToken);

            List<Claim> claim = new List<Claim> { new Claim("token", token) };
            ClaimsIdentity claimsIentity = new ClaimsIdentity(claim);

            principal.AddIdentity(claimsIentity);
            return principal;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            bool skipCredential = bool.Parse(configuration["SkipCredential"]);
            bool validCredential = IsValidCredential(model.NTLogin, model.Password);
            if (skipCredential)
            {
                validCredential = true;
            }
            if (validCredential)
            {
                var responseResult = await accountService.Login(model);

                var userPrincipal = this.ValidateToken(responseResult.Message);
                var authenticationProperty = new AuthenticationProperties()
                {
                    IsPersistent = false
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authenticationProperty);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                    return RedirectToAction("search", "request");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ntlogin or Password is incorrect!");
                return View(model);
            }
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
