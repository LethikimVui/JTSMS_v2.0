using Newtonsoft.Json;
using Services.Interfaces;
using SharedObjects.Commons;
using SharedObjects.ValueObjects;
using SharedObjects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ConfigService : BaseService, IConfigService
    {
        public async Task<List<VConfig>> WatchDogConfig_get()
        {
            List<VConfig> list = new List<VConfig>();
            //StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            using (var response = await httpClient.GetAsync("api/config/WatchDogConfig_get"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<VConfig>>(apiResponse);
            }
            return list;
        }

        public async Task<ResponseResult> WatchDogConfig_insert(ConfigViewModel model)
        {
            ResponseResult responseResult = new ResponseResult();
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync("api/config/WatchDogConfig_insert", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
            }
            return responseResult;
        }
    }
}
