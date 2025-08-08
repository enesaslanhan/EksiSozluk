using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;

namespace BlazorSozluk.WebApp.Infrastructure.Services.Interfaces
{
    public interface IEntryService
    {
        Task<List<GetEntiresViewModel>> GetEntires();
        Task<GetEntryDetailViewModel> GetEntryDetail(Guid Id);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPagedEntries(int page,int pageSize);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page,int pageSize,string userName=null);
        Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize);

        Task<Guid> CreateEntry(CreateEntryCommand createEntryCommand);
        Task<Guid> CreateEntryComment(CreateEntryCommentCommand createEntryCommentCommand);
        Task<List<SearchEntryViewModel>> SearchSubject(string searchText);


    }
}
