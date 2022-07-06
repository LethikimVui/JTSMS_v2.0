using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.ViewModels
{
    public class UserRoleViewModel
    {
        public string Ntlogin { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int RoleId { get; set; }
        public int UserRoleId { get; set; }
        public int ApprovalId { get; set; }
        public int OwnerId { get; set; }
        public byte PlantId { get; set; }
        public int CustId { get; set; }
        public int RouteId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public string CreatedEmail { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedName { get; set; }
        public string UpdatedEmail { get; set; }
    }
}
