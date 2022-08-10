using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.ValueObjects
{
    public class VAssembly
    {
        public int? RegId { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public bool? IsReady { get; set; }
        public string AssemblyNumber { get; set; }
        public string Type { get; set; }
    }
}
