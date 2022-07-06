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
    public interface IRequestService
    {
        Task<ResponseResult> Request_add(RequestViewModel model);
        Task<ResponseResult> RequestDetail_delete(RequestViewModel model);
        Task<ResponseResult> RequestDetail_filehash_update(RequestViewModel model);
        Task<ResponseResult> Request_submit(RequestViewModel model);
        Task<ResponseResult> Request_approve(RequestViewModel model);
        Task<ResponseResult> Request_reject(RequestViewModel model);

        Task<ResponseResult> Request_approve_close_deviation(RequestViewModel model);
        Task<ResponseResult> Request_close_deviation(RequestViewModel model);
       
        Task<List<VDetail>> RequestDetail_get(RequestViewModel model);
        Task<VDetail> RequestDetail_get_by_id(int reqId);
        Task<List<VApproval>> Approval_get(int reqId);
        Task<List<VApproval>> Approval_get_current(int reqId);
        Task<List<VApproval>> Approval_get_deviation(int reqId);
        Task<List<VUserRole>> Access_UserRole_Get_By_ScriptId(RequestViewModel model);

    }
}
