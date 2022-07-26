using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedObjects.Commons;
using SharedObjects.StoredProcedures;
using SharedObjects.ValueObjects;
using SharedObjects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public RegistrationController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpPost("Registration_get")]
        [Obsolete]
        public async Task<List<VRegistration>> Get(RegistrationViewModel model)
        {
            var results = await context.Query<VRegistration>().AsNoTracking().FromSql(SPRegistration.Registration_get, model.CustId, model.StationId, model.PlatformId, model.TypeId, model.ScriptId).ToListAsync();
            return results;
        }
        [HttpGet("Registration_get_by_id/{id}")]
        [Obsolete]
        public async Task<VRequest> Registration_get_by_id(int id)
        {
            var results = await context.Query<VRequest>().AsNoTracking().FromSql(SPRegistration.Registration_get_by_id, id).ToListAsync();
            return results[0];
        }
        [HttpPost("Registration_add")]
        [Obsolete]
        public async Task<IActionResult> Registration_add(RegistrationViewModel model)
        {
            var registrationList = context.Registration.Where(s => s.CustId == model.CustId && s.StationId == model.StationId && s.RouteStep == model.RouteStep && s.PlatformId == model.PlatformId);
            bool isPending = registrationList.Where(s => s.TypeId == model.TypeId && s.StatusId == 1).Any();
            if (model.TypeId == 1) // new
            {
                if (!isPending)
                {
                    await context.Database.ExecuteSqlCommandAsync(SPRegistration.Registration_add, model.CustId, model.StationId, model.RouteStep, model.TypeId, model.PlatformId, model.Family, model.Description, model.CreatedBy, model.CreatedName, model.CreatedEmail);
                    return Ok(new ResponseResult(200, "NEW"));
                }
                else
                {
                    return BadRequest(new ResponseResult(400, "NEW request is pending submission"));
                }
            }
            else
            {
                bool isReady = registrationList.Where(s => (s.TypeId == 1 || s.TypeId == 3) & s.IsReady).Any();
                bool isDev = registrationList.Where(s => s.TypeId == 2 & s.IsReady).Any();
                if (isReady && !isPending) //!isDev &&
                {
                    await context.Database.ExecuteSqlCommandAsync(SPRegistration.Registration_add, model.CustId, model.StationId, model.RouteStep, model.TypeId, model.PlatformId, model.Family, model.Description, model.CreatedBy, model.CreatedName, model.CreatedEmail);
                    return Ok(new ResponseResult(200, "Created"));
                }
                else if (isPending)
                {
                    return BadRequest(new ResponseResult(400, model.TypeId + " request is pending submission"));
                }
                else
                {
                    return BadRequest(new ResponseResult(400, "NEW / ECN/ ECO Request is not ready for a new request"));
                }
            }
        }
        [HttpGet("CheckAssy/{assy}")]
        public bool CheckAssy(string assy)
        {
            var assyList = context.Assembly.Where(s => s.AssemblyNumber == assy && s.IsActive == 1);
            bool isExisting = assyList.Any();
            if (!isExisting)
            {
                return true;
            }
            else
            {
                var t = from a in context.Assembly
                        join b in context.Registration on a.RegId equals b.RegId
                        select new
                        {
                            AssemblyNumber = a.AssemblyNumber,
                            StatusId = b.StatusId,
                            IsReady = b.IsReady,

                        };
                return false;
            }

        }
        [HttpPost("Registration_submit")]
        [Obsolete]
        public async Task<IActionResult> Registration_submit(RegistrationViewModel model)
        {
            try
            {
                var WorkflowRoute = context.NewWorkflowRoute.Where(s => s.TypeId == model.TypeId && s.IsActive ==1).ToList();
                if (WorkflowRoute.Any())
                {
                    await context.Database.ExecuteSqlCommandAsync(SPRegistration.Registration_submit, model.RegId, model.ScriptName, model.ScriptRev, model.PcnorDevNumber, model.ChangeDetail, model.FileHash, model.OriginalFileName, model.EncryptedFileName, model.Description, model.CreatedBy, model.CreatedName, model.CreatedEmail);
                    var arr_AssemblyNumber = model.AssemblyNumber.Split('|');
                    foreach (var AssemblyNumber in arr_AssemblyNumber)
                    {
                        await context.Database.ExecuteSqlCommandAsync(SPRegistration.Assembly_add, model.RegId, AssemblyNumber);
                    }
                    var arr_Attachment = model.Evidence.Split('|');

                    foreach (var item in arr_Attachment)
                    {
                        await context.Database.ExecuteSqlCommandAsync(SPRegistration.Attachment_add, model.RegId, item);
                    }
                    return Ok(new ResponseResult(200, "Submit successfully"));

                }
                else
                    return BadRequest(new ResponseResult(400, "Missing work flow route. Please contact administrator!"));
            }
            catch (Exception ex)
            {
                await context.Database.ExecuteSqlCommandAsync(SPRegistration.Assembly_delete, model.RegId);
                return BadRequest(new ResponseResult(400, ex.Message));
            }

        }
        [HttpPost("Registration_approve")]
        [Obsolete]
        public async Task<IActionResult> Registration_approve(RegistrationViewModel model)
        {
            try
            {
                await context.Database.ExecuteSqlCommandAsync(SPRegistration.Registration_approve, model.RegId, model.RouteId, model.Remark, model.CreatedBy, model.CreatedName, model.CreatedEmail);
                return Ok(new ResponseResult(200, "The ticket is approved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult(400, ex.Message));
            }

        }  
        [HttpPost("Registration_reject")]
        [Obsolete]
        public async Task<IActionResult> Registration_reject(RegistrationViewModel model)
        {
            try
            {
                await context.Database.ExecuteSqlCommandAsync(SPRegistration.Registration_reject, model.RegId, model.RouteId, model.Remark, model.CreatedBy, model.CreatedName, model.CreatedEmail);
                return Ok(new ResponseResult(200, "The ticket is rejected"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult(400, ex.Message));
            }

        }

        [HttpGet("Approval_get/{id}")]
        [Obsolete]
        public async Task<List<VApproval>> Approval_get(int id)
        {
            var results = await context.Query<VApproval>().AsNoTracking().FromSql(SPRegistration.Approval_get, id).ToListAsync();
            return results;
        }
    }
}
