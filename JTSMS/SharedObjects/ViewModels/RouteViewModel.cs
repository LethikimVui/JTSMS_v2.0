using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.ViewModels
{
    public class RouteViewModel
    {
     
        public int RouteId { get; set; }
        public int TypeId { get; set; }
        public int Sequence { get; set; }
        public int IsActive { get; set; }
        public string RouteName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public string CreatedEmail { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedName { get; set; }
        public string UpdatedEmail { get; set; }
   
    }
}
