using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.StoredProcedures
{
    public class SPRegistration
    {
        public static string Registration_get = "call usp_Registration_get (@p0,@p1,@p2,@p3,@p4)";
        public static string Registration_get_by_id = "call usp_Registration_get_by_id (@p0)";
        public static string Registration_add = "call usp_Registration_add (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)";


        public static string Registration_submit = "call usp_Registration_submit (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)";
        public static string Registration_approve = "call usp_Registration_approve (@p0,@p1,@p2,@p3,@p4,@p5)";
        public static string Registration_reject = "call usp_Registration_reject (@p0,@p1,@p2,@p3,@p4,@p5)";




        public static string Assembly_add = "call usp_Assembly_add (@p0,@p1,@p2,@p3,@p4)";
        public static string Assembly_delete = "call usp_Assembly_delete (@p0)";

        public static string Attachment_add = "call usp_Attachment_add (@p0,@p1)";


        public static string Approval_get = "call usp_Approval_get (@p0)";



    }
}
