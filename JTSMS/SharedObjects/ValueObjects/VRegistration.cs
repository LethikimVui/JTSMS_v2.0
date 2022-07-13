using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.ValueObjects
{
    public class VRegistration
    {
        public int? RegId { get; set; }
        public int CustId { get; set; }
        public string CustName { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public string Platform { get; set; }
        //public int StationId { get; set; }
        public string Station { get; set; }
        public string RouteStep { get; set; }
        public string Family { get; set; }
        public string ScriptId { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusColour { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public string CreatedEmail { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
