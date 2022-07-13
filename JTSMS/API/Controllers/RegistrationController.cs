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
                //else if (isDev)
                //{
                //    return BadRequest(new ResponseResult(400, "Dev is applying for this request"));
                //}
                else
                {
                    return BadRequest(new ResponseResult(400, "NEW / ECN/ ECO Request is not ready for a new request"));
                }


                //if (model.TypeId == 3) // PCN / ECN / ECO
                //{
                //    if (isReady && !isPending) //!isDev &&
                //    {
                //        await context.Database.ExecuteSqlCommandAsync(SPRegistration.Registration_add, model.CustId, model.StationId, model.RouteStep, model.TypeId, model.PlatformId, model.Family, model.Description, model.CreatedBy, model.CreatedName, model.CreatedEmail);
                //        return Ok(new ResponseResult(200, "PCN / ECN / ECO"));
                //    }
                //    else if (isPending)
                //    {
                //        return BadRequest(new ResponseResult(400, "PCN/ECN/ECO request is pending submission"));

                //    }
                //    //else if (isDev)
                //    //{
                //    //    return BadRequest(new ResponseResult(400, "Dev is applying for this request"));
                //    //}
                //    else
                //    {
                //        return BadRequest(new ResponseResult(400, "NEW / ECN/ ECO Request is not ready for a new request"));
                //    }
                //}
                //else // DEV
                //{

                //    if (isReady && !isPending) //!isDev &&
                //    {
                //        await context.Database.ExecuteSqlCommandAsync(SPRegistration.Registration_add, model.CustId, model.StationId, model.RouteStep, model.TypeId, model.PlatformId, model.Family, model.Description, model.CreatedBy, model.CreatedName, model.CreatedEmail);
                //        return Ok(new ResponseResult(200, "DEV"));
                //    }
                //    else if (isPending)
                //    {
                //        return BadRequest(new ResponseResult(400, "DEV request is pending submission"));

                //    }
                //    //else if (isDev)
                //    //{
                //    //    return BadRequest(new ResponseResult(400, "Dev is applying for this request"));
                //    //}
                //    else
                //    {
                //        return BadRequest(new ResponseResult(400, "NEW / ECN/ ECO Request is not ready for a new DEV request"));
                //    }
                //}
            }
            //try
            //{
            //    await context.Database.ExecuteSqlCommandAsync(SPRegistration.Registration_add, model.CustId, model.StationId, model.RouteStep, model.TypeId, model.PlatformId, model.Family, model.Description, model.CreatedBy, model.CreatedName, model.CreatedEmail);
            //    return Ok(new ResponseResult(200, "ok"));
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new ResponseResult(400, ex.Message));
            //}
        }
        [HttpGet("CheckAssy/{assy}")]
        public bool CheckAssy(string assy)
        {
            var assyList = context.Assembly.Where(s => s.AssemblyNumber == assy);
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
                //await context.Database.ExecuteSqlCommandAsync(SPRegistration.Registration_submit, model.RegId, model.ScriptName, model.ScriptRev, model.PcnorDevNumber, model.ChangeDetail, model.FileHash, model.OriginalFileName, model.EncryptedFileName, model.Description, model.CreatedBy, model.CreatedName, model.CreatedEmail);
                return Ok(new ResponseResult(200, "Submit successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult(400, ex.Message));
            }
        }
    }
}
