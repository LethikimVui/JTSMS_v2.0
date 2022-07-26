using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JTSMS.Models
{
    public class UploadModel
    {
        [Required(ErrorMessage = "Please select file")]
        public List<IFormFile> Files { get; set; }
        //public IFormFile File { get; set; }
        public string Type { get; set; }
        public string CustName { get; set; }
        public string Station { get; set; }
        public string Assembly { get; set; }
        public string Date { get; set; }
       
      
    }
  
}
