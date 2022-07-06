using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects.StoredProcedures
{
    public class SPAccount
    {
        public static string UserRole_get = "call usp_UserRole_get (@p0)";
    }
}
