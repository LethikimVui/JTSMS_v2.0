using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.ViewModels
{
    public class RequestViewModel
    {
        public int TtId { get; set; }
        public int ReqId { get; set; }
        public int RouteId { get; set; }
        public string RouteStep { get; set; }
        public string ReqNumber { get; set; }
        public int? CustId { get; set; }
        public int? StationId { get; set; }
        public string AssemblyNumber { get; set; }
        public string AssemblyRevision { get; set; }
        public string ScriptId { get; set; }
        public string ScriptName { get; set; }
        public string ScriptRev { get; set; }
        public string PcnorDevNumber { get; set; }
        public string ChangeDetail { get; set; }
        public string Firmware { get; set; }
        public string FirmwareRevision { get; set; }
        public string Description { get; set; }
        public string FileHash { get; set; }
        public string IsDeviation { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public string CreatedEmail { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedName { get; set; }
        public string UpdatedEmail { get; set; }
        public int TypeId { get; set; }
        public int PlatformId { get; set; }
        public string File { get; set; }
        public string EncryptedFileName { get; set; }
        public string ScriptFileName { get; set; }
        public string Remark { get; set; }
        public string Action { get; set; }

    }
}
