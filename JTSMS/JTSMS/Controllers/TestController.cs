using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JTSMS.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace JTSMS.Controllers
{
    public class TestController : Controller
    {
        private readonly IConfiguration configuration;

        public TestController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpGet]
        public IActionResult TestUpload()
        {
            MyForm model = new MyForm();
            return View(model);
        }
        [HttpPost]
        public IActionResult TestUpload([FromForm] MyForm model)
        {
            if (ModelState.IsValid)
            {
                model.IsResponse = true;
                if (model.Files.Count > 0)
                {
                    foreach (var file in model.Files)
                    {
                        string uploadFolder = @"\\" + configuration["Server"] + @"\JTSMS\Attachment\";
                        if (!Directory.Exists(uploadFolder))
                            Directory.CreateDirectory(uploadFolder);
                        var fullFilePath = Path.Combine(uploadFolder, file.FileName);

                        using (var stream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            try
                            {
                                file.CopyTo(stream);
                            }
                            catch (Exception ex)
                            {
                                model.IsSuccess = false;
                                model.Message = "Files uploaded failed";
                            }                            
                        }
                    }
                    model.IsSuccess = true;
                    model.Message = "Files upload successfully";
                }
                else
                {
                    model.IsSuccess = false;
                    model.Message = "Please select files";
                }               
            }
            return View();
        }
        public IActionResult Index()
        {
            MultipleFilesModel model = new MultipleFilesModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Upload(MultipleFilesModel model)
        {
            if (ModelState.IsValid)
            {
               // model.IsResponse = true;
                if (model.Files.Count > 0)
                {
                    foreach (var file in model.Files)
                    {
                        string uploadFolder = @"\\" + configuration["Server"] + @"\JTSMS\Attachment\";
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
            return View("Index", model);
        }
    }
}
