using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.ValueObjects
{
    public class VDetail
    {
        public int ReqId { get; set; }
        public string ReqNumber { get; set; }
        public int CustId { get; set; }
        public string CustName { get; set; }
        public int StationId { get; set; }
        public string routeStepId { get; set; }
        public string Station { get; set; }
        public string AssemblyNumber { get; set; }
        public string AssemblyRevision { get; set; }
        public string ScriptId { get; set; }
        public string ScriptName { get; set; }
        public string ScriptRev { get; set; }
        public string PcnorDevNumber { get; set; }
        public string ChangeDetail { get; set; }
        public string Description { get; set; }
        public string FileHash { get; set; }
        public string ScriptFileName { get; set; }
        public string EncriptedFileName { get; set; }
        //public string IsDeviation { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public string CreatedEmail { get; set; }
        public DateTime? creationDate { get; set; }
        public string UpdatedBy { get; set; }
        public string Firmware { get; set; }
        public string FirmwareRevision { get; set; }
        public string StatusName { get; set; }
        public string StatusColour { get; set; }
        public string Platform { get; set; }
    }
}
