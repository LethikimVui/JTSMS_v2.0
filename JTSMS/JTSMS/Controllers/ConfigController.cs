using JTSMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using SharedObjects.ValueObjects;
using SharedObjects.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;
using SharedObjects.Extensions;

namespace JTSMS.Controllers
{
    [Authorize]

    public class ConfigController : Controller
    {
        private readonly IConfigService configService;
        private readonly ICommonService commonService;
        private readonly IConfiguration configuration;

        private ADODB.Recordset rs = new ADODB.Recordset();

        public ConfigController(IConfigService configService, ICommonService commonService, IConfiguration configuration)
        {
            this.configService = configService;
            this.commonService = commonService;
            this.configuration = configuration;
        }
        public async Task<IActionResult> Search()
        {
            var ntlogin = User.GetSpecificClaim("Ntlogin");

            ViewData["Customer"] = await commonService.Customer_Get(ntlogin);
            ViewData["Station"] = await commonService.Station_get();
            return View();
        }

        public async Task<IActionResult> Insert()
        {
            var ntlogin = User.GetSpecificClaim("Ntlogin");

            ViewData["Customer"] = await commonService.Customer_Get(ntlogin);
            //ViewData["RouteStep"] = await commonService.Master_RouteStep_get();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] ConfigViewModel model)
        {
            var result = await configService.WatchDogConfig_insert(model);
            return Json(new { results = result });
        }
        public int EquipmentId(string commonName)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            JEMS_3.CR_Equipment CRa = new JEMS_3.CR_Equipment();
            rs = CRa.ListByCommonName("VNHCMM0MSSQLV1A", "JEMS", 7022, commonName);
            //rs.Filter = "Number ='" + assy + "'";
            dataAdapter.Fill(dt, rs);
            if (dt.Rows.Count != 0)
            {
                var results = Int32.Parse(dt.Rows[0]["Equipment_ID"].ToString());
                return results;
            }
            else
            {
                return 0;
            }
        }
        [HttpPost]
        public IActionResult Search_Equipment(string commonName)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            JEMS_3.CR_Equipment CRa = new JEMS_3.CR_Equipment();
            rs = CRa.ListByCommonName("VNHCMM0MSSQLV1A", "JEMS", 7022, commonName);
            //rs.Filter = "Number ='" + assy + "'";
            dataAdapter.Fill(dt, rs);
            List<EquipmentViewModel> lst = new List<EquipmentViewModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                EquipmentViewModel t = new EquipmentViewModel()
                {
                    CommonName = dt.Rows[i]["CommonName"].ToString(),
                    Equipment_ID = dt.Rows[i]["Equipment_ID"].ToString()

                };
                lst.Add(t);
            }         
          
            return PartialView(lst);
        }
    }
}
