using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace API.Models
{
    public partial class MasterStation
    {
        public int StationId { get; set; }
        public int? StepId { get; set; }
        public string Station { get; set; }
        public string IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public string CreatedEmail { get; set; }
        public string UpdatedName { get; set; }
        public string UpdatedEmail { get; set; }
    }
}
