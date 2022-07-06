using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
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
    public class RequestController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public RequestController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpPost("Request_add")]
        [Obsolete]
        public async Task<IActionResult> Request_add([FromBody] RequestViewModel model)
        {
            try
            {
                var list = context.Requestdetail.Where(s => s.CustId == model.CustId && s.StationId == model.StationId
                                                         && s.RouteStepId == model.RouteStep && s.AssemblyNumber == model.AssemblyNumber
                                                         && s.PlatformId == model.PlatformId && s.IsActive == 1)
                                                .ToList(); // && s.AssemblyRevision == model.AssemblyRevision
                                                           //var t = list.Where(s => s.StatusId == 2);

                //if (model.TypeId == 1)// New
                //{
                //    if (!list.Where(s => s.TypeId == 1 || s.TypeId == 3).Any())
                //    {
                //        await context.Database.ExecuteSqlCommandAsync(SPRequest.Request_add, model.CustId, model.StationId, model.RouteStep, model.TypeId, model.PlatformId, model.AssemblyNumber, model.AssemblyRevision, model.Description, model.CreatedBy, model.CreatedName, model.CreatedEmail);
                //        return Ok(new ResponseResult(200, "New Request added"));
                //    }
                //    else
                //    {
                //        return Conflict(new ResponseResult(409, "The New / ECO / ECN/ PCN request is implementing"));
                //    }
                //}
                //else if (model.TypeId == 2) // Deviation
                //{
                //    if (list.Where(s => (s.TypeId == 1 || s.TypeId == 3) && s.StatusId == 4).Any())
                //    {
                //        if (!list.Where(s => s.TypeId == 2 && s.StatusId == 4).Any())
                //        {
                //            await context.Database.ExecuteSqlCommandAsync(SPRequest.Request_add, model.CustId, model.StationId, model.RouteStep, model.TypeId, model.PlatformId, model.AssemblyNumber, model.AssemblyRevision, model.Description, model.CreatedBy, model.CreatedName, model.CreatedEmail);
                //            return Ok(new ResponseResult(200, "Deviation Request added"));
                //        }
                //        else
                //        {
                //            return Ok(new ResponseResult(409, "Deviation Request is implementing"));
                //        }
                //    }
                //    else
                //    {
                //        return BadRequest(new ResponseResult(400, "The New / ECO / ECN/ PCN request is not existing"));

                //    }
                //}
                //else if (model.TypeId == 3) // PCN/ECO/ECN
                //{
                //    if (!list.Where(s => s.TypeId == 1 || s.TypeId == 3).Any())             
                //    {

                //            await context.Database.ExecuteSqlCommandAsync(SPRequest.Request_add, model.CustId, model.StationId, model.RouteStep, model.TypeId, model.PlatformId, model.AssemblyNumber, model.AssemblyRevision, model.Description, model.CreatedBy, model.CreatedName, model.CreatedEmail);
                //            return Ok(new ResponseResult(200, "PCN/ECO/ECN Request added"));                      
                //    }
                //    else
                //    {

                //        if (list.Where(s => s.StatusId == 1 || s.StatusId == 2 || s.StatusId == 3).Any())
                //        {
                //            return Conflict(new ResponseResult(409, "The request is already pending approval"));
                //        }
                //        else if (list.Where(s => s.StatusId == 4 && (s.StatusId == 5 || s.StatusId == 6)).Any())
                //        {
                //            return Conflict(new ResponseResult(409, "PCN/ECO/ECN Request is implementing"));


                //        }
                //        else if (list.Where(s => s.StatusId == 5 || s.StatusId == 6).Any())
                //        {
                //            return Ok(new ResponseResult(409, "Deviation Request is implementing"));

                //        }
                //        else
                //        {
                //            await context.Database.ExecuteSqlCommandAsync(SPRequest.Request_add, model.CustId, model.StationId, model.RouteStep, model.TypeId, model.PlatformId, model.AssemblyNumber, model.AssemblyRevision, model.Description, model.CreatedBy, model.CreatedName, model.CreatedEmail);
                //            return Ok(new ResponseResult(200, "PCN/ECO/ECN Request added"));
                //        }

                //    }
                //}
                //else
                //{
                //    return BadRequest(new ResponseResult(400, "ERROR"));

                //}

                if (model.TypeId == 1)// New
                {
                    if (!list.Where(s => s.TypeId == 1).Any())
                    {
                        await context.Database.ExecuteSqlCommandAsync(SPRequest.Request_add, model.CustId, model.StationId, model.RouteStep, model.TypeId, model.PlatformId, model.AssemblyNumber, model.AssemblyRevision, model.Description, model.CreatedBy, model.CreatedName, model.CreatedEmail);
                        return Ok(new ResponseResult(200, "New Request added"));
                    }
                    else
                    {
                        return Conflict(new ResponseResult(409, "The New / ECO / ECN/ PCN request is already existing"));
                    }
                }
                else //PCN / Deviation
                {

                    if (!list.Where(s => s.TypeId == model.TypeId).Any() && list.Where(s => s.StatusId == 4 && (s.TypeId == 1 || s.TypeId == 3)).Any())
                    {
                        await context.Database.ExecuteSqlCommandAsync(SPRequest.Request_add, model.CustId, model.StationId, model.RouteStep, model.TypeId, model.PlatformId, model.AssemblyNumber, model.AssemblyRevision, model.Description, model.CreatedBy, model.CreatedName, model.CreatedEmail);
                        return Ok(new ResponseResult(200, "Request PCN / Deviation added"));
                    }
                    else if (!list.Where(s => s.TypeId == model.TypeId).Any() && list.Where(s => s.StatusId == 6 && s.TypeId == 1).Any())
                    {
                        return Conflict(new ResponseResult(409, "Deviation is applied, you are not allowed to request Deviation! Please check"));
                    }
                    else if (list.Where(s => s.TypeId == model.TypeId).Any())
                    {
                        return Ok(new ResponseResult(409, "Request PCN / Deviation is already existing")); ;
                    }
                    else
                    {
                        return Conflict(new ResponseResult(409, "No record for New request, you are not allowed to request ECN / Deviation! Please check"));
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult(400, ex.Message));
            }

        }
        [HttpPost("Request_submit")]
        [Obsolete]
        public async Task<IActionResult> Request_submit([FromBody] RequestViewModel model)
        {
            try
            {
                await context.Database.ExecuteSqlCommandAsync(SPRequest.Request_submit, model.ReqId, model.TypeId, model.ScriptName, model.ScriptRev, model.PcnorDevNumber, model.ChangeDetail, model.Firmware, model.FirmwareRevision, model.FileHash, model.ScriptFileName, model.EncryptedFileName, model.Description, model.UpdatedBy, model.UpdatedName, model.UpdatedEmail);
                return Ok(new ResponseResult(200, "Request is submitted, you need to get it approved to use in testscript"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult(400, ex.Message));
            }
        }
        [HttpPost("Request_approve")]
        [Obsolete]
        public async Task<IActionResult> Request_approve([FromBody] RequestViewModel model)
        {
            try
            {
                await context.Database.ExecuteSqlCommandAsync(SPRequest.Request_approve, model.ReqId, model.RouteId, model.Remark, model.UpdatedBy, model.UpdatedName, model.UpdatedEmail);
                return Ok(new ResponseResult(200, "Request is approved"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult(400, ex.Message));
            }
        }
        [HttpPost("Request_reject")]
        [Obsolete]
        public async Task<IActionResult> Request_reject([FromBody] RequestViewModel model)
        {
            try
            {
                await context.Database.ExecuteSqlCommandAsync(SPRequest.Request_reject, model.ReqId, model.RouteId, model.Remark, model.UpdatedBy, model.UpdatedName, model.UpdatedEmail);
                return Ok(new ResponseResult(200, "Request is rejected"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult(400, ex.Message));
            }
        }
        //[HttpPost("Request_close")]
        //[Obsolete]
        //public async Task<IActionResult> Request_close([FromBody] RequestViewModel model)
        //{
        //    try
        //    {
        //        await context.Database.ExecuteSqlCommandAsync(SPRequest.Request_close, model.ReqId, model.UpdatedBy, model.UpdatedName, model.UpdatedEmail);
        //        return Ok(new ResponseResult(200, "Deviation is closed, please get your team to updated the Script Id"));
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(new ResponseResult(400, ex.Message));
        //    }
        //}
        [HttpPost("Request_close_deviation")]
        [Obsolete]
        public async Task<IActionResult> Request_close_deviation([FromBody] RequestViewModel model)
        {
            try
            {
                await context.Database.ExecuteSqlCommandAsync(SPRequest.Request_close_deviation, model.ReqId, model.Remark, model.UpdatedBy, model.UpdatedName, model.UpdatedEmail);
                return Ok(new ResponseResult(200, "The Deviation Closure is submmited"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult(400, ex.Message));
            }
        }
        [HttpPost("Request_approve_close_deviation")]
        [Obsolete]
        public async Task<IActionResult> Request_approve_close_deviation([FromBody] RequestViewModel model)
        {
            try
            {
                await context.Database.ExecuteSqlCommandAsync(SPRequest.Request_approve_close_deviation, model.ReqId, model.Remark, model.UpdatedBy, model.UpdatedName, model.UpdatedEmail);
                return Ok(new ResponseResult(200, "The Deviation is Closed"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult(400, ex.Message));
            }
        }
        [HttpPost("RequestDetail_delete")]
        [Obsolete]
        public async Task<IActionResult> RequestDetail_delete([FromBody] RequestViewModel model)
        {
            try
            {
                await context.Database.ExecuteSqlCommandAsync(SPRequest.RequestDetail_delete, model.ReqId, model.UpdatedBy, model.UpdatedName, model.UpdatedEmail);
                return Ok(new ResponseResult(200, "The Request " + model.ReqId + " is already deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult(400, ex.Message));
            }
        }
        //[HttpPost("RequestDetail_filehash_update")]
        //[Obsolete]
        //public async Task<IActionResult> RequestDetail_filehash_update([FromBody] RequestViewModel model)
        //{
        //    try
        //    {
        //        await context.Database.ExecuteSqlCommandAsync(SPRequest.RequestDetail_filehash_update, model.FileHash, model.EncriptedFileName);
        //        return Ok(new ResponseResult(200, "RequestDetail_filehash_update"));
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(new ResponseResult(400, ex.Message));
        //    }
        //}

        [HttpPost("RequestDetail_get")]
        [Obsolete]
        public async Task<List<VDetail>> RequestDetail_get([FromBody] RequestViewModel model)
        {
            var results = await context.Query<VDetail>().AsNoTracking().FromSql(SPRequest.RequestDetail_get, model.CustId, model.StationId, model.AssemblyNumber, model.AssemblyRevision, model.ScriptId).ToListAsync();
            return results;
        }
        [HttpPost("Access_UserRole_Get_By_ScriptId")]
        [Obsolete]
        public async Task<List<VUserRole>> Access_UserRole_Get_By_ScriptId([FromBody] RequestViewModel model)
        {
            var results = await context.Query<VUserRole>().AsNoTracking().FromSql(SPRequest.Access_UserRole_Get_By_ScriptId, model.ScriptId).ToListAsync();
            return results;
        }
        [HttpGet("RequestDetail_get_by_id/{reqId}")]
        [Obsolete]
        public async Task<VDetail> RequestDetail_get_by_id(int reqId)
        {
            var results = await context.Query<VDetail>().AsNoTracking().FromSql(SPRequest.RequestDetail_get_by_id, reqId).ToListAsync();
            return results[0];
        }
        [HttpGet("Approval_get/{reqId}")]
        [Obsolete]
        public async Task<List<VApproval>> Approval_get(int reqId)
        {
            var results = await context.Query<VApproval>().AsNoTracking().FromSql(SPRequest.Approval_get, reqId).ToListAsync();
            return results;
        }
        [HttpGet("Approval_get_current/{reqId}")]
        [Obsolete]
        public async Task<List<VApproval>> Approval_get_current(int reqId)
        {
            var results = await context.Query<VApproval>().AsNoTracking().FromSql(SPRequest.Approval_get_current, reqId).ToListAsync();
            return results;
        }
        [HttpGet("Approval_get_deviation/{reqId}")]
        [Obsolete]
        public async Task<List<VApproval>> Approval_get_deviation(int reqId)
        {
            var results = await context.Query<VApproval>().AsNoTracking().FromSql(SPRequest.Approval_get_deviation, reqId).ToListAsync();
            return results;
        }

    }
}
