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
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public AdminController(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        [HttpGet("Access_Role_get")]
        [Obsolete]
        public async Task<List<VRole>> Access_Role_get()
        {
            var results = await context.Query<VRole>().AsNoTracking().FromSql(SPAdmin.Access_Role_get).ToListAsync();
            return results;
        }
        
        [HttpPost("Access_UserRole_get")]
        [Obsolete]
        public async Task<List<VUserRole>> Access_UserRole_get(UserRoleViewModel model)
        {
            var results = await context.Query<VUserRole>().AsNoTracking().FromSql(SPAdmin.Access_UserRole_get, model.RoleId, model.CustId, model.Ntlogin).ToListAsync();
            return results;
        }
        [HttpGet("Access_UserRole_Get_By_Id/{id}")]
        [Obsolete]
        public async Task<List<VUserRole>> Access_UserRole_Get_By_Id(int id)
        {
            var results = await context.Query<VUserRole>().AsNoTracking().FromSql(SPAdmin.Access_UserRole_Get_By_Id, id).ToListAsync();
            return results;
        }

        [HttpPost("Access_UserRole_insert")]
        [Obsolete]
        public async Task<IActionResult> Access_UserRole_insert(UserRoleViewModel model)
        {
            try
            {
                if (!context.AccessUserRole.Where(x => (x.Ntlogin == model.Ntlogin) && (x.CustId == model.CustId) && (x.IsActive == 1)).ToList().Any())
                {
                    await context.Database.ExecuteSqlCommandAsync(SPAdmin.Access_UserRole_insert, model.Ntlogin, model.UserName, model.UserEmail, model.RoleId, model.PlantId, model.CustId, model.CreatedBy, model.CreatedName, model.CreatedEmail);
                    return Ok(new ResponseResult(200, "User Added Successfully"));
                }
                else
                {
                    return Conflict(new ResponseResult(400, "User already existing"));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult(400, ex.Message));
            }
        }

        [HttpPost("Access_UserRole_delete")]
        [Obsolete]
        public async Task<IActionResult> Access_UserRole_delete(UserRoleViewModel model)
        {
            try
            {
                await context.Database.ExecuteSqlCommandAsync(SPAdmin.Access_UserRole_delete, model.UserRoleId, model.UpdatedBy, model.UpdatedName, model.UpdatedEmail);
                return Ok(new ResponseResult(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult(400, ex.Message));
            }
        }
        [HttpPost("Access_UserRole_update")]
        [Obsolete]
        public async Task<IActionResult> Access_UserRole_update(UserRoleViewModel model)
        {
            try
            {
                await context.Database.ExecuteSqlCommandAsync(SPAdmin.Access_UserRole_update, model.UserRoleId, model.PlantId, model.CustId, model.RoleId, model.UpdatedBy, model.UpdatedName, model.UpdatedEmail);
                return Ok(new ResponseResult(200, "User Updated Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult(400, ex.Message));
            }
        }



        [HttpGet("Master_Approval_get_by_routeId/{routeId}")]
        [Obsolete]
        public async Task<List<VMasterApproval>> Master_Approval_get_by_routeId(int routeId)
        {
            var results = await context.Query<VMasterApproval>().AsNoTracking().FromSql(SPAdmin.Master_Approval_get_by_routeId, routeId).ToListAsync();
            return results;
        }
        [HttpPost("Master_Approval_insert")]
        [Obsolete]
        public async Task<IActionResult> Master_Approval_insert(UserRoleViewModel model)
        {
            try
            {
                if (!context.MasterApproval.Where(x => (x.Ntlogin == model.Ntlogin) && (x.CustId == model.CustId) && x.RouteId ==model.RouteId && (x.IsActive == 1)).ToList().Any())
                {
                    await context.Database.ExecuteSqlCommandAsync(SPAdmin.Master_Approval_insert, model.CustId, model.RouteId, model.Ntlogin, model.UserName, model.UserEmail, model.CreatedBy);
                    return Ok(new ResponseResult(200, "User Added Successfully"));
                }
                else
                {
                    return Conflict(new ResponseResult(400, "User already existing"));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult(400, ex.Message));
            }
        }

        [HttpPost("Master_Approval_delete")]
        [Obsolete]
        public async Task<IActionResult> Master_Approval_delete(UserRoleViewModel model)
        {
            try
            {
                await context.Database.ExecuteSqlCommandAsync(SPAdmin.Master_Approval_delete, model.ApprovalId, model.UpdatedBy);
                return Ok(new ResponseResult(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult(400, ex.Message));
            }

        }
    }
}
