using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace API.Models
{
    public partial class MasterApproval
    {
        public int ApprovalId { get; set; }
        public int? CustId { get; set; }
        public int? RouteId { get; set; }
        public string Ntlogin { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int IsActive { get; set; }
        public string CreatedBy { get; set; }
    }
}
