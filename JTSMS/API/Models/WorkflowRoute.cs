using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace API.Models
{
    public partial class WorkflowRoute
    {
        public int WfrId { get; set; }
        public int TypeId { get; set; }
        public int RouteId { get; set; }
        public int? Sequence { get; set; }
        public int? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public string CreatedEmail { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedName { get; set; }
        public string UpdatedEmail { get; set; }
    }
}
