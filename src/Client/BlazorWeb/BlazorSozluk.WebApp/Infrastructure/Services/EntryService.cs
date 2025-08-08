using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;

namespace BlazorSozluk.WebApp.Infrastructure.Services
{
    public class EntryService : IEntryService
    {
        private readonly HttpClient _httpClient;

        public EntryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Guid> CreateEntry(CreateEntryCommand createEntryCommand)
        {
            var res = await _httpClient.PostAsJsonAsync("/api/Entry/CreateEntry",createEntryCommand);
            if (!res.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr=await res.Content.ReadAsStringAsync();
            return new Guid(guidStr.Trim('"'));
        }

        public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand createEntryCommentCommand)
        {
            var res = await _httpClient.PostAsJsonAsync("/api/Entry/CreateEntryComment",createEntryCommentCommand);
            if (!res.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await res.Content.ReadAsStringAsync();
            return new Guid(guidStr.Trim('"'));
        }

        public async Task<List<GetEntiresViewModel>> GetEntires()
        {
            var result = await _httpClient.GetFromJsonAsync<List<GetEntiresViewModel>>("/api/entry?todaysEntries=false&count=30");
            return result;
        }

        public Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
        {
            var result = _httpClient.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"/api/entry/comments/{entryId}?page={page}&pageSize={pageSize}");
            return result;
        }

        public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
        {
            var result = await _httpClient.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/entry/{entryId}");
            return result;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPagedEntries(int page, int pageSize)
        {
            var result = await _httpClient.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/mainpageentries?page={page}&pageSize={pageSize}");
            return result;
        }

        public Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null)
        {
            var result = _httpClient.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/UserEntries?userName={userName}&page={page}&pageSize={pageSize}");
            return result;
        }

        public async Task<List<SearchEntryViewModel>> SearchSubject(string searchText)
        {
            var result = await _httpClient.GetFromJsonAsync<List<SearchEntryViewModel>>($"/api/entry/Search?searchText={searchText}");
            return result;
        }
    }
}
