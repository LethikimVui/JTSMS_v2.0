using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.ViewModels
{
    public class ConfigViewModel
    {
        public int WdconfigId { get; set; }
        public int CustId { get; set; }
        public int PlatformId { get; set; }
        public string ProcessStep { get; set; }
        public string RouteStep { get; set; }
        public string AssyNumber { get; set; }
        public string AssyRev { get; set; }
        public string TesterName { get; set; }
        public string TesterPcName { get; set; }
        public int EquipmentId { get; set; }
        public string Equipment { get; set; }
        public int TestTime { get; set; }
        public int IsDmz { get; set; }
        public int Trigger { get; set; }
        public int IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public string CreatedEmail { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedName { get; set; }
        public string UpdatedEmail { get; set; }
    }
}
