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
    public interface IRegistrationService
    {

        Task<List<VRegistration>> Registration_get(RegistrationViewModel model);
        Task<ResponseResult> Registration_submit(RegistrationViewModel model);
        Task<ResponseResult> Registration_approve(RegistrationViewModel model);
        Task<ResponseResult> Registration_reject(RegistrationViewModel model);
        Task<VRequest> Registration_get_by_id(int id);
        Task<ResponseResult> Registration_add(RegistrationViewModel model);
        Task<bool> CheckAssy(string assy);


        Task<List<VApproval>> Approval_get(int id);

    }
}
