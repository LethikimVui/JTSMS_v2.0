using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.StoredProcedures
{
    public class SPConfig
    {
        public static string WatchDogConfig_insert = "call usp_WatchDogConfig_insert (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12)";
        public static string WatchDogConfig_get = "call usp_WatchDogConfig_get";


        public static string Access_UserRole_Get_By_Id = "call usp_Access_UserRole_Get_By_Id (@p0)";
        public static string Access_UserRole_insert = "call usp_Access_UserRole_insert (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)";
        public static string Access_UserRole_update = "call usp_Access_UserRole_update (@p0,@p1,@p2,@p3,@p4,@p5,@p6)";
        public static string Access_UserRole_delete = "call usp_Access_UserRole_delete (@p0,@p1,@p2,@p3)";


    }
}
