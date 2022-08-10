using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JTSMS.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using SharedObjects.Extensions;
using Services.Interfaces;
using SharedObjects.ViewModels;
using Microsoft.AspNetCore.Authorization;
using SharedObjects.Commons;
using System.Data.OleDb;
using System.Data;

namespace JTSMS.Controllers
{
    [Authorize]
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService registrationService;
        private readonly IConfiguration configuration;
        private readonly ICommonService commonService;
        private readonly IAdminService adminService;
        Notification notification = new Notification();
        private ADODB.Recordset rs = new ADODB.Recordset();
        public RegistrationController(IRegistrationService registrationService, IConfiguration configuration, ICommonService commonService, IAdminService adminService)
        {
            this.registrationService = registrationService;
            this.configuration = configuration;
            this.commonService = commonService;
            this.adminService = adminService;
        }

        public async Task<IActionResult> Search()
        {
            var ntlogin = User.GetSpecificClaim("Ntlogin");
            ViewData["Customer"] = await commonService.Customer_Get(ntlogin);
            ViewData["Station"] = await commonService.Station_get();
            ViewData["Type"] = await commonService.Type_get();
            ViewData["Platform"] = await commonService.Master_Platform_get();
            return View();
        }
        public async Task<IActionResult> Registration_get([FromBody] RegistrationViewModel model)
        {
            var ntlogin = User.GetSpecificClaim("Ntlogin");
            ViewData["Customer"] = await commonService.Customer_Get(ntlogin);
            ViewData["Station"] = await commonService.Station_get();
            ViewData["Type"] = await commonService.Type_get();
            ViewData["Platform"] = await commonService.Master_Platform_get();

            var results = await registrationService.Registration_get(model);
            return PartialView(results);
        }
        public async Task<IActionResult> Registration_get_by_id(int id)
        {
            var result = await registrationService.Registration_get_by_id(id);
            var Approval_get = await registrationService.Approval_get(id);
            ViewData["Approval_get"] = Approval_get;
            return View(result);
        }
        public async Task<IActionResult> Registration_add([FromBody] RegistrationViewModel model)
        {
            var results = await registrationService.Registration_add(model);
            return Json(new { results = results });
        }
        public async Task<IActionResult> Registration_approve([FromBody] RegistrationViewModel model)
        {
            var results = await registrationService.Registration_approve(model);
            var Registration_get_by_id = await registrationService.Registration_get_by_id(model.RegId);
            var Access_UserRole_Get_By_regId = await adminService.Access_UserRole_Get_By_regId(model.RegId);
            if (results.StatusCode == 200)
            {
                var Approval_get = await registrationService.Approval_get(model.RegId);
                var approval_current = Approval_get.Where(s => s.IsClosed == 0).OrderBy(s => s.Sequence).FirstOrDefault();
                var Cc = configuration["Email:Cc"];

                notification.SentEmail_UpdateStatus(model, Registration_get_by_id, Cc, approval_current, Access_UserRole_Get_By_regId);
            }
            return Json(new { results = results });
        }

        public async Task<IActionResult> Registration_reject([FromBody] RegistrationViewModel model)
        {
            var results = await registrationService.Registration_reject(model);
            var Registration_get_by_id = await registrationService.Registration_get_by_id(model.RegId);
            var Cc = configuration["Email:Cc"];
            if (results.StatusCode == 200)
            {
                notification.SentEmail_UpdateStatus(model, Registration_get_by_id, Cc);
            }
            return Json(new { results = results });
        }
        public async Task<IActionResult> CheckAssy(string assy)
        {
            var results = await registrationService.CheckAssy(assy);
            return Json(new { results = results });
        }
        public async Task<IActionResult> Registration_submit([FromBody] RegistrationViewModel model)
        {
            ResponseResult results = new ResponseResult();

            results = await registrationService.Registration_submit(model);
            if (results.StatusCode == 200)
            {
                var Approval_get = await registrationService.Approval_get(model.RegId);
                if (Approval_get.Where(s => s.IsClosed == 0).Any())
                {
                    var approval_current = Approval_get.Where(s => s.IsClosed == 0).OrderBy(s => s.Sequence).FirstOrDefault();
                    try
                    {
                        var CC = configuration["Email:Cc"];
                        notification.SentEmail(model, approval_current, CC);

                    }
                    catch (Exception ex)
                    {

                        results.StatusCode = 400;
                        results.Message = ex.Message;
                    }
                }
                else
                {
                    results.StatusCode = 400;
                    results.Message = "Missing approval list. Please contact administrator!";
                }
            }
            return Json(new { results = results });
        }

        public List<string> DescrText(string step_ID)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            JEMS_3.CR_RouteSteps CRr = new JEMS_3.CR_RouteSteps();
            rs = CRr.ListByFactoryText(configuration["JEMS:MESStaticAPIServer"], configuration["JEMS:MESDatabase"], int.Parse(configuration["JEMS:MESUser_ID"]));

            rs.Filter = "Step_ID ='" + step_ID + "'";

            dataAdapter.Fill(dt, rs);
            List<string> lst = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var DescrText = dt.Rows[i]["DescrText"].ToString();
                lst.Add(DescrText);
            }
            var lst1 = lst.Distinct().ToList();
            lst1.Sort();
            return lst1;
        }
        [HttpPost]
        public IActionResult Upload(UploadModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Files.Count > 0)
                {
                    foreach (var file in model.Files)
                    {
                        //string uploadFolder = @"\\" + configuration["Server"] + @"\JTSMS\Attachment\Test\" + model.Type + "\\";
                        string uploadFolder = @"\\" + configuration["Server"] + @"\JTSMS\Attachment\Test\" + model.Type + "\\" + model.CustName + "\\" + model.Station + "\\" + model.Assembly;

                        if (!Directory.Exists(uploadFolder))
                            Directory.CreateDirectory(uploadFolder);
                        string fileName = string.Empty;
                        var splitName = file.FileName.Split('.');
                        var len = splitName.Length;
                        fileName += string.Join(".", splitName.Take(len - 1)) + "_" + model.Date + "." + splitName.LastOrDefault();
                        //fileName += string.Join("", splitName[..index]) + model.Date + "." + splitName.LastOrDefault();
                        var fullFilePath = Path.Combine(uploadFolder, fileName);

                        using (var stream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }

                }
            }
            return Ok();

        }
        public async Task<IActionResult> Download(string fileName, string type, string custName, string station, string assembly)
        {
            string path = @"\\" + configuration["Server"] + @"\JTSMS\Attachment\Test\" + type + "\\" + custName + "\\" + station + "\\" + assembly + "\\" + fileName;
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".jts", "text/plain"},
                {".jts_encrypted", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
