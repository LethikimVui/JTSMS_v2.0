using SharedObjects.Commons;
using SharedObjects.ValueObjects;
using SharedObjects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAdminService
    {
        Task<List<VUserRole>> Access_UserRole_get(UserRoleViewModel model);
        Task<List<VUserRole>> Access_UserRole_Get_By_Id(int id);
        Task<ResponseResult> Access_UserRole_insert(UserRoleViewModel model);
        Task<ResponseResult> Access_UserRole_update(UserRoleViewModel model);
        Task<ResponseResult> Access_UserRole_delete(UserRoleViewModel model);
        Task<List<VRole>> Access_Role_get();



        Task<List<VMasterApproval>> Master_Approval_get_by_routeId(int routeId);
        Task<ResponseResult> Master_Approval_insert(UserRoleViewModel model);
        Task<ResponseResult> Master_Approval_delete(UserRoleViewModel model);

    }
}
