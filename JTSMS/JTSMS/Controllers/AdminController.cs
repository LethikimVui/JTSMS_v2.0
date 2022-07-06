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
using SharedObjects.Extensions;

namespace JTSMS.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        private readonly ICommonService commonService;

        public AdminController(IAdminService adminService, ICommonService commonService)
        {
            this.adminService = adminService;
            this.commonService = commonService;
        }
        public string GetDisplayNameFromSamAccountName(string samAccountName)
        {
            using (var principalContext = new PrincipalContext(ContextType.Domain))
            {
                try
                {
                    var userPrincipal = UserPrincipal.FindByIdentity(principalContext, samAccountName);
                    if (userPrincipal != null)
                    {
                        return userPrincipal.DisplayName;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    return null;
                }

            }
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
       
        public async Task<IActionResult> Search()
        {
            var roles = await adminService.Access_Role_get();
            ViewData["Customers"] = await commonService.Customer_Get();
            ViewData["roles"] = roles;
            return View();
        }
        public async Task<IActionResult> User_Get([FromBody] UserRoleViewModel model)
        {
            var userRoles = await adminService.Access_UserRole_get(model);
            var roles = await adminService.Access_Role_get();
            ViewData["Customers"] = await commonService.Customer_Get();

            ViewData["roles"] = roles;
            return PartialView(userRoles);
        }
        public async Task<IActionResult> Access_UserRole_insert([FromBody] UserRoleViewModel model)
        {
            model.UserName = DisplayName(model.Ntlogin);
            model.UserEmail = EmailAddress(model.Ntlogin);
            var result = await adminService.Access_UserRole_insert(model);
            return Json(new { results = result });
        }
        public async Task<IActionResult> Access_UserRole_update([FromBody] UserRoleViewModel model)
        {
            var result = await adminService.Access_UserRole_update(model);
            return Json(new { results = result });
        }
        public async Task<IActionResult> Access_UserRole_delete([FromBody] UserRoleViewModel model)
        {
            var result = await adminService.Access_UserRole_delete(model);
            return Json(new { results = result });
        }
        public async Task<IActionResult> Access_UserRole_Get_By_Id(int id)
        {
            var result = await adminService.Access_UserRole_Get_By_Id(id);
            return Json(new { results = result });
        }


        public async Task<IActionResult> Approval()
        {
            var Routes = await commonService.Master_Route_get();
            ViewData["Customers"] = await commonService.Customer_Get();
            ViewData["Routes"] = Routes;
            return View(Routes);
        }
        public async Task<IActionResult> Master_Approval_get_by_routeId(int routeId)
        {
            ViewData["Customers"] = await commonService.Customer_Get();
            var Approval_get_by_routeId = await adminService.Master_Approval_get_by_routeId(routeId);
            return PartialView(Approval_get_by_routeId);
        }
        public async Task<IActionResult> Master_Approval_insert([FromBody] UserRoleViewModel model)
        {
            model.UserEmail = EmailAddress(model.Ntlogin);
            model.UserName = DisplayName(model.Ntlogin);
            var result = await adminService.Master_Approval_insert(model);
            return Json(new { results = result });
        }
        public async Task<IActionResult> Master_Approval_delete([FromBody] UserRoleViewModel model)
        {
            var result = await adminService.Master_Approval_delete(model);
            return Json(new { results = result });
        }
    }
}
