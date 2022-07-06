using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.ValueObjects
{
    public class VConfig
    {
        public int WdconfigId { get; set; }
        public int CustId { get; set; }
        public string ProcessStep { get; set; }
        public string RouteStep { get; set; }
        public string AssyNumber { get; set; }
        public string AssyRev { get; set; }
        public string TesterName { get; set; }
        public string TesterPcName { get; set; }
        public int EquipmentId { get; set; }
        public int TestTime { get; set; }
        public int IsDmz { get; set; }
        public int IsActive { get; set; }
    }
}
