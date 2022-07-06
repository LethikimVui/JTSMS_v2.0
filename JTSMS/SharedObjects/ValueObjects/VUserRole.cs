using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.ValueObjects
{
    public class VUserRole
    {
        public int UserRoleId { get; set; }
        public string Ntlogin { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public int PlantId { get; set; }
        public int? CustId { get; set; }
        public int? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public string CreatedEmail { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedName { get; set; }
        public string UpdatedEmail { get; set; }
        public DateTime? UpdateDate { get; set; }
        //public string Plant { get; set; }
        public string CustName { get; set; }
        public string RoleName { get; set; }
        public string UserEmail { get; set; }
    }
}
