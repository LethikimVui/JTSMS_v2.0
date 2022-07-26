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
    public class AdminService : BaseService, IAdminService
    {
        public async Task<List<VRole>> Access_Role_get()
        {
            List<VRole> roles = new List<VRole>();

            using (var response = await httpClient.GetAsync("api/Admin/Access_Role_get"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                roles = JsonConvert.DeserializeObject<List<VRole>>(apiResponse);
            }
            return roles;
        }
        public async Task<ResponseResult> Access_UserRole_delete(UserRoleViewModel model)
        {
            ResponseResult responseResult = new ResponseResult();
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync("api/admin/Access_UserRole_delete", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
            }
            return responseResult;
        }
        public async Task<List<VUserRole>> Access_UserRole_get(UserRoleViewModel model)
        {
            List<VUserRole> userRoles = new List<VUserRole>();
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync("api/admin/Access_UserRole_get", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                userRoles = JsonConvert.DeserializeObject<List<VUserRole>>(apiResponse);
            }
            return userRoles;
        }
        public async Task<List<VUserRole>> Access_UserRole_Get_By_Id(int id)
        {
            List<VUserRole> userRoles = new List<VUserRole>();
            using (var response = await httpClient.GetAsync("api/Admin/Access_UserRole_Get_By_Id/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                userRoles = JsonConvert.DeserializeObject<List<VUserRole>>(apiResponse);
            }
            return userRoles;
        }
        public async Task<ResponseResult> Access_UserRole_insert(UserRoleViewModel model)
        {
            ResponseResult responseResult = new ResponseResult();
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync("api/admin/Access_UserRole_insert", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
            }
            return responseResult;
        }
        public async Task<ResponseResult> Access_UserRole_update(UserRoleViewModel model)
        {
            ResponseResult responseResult = new ResponseResult();
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync("api/admin/Access_UserRole_update", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
            }
            return responseResult;
        }

        public async Task<List<VMasterApproval>> Master_Approval_get_by_routeId(int routeId)
        {
            List<VMasterApproval> userRoles = new List<VMasterApproval>();
            using (var response = await httpClient.GetAsync("api/Admin/Master_Approval_get_by_routeId/" + routeId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                userRoles = JsonConvert.DeserializeObject<List<VMasterApproval>>(apiResponse);
            }
            return userRoles;
        }

        public async Task<ResponseResult> Master_Approval_insert(UserRoleViewModel model)
        {
            ResponseResult responseResult = new ResponseResult();
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync("api/admin/Master_Approval_insert", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
            }
            return responseResult;
        }
        public async Task<ResponseResult> Master_Approval_delete(UserRoleViewModel model)
        {
            ResponseResult responseResult = new ResponseResult();
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync("api/admin/Master_Approval_delete", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
            }
            return responseResult;
        }

        public async Task<List<VWorkFlow>> WorkFlow_Route_get(int id)
        {
            List<VWorkFlow> results = new List<VWorkFlow>();
            using (var response = await httpClient.GetAsync("api/Admin/WorkFlow_Route_get/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                results = JsonConvert.DeserializeObject<List<VWorkFlow>>(apiResponse);
            }
            return results;
        }

        public async Task<ResponseResult> WorkFlow_Route_update(RouteViewModel model)
        {

            ResponseResult responseResult = new ResponseResult();
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync("api/admin/WorkFlow_Route_update", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
            }
            return responseResult;
        }

        public async Task<ResponseResult> WorkFlow_Route_add(RouteViewModel model)
        {
            ResponseResult responseResult = new ResponseResult();
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync("api/admin/WorkFlow_Route_add", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
            }
            return responseResult;
        }

        public async Task<ResponseResult> Master_Route_add(RouteViewModel model)
        {
            ResponseResult responseResult = new ResponseResult();
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync("api/admin/Master_Route_add", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
            }
            return responseResult;
        }
    }
}
