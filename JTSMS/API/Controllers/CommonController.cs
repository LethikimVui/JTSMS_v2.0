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
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CommonController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("JTSMS_ConnectionString")]
        [Obsolete]
        public async Task<string> JTSMS_ConnectionString()
        {
            var results = await context.Query<VScriptId>().AsNoTracking().FromSql(SPCommon.JTSMS_ConnectionString).ToListAsync();
            return results[0].ScriptId;
        }
        [HttpGet("Customer_get/{ntlogin}")]
        [Obsolete]
        public async Task<List<VCustomer>> Customer_get(string ntlogin)
        {
            var results = await context.Query<VCustomer>().AsNoTracking().FromSql(SPCommon.Customer_get, ntlogin).ToListAsync();
            return results;
        }
        [HttpGet("Master_Route_get")]
        [Obsolete]
        public async Task<List<VRoute>> Master_Route_get()
        {
            var results = await context.Query<VRoute>().AsNoTracking().FromSql(SPCommon.Master_Route_get).ToListAsync();
            return results;
        }
        [HttpGet("Station_get")]
        [Obsolete]
        public async Task<List<VStation>> Station_get()
        {
            var results = await context.Query<VStation>().AsNoTracking().FromSql(SPCommon.Station_get).ToListAsync();
            return results;
        }
        [HttpGet("Type_get")]
        [Obsolete]
        public async Task<List<VType>> Type_get()
        {
            var results = await context.Query<VType>().AsNoTracking().FromSql(SPCommon.Type_get).ToListAsync();
            return results;
        }
        [HttpGet("Master_RouteStep_get")]
        [Obsolete]
        public async Task<List<VRouteStep>> Master_RouteStep_get()
        {
            var results = await context.Query<VRouteStep>().AsNoTracking().FromSql(SPCommon.Master_RouteStep_get).ToListAsync();
            return results;
        }
        [HttpGet("Master_Platform_get")]
        [Obsolete]
        public async Task<List<VPlatform>> Master_Platform_get()
        {
            var results = await context.Query<VPlatform>().AsNoTracking().FromSql(SPCommon.Master_Platform_get).ToListAsync();
            return results;
        }
        [HttpGet("Access_UserRole_Get_By_ScriptId/{scriptId}")]
        [Obsolete]
        public async Task<List<VUserRole>> Access_UserRole_Get_By_ScriptId(string scriptId)
        {
            var results = await context.Query<VUserRole>().AsNoTracking().FromSql(SPCommon.Access_UserRole_Get_By_ScriptId, scriptId).ToListAsync();
            return results;
        }
    }
}
