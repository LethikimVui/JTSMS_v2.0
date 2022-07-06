using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace API.Models
{
    public partial class Requestdetail
    {
        public int ReqId { get; set; }
        public string ReqNumber { get; set; }
        public int? CustId { get; set; }
        public int? StationId { get; set; }
        public string AssemblyNumber { get; set; }
        public string AssemblyRevision { get; set; }
        public string Scriptid { get; set; }
        public string Scriptname { get; set; }
        public string Scriptrev { get; set; }
        public string Description { get; set; }
        public string Filehash { get; set; }
        public int TypeId { get; set; }
        public string PcnorDevNumber { get; set; }
        public string ChangeDetail { get; set; }
        public int StatusId { get; set; }
        public string Firmware { get; set; }
        public string FirmwareRevision { get; set; }
        public int? PlatformId { get; set; }
        public string ScriptFileName { get; set; }
        public string EncriptedFileName { get; set; }
        public string RouteStepId { get; set; }
        public int IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public string CreatedEmail { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedName { get; set; }
        public string UpdatedEmail { get; set; }
    }
}
