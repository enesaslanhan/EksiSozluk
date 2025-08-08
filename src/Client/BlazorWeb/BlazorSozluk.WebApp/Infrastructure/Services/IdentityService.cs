using Blazored.LocalStorage;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Infrastructure.Results;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;
using System.Security.Principal;
using BlazorSozluk.WebApp.Infrastructure.Extensions;

namespace BlazorSozluk.WebApp.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly ISyncLocalStorageService _syncLocalStorageService;

        public IdentityService(HttpClient httpClient, ISyncLocalStorageService syncLocalStorageService)
        {
            _httpClient = httpClient;
            _syncLocalStorageService = syncLocalStorageService;
        }

        public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

        public Guid GetUserId()
        {
            return _syncLocalStorageService.GetUserId();
        }

        public string GetUserName()
        {
            return _syncLocalStorageService.GetToken();
        }

        public string GetUserToken()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Login(LoginUserCommand command)
        {
            string responseStr;
            var httpResponse = await _httpClient.PostAsJsonAsync("/api/User/Login", command);
            if (httpResponse!=null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode==System.Net.HttpStatusCode.BadRequest)
                {
                    responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation=JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidateException(responseStr);
                }
                return false;
            }
            responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response=JsonSerializer.Deserialize<LoginUserViewModel>(responseStr);

            if (!string.IsNullOrEmpty(response.Token))
            {
                _syncLocalStorageService.SetToken(response.Token);
                _syncLocalStorageService.SetUsername(response.UserName);
                _syncLocalStorageService.SetUserId(response.Id);

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", response.Token);
                return true;
            }
            return false;
        }

        public void Logout()
        {
            _syncLocalStorageService.RemoveItem(LocalStorageExtension.TokenName);
            _syncLocalStorageService.RemoveItem(LocalStorageExtension.UserId);
            _syncLocalStorageService.RemoveItem(LocalStorageExtension.UserName);

            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
