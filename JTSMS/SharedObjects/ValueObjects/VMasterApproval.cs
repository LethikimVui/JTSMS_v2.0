using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.ValueObjects
{
    public class VMasterApproval
    {
        //approvalId, custId, routeId, NTLogin, Name, Email, isActive, createdBy, creationDate, routeName, custName
        public int RouteId { get; set; }//
        public int ApprovalId { get; set; }//
        public int CustId { get; set; }//
        public int IsActive { get; set; }//
        public string RouteName { get; set; }//
        public string NTLogin { get; set; }//
        public string Name { get; set; }//
        public string Email { get; set; }//
        public string CreatedBy { get; set; }//
        public string CustName { get; set; }//
    }
}
