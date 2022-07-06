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
    public interface IConfigService
    {
        Task<List<VConfig>> WatchDogConfig_get();
        Task<ResponseResult> WatchDogConfig_insert(ConfigViewModel model);


    }
}
