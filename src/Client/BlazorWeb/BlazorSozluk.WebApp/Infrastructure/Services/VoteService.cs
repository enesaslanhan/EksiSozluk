using BlazorSozluk.Common.ViewModels;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;

namespace BlazorSozluk.WebApp.Infrastructure.Services
{


    public class VoteService : IVoteService
    {
        private readonly HttpClient httpClient;

        public VoteService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task DeleteEntryVote(Guid entryId)
        {
            var response = await httpClient.PostAsync($"/api/vote/DeleteEntryVote/{entryId}", null);
            if (!response.IsSuccessStatusCode)
                throw new Exception("DeleteEntryVote error");
        }
        public async Task DeleteEntryCommenVote(Guid entrytCommentId)
        {
            var response = await httpClient.PostAsync($"/api/vote/DeleteEntryCommentVote/{entrytCommentId}", null);
            if (!response.IsSuccessStatusCode)
                throw new Exception("DeleteEntryCommentVote error");
        }

        public async Task CreateEntryUpVote(Guid entryId)
        {
            await CreateEntryVote(entryId, VoteType.UpVote);
        }

        public async Task CreateEntryDownVote(Guid entryId)
        {

            await CreateEntryVote(entryId, VoteType.DownVote);
        }

        public async Task CreateEntryCommentUpVote(Guid entryCommentId)
        {
            await CreateEntryCommentVote(entryCommentId, VoteType.UpVote);
        }

        public async Task CreateEntryCommentDownVote(Guid entryCommentId)
        {

            await CreateEntryCommentVote(entryCommentId, VoteType.DownVote);
        }


        private async Task<HttpResponseMessage> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            var response = await httpClient.PostAsync($"/api/vote/Entry/{entryId}?voteType=" + voteType, null);
            return response;
        }
        private async Task<HttpResponseMessage> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            var response = await httpClient.PostAsync($"/api/vote/EntryComment/{entryCommentId}?voteType=" + voteType, null);
            return response;
        }

    }
}
