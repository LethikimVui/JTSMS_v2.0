using Newtonsoft.Json;
using Services.Interfaces;
using SharedObjects.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CommonService : BaseService, ICommonService
    {
        public async Task<List<VUserRole>> Access_UserRole_Get_By_ScriptId(string scriptId)
        {
            List<VUserRole> results = new List<VUserRole>();
            using (var response = await httpClient.GetAsync("api/common/Access_UserRole_Get_By_ScriptId/"+ scriptId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                results = JsonConvert.DeserializeObject<List<VUserRole>>(apiResponse);
            }
            return results;
        }
        public async Task<List<VRouteStep>> Master_RouteStep_get()
        {
            List<VRouteStep> results = new List<VRouteStep>();
            using (var response = await httpClient.GetAsync("api/common/Master_RouteStep_get/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                results = JsonConvert.DeserializeObject<List<VRouteStep>>(apiResponse);
            }
            return results;
        }

        public async Task<List<VCustomer>> Customer_Get(string ntlogin)
        {
            List<VCustomer> customers = new List<VCustomer>();
            using (var response = await httpClient.GetAsync("api/common/Customer_Get/" + ntlogin))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                customers = JsonConvert.DeserializeObject<List<VCustomer>>(apiResponse);
            }
            return customers;
        }

        public async Task<List<VRoute>> Master_Route_get()
        {
            List<VRoute> results = new List<VRoute>();
            using (var response = await httpClient.GetAsync("api/common/Master_Route_get"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                results = JsonConvert.DeserializeObject<List<VRoute>>(apiResponse);
            }
            return results;
        }

        public async Task<List<VStation>> Station_get()
        {
            List<VStation> results = new List<VStation>();
            using (var response = await httpClient.GetAsync("api/common/Station_get"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                results = JsonConvert.DeserializeObject<List<VStation>>(apiResponse);
            }
            return results;
        }

        public async Task<List<VType>> Type_get()
        {
            List<VType> results = new List<VType>();
            using (var response = await httpClient.GetAsync("api/common/Type_get"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                results = JsonConvert.DeserializeObject<List<VType>>(apiResponse);
            }
            return results;
        }

        public async Task<List<VPlatform>> Master_Platform_get()
        {
            List<VPlatform> results = new List<VPlatform>();
            using (var response = await httpClient.GetAsync("api/common/Master_Platform_get"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                results = JsonConvert.DeserializeObject<List<VPlatform>>(apiResponse);
            }
            return results;
        }
    }
}
