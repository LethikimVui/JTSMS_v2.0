using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.StoredProcedures
{
    public class SPAdmin
    {
        public static string Access_Role_get = "call usp_Access_Role_get";
        public static string Access_UserRole_get = "call usp_Access_UserRole_Get (@p0,@p1,@p2)";
        public static string Access_UserRole_Get_By_Id = "call usp_Access_UserRole_Get_By_Id (@p0)";
        public static string Access_UserRole_Get_By_regId = "call usp_Access_UserRole_Get_By_regId (@p0)";
        public static string Access_UserRole_insert = "call usp_Access_UserRole_insert (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)";
        public static string Access_UserRole_update = "call usp_Access_UserRole_update (@p0,@p1,@p2,@p3,@p4,@p5,@p6)";
        public static string Access_UserRole_delete = "call usp_Access_UserRole_delete (@p0,@p1,@p2,@p3)";


        //Approval
        //public static string Master_Approval_get = "usp_Master_Approval_get";
        //public static string Master_Approval_Get_By_Id = "usp_Master_Approval_Get_By_Id @p0";
        public static string Master_Approval_get_by_routeId = "call usp_Master_Approval_get_by_routeId (@p0)";
        public static string Master_Approval_insert = "call usp_Master_Approval_insert (@p0,@p1,@p2,@p3,@p4,@p5)";
        //public static string Master_Approval_update = "usp_Master_Approval_update @p0,@p1,@p2,@p3,@p4";
        public static string Master_Approval_delete = "call usp_Master_Approval_delete (@p0,@p1)";


        public static string Master_Route_add = "call usp_Master_Route_add (@p0,@p1,@p2,@p3)";




        public static string WorkFlow_Route_get = "call usp_WorkFlow_Route_get (@p0)";
        public static string WorkFlow_Route_add = "call usp_WorkFlow_Route_add (@p0,@p1,@p2,@p3,@p4,@p5,@p6)";
        public static string WorkFlow_Route_update = "call usp_WorkFlow_Route_update (@p0,@p1,@p2,@p3,@p4,@p5,@p6)";



    }
}
