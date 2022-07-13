using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JTSMS.Models
{
    public class MyForm : ReponseModel1
    {
        public List<IFormFile> Files { get; set; }
        public IFormFile File { get; set; }
        public string Reply { get; set; }
        public int Mid { get; set; }
      
    }
    public class ReponseModel1
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsResponse { get; set; }
    }
}
