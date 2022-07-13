using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JTSMS.Models
{
    public class UploadModel
    {
        public List<IFormFile> Files { get; set; }
        //public IFormFile File { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
       
      
    }
  
}
