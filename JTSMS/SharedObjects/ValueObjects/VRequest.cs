using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.ValueObjects
{
    public class VRequest :VRegistration
    {
        public int? DetailId { get; set; }
        //public string AssemblyNumber { get; set; }
        public string PcnorDevNumber { get; set; }
        public string FileHash { get; set; }
        public string OriginalFileName { get; set; }
        public string EncriptedFileName { get; set; }
        public string ChangeDetail { get; set; }
        public string ScriptName { get; set; }
        public string ScriptRev { get; set; }
    }
}
