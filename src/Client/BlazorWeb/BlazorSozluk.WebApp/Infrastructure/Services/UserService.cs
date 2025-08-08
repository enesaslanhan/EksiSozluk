using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Infrastructure.Results;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorSozluk.WebApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<bool> ChangeUserPassword(string oldPassword, string newPassword)
        {
            var command=new ChangeUserPasswordCommand(null,oldPassword, newPassword);
            var httpResponse = await httpClient.PostAsJsonAsync($"/api/user/ChangePassword", command);

            if (httpResponse!=null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode==System.Net.HttpStatusCode.BadRequest)
                {
                    var responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation=JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidateException(responseStr);
                }
                return true;
            }
            return httpResponse.IsSuccessStatusCode;
        }

        public async Task<UserDetailViewModel> GetUserDetail(Guid? id)
        {
            var userDetail = await httpClient.GetFromJsonAsync<UserDetailViewModel>($"/api/user/{id}");
            return userDetail;
        }

        public async Task<UserDetailViewModel> GetUserDetail(string userName)
        {
            var userDetail = await httpClient.GetFromJsonAsync<UserDetailViewModel>($"/api/user/username/{userName}");
            return userDetail;
        }

        public async Task<bool> UpdateUser(UserDetailViewModel user)
        {
            var res = await httpClient.PostAsJsonAsync($"/api/user/update", user);
            return res.IsSuccessStatusCode;
        }
    }
}
