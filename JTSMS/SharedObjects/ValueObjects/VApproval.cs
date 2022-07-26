using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.ValueObjects
{
    public class VApproval
    {
        public int TtId { get; set; }
        public int? Sequence { get; set; }      
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public string NTLogin { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? IsClosed { get; set; }
        public string Remarks { get; set; }
        public string UpdatedName { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
