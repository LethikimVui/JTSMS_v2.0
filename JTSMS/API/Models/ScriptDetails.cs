using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace API.Models
{
    public partial class ScriptDetails
    {
        public int IdscriptDetails { get; set; }
        public string Workcell { get; set; }
        public string TestStationName { get; set; }
        public string AssemblyNo { get; set; }
        public string AssemblyRev { get; set; }
        public string TestScriptId { get; set; }
        public string TestScriptName { get; set; }
        public string TestScriptRev { get; set; }
        public string TestScriptDesc { get; set; }
        public string TestScriptFileHash { get; set; }
        public string TestScriptFileLocation { get; set; }
        public string ApprovalStatus { get; set; }
        public string ChangeType { get; set; }
        public string RequesteddBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
