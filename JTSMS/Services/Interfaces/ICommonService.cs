using SharedObjects.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICommonService
    {
        Task<List<VCustomer>> Customer_Get();
        Task<List<VStation>> Station_get();
        Task<List<VType>> Type_get();
        Task<List<VRoute>> Master_Route_get();
        Task<List<VRouteStep>> Master_RouteStep_get();
        Task<List<VUserRole>> Access_UserRole_Get_By_ScriptId(string scriptId);

    }
}
