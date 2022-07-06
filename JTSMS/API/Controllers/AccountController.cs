using API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedObjects.Commons;
using SharedObjects.StoredProcedures;
using SharedObjects.ValueObjects;
using SharedObjects.ViewModels;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public AccountController(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        [HttpPost("login")]
        [Obsolete]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await context.Query<VUserRole>().AsNoTracking().FromSql(SPAccount.UserRole_get, model.NTLogin).ToListAsync();
            List<Claim> informationClaim = new List<Claim>();

            if (user.Count() > 0)
            {
                foreach (var item in user)
                {
                    informationClaim.Add(new Claim(ClaimTypes.Role, item.RoleName));
                    informationClaim.Add(new Claim("CustId", user[0].CustId.ToString()));


                }
                informationClaim.Add(new Claim("Ntlogin", model.NTLogin));
                informationClaim.Add(new Claim("UserName", user[0].UserName));
                informationClaim.Add(new Claim("UserEmail", user[0].UserEmail));
            }
            else
            {
                informationClaim.Add(new Claim("Ntlogin", model.NTLogin));
                informationClaim.Add(new Claim("UserName", DisplayName(model.NTLogin)));
                informationClaim.Add(new Claim("UserEmail", EmailAddress(model.NTLogin)));
            }


            ClaimsIdentity claimsIdentity = new ClaimsIdentity(informationClaim, CookieAuthenticationDefaults.AuthenticationScheme);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claimsIdentity.Claims,
                expires: DateTime.UtcNow.AddMinutes(2),
                signingCredentials: sign
                );
            string strToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new ResponseResult(200, strToken));
        }


        static string EmailAddress(string userName)
        {
            var EmailAddress = "";
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                var userPrincipal = UserPrincipal.FindByIdentity(context, userName);
                EmailAddress = userPrincipal.EmailAddress;

            }
            return EmailAddress;
        }
        static string DisplayName(string userName)
        {
            var DisplayName = "";
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                var userPrincipal = UserPrincipal.FindByIdentity(context, userName);
                DisplayName = userPrincipal.DisplayName;

            }
            return DisplayName;
        }
    }
}
