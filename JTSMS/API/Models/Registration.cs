using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace API.Models
{
    public partial class Registration
    {
        public int RegId { get; set; }
        public int CustId { get; set; }
        public int TypeId { get; set; }
        public int PlatformId { get; set; }
        public int StationId { get; set; }
        public string RouteStep { get; set; }
        public string Family { get; set; }
        public string ScriptId { get; set; }
        public int StatusId { get; set; }
        public bool IsReady { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public string CreatedEmail { get; set; }
        public DateTime CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedName { get; set; }
        public string UpdatedEmail { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
