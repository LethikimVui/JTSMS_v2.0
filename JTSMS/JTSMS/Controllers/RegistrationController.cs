using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JTSMS.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using Services.Interfaces;
using SharedObjects.ViewModels;

namespace JTSMS.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService registrationService;
        private readonly IConfiguration configuration;
        private readonly ICommonService commonService;

        public RegistrationController(IRegistrationService registrationService, IConfiguration configuration, ICommonService commonService)
        {
            this.registrationService = registrationService;
            this.configuration = configuration;
            this.commonService = commonService;
        }
        public async Task<IActionResult> Search()
        {
            ViewData["Customer"] = await commonService.Customer_Get();
            ViewData["Station"] = await commonService.Station_get();
            return View();
        }

        public async Task<IActionResult> Registration_get([FromBody] RegistrationViewModel model)
        {
            ViewData["Customer"] = await commonService.Customer_Get();
            ViewData["Station"] = await commonService.Station_get();
            ViewData["Type"] = await commonService.Type_get();

            var results = await registrationService.Registration_get(model);
            return PartialView(results);
        }
        public async Task<IActionResult> Registration_get_by_id(int id)
        {
            var result = await registrationService.Registration_get_by_id(id);
            return View(result);
        }
        public async Task<IActionResult> Registration_add([FromBody] RegistrationViewModel model)
        {
            var results = await registrationService.Registration_add(model);
            return Json(new { results = results });
        }

        public async Task<IActionResult> CheckAssy(string assy)
        {
            var results = await registrationService.CheckAssy(assy);
            return Json(new { results = results });
        }

        public async Task<IActionResult> Registration_submit([FromBody] RegistrationViewModel model)
        {
            var results = await registrationService.Registration_submit(model);
            return Json(new { results = results });
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
                        string uploadFolder = @"\\" + configuration["Server"] + @"\JTSMS\Attachment\Test\" + model.Type +"\\" ;
                        if (!Directory.Exists(uploadFolder))
                            Directory.CreateDirectory(uploadFolder);
                        var fullFilePath = Path.Combine(uploadFolder, file.FileName);

                        using (var stream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }

                }
            }
            return Ok();

        }

    }
}
