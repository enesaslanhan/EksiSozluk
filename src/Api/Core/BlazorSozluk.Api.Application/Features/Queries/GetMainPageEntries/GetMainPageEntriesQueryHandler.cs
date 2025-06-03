using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Extensions;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetMainPageEntries
{
    public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
    {
        private readonly IEntryRepository _entryRepository;
        private readonly IMapper _mapper;

        public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
        {
            _entryRepository = entryRepository;
            _mapper = mapper;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = _entryRepository.AsQueryable();
            query = query.Include(x => x.EntryFavorites)
                        .Include(x => x.CreatedBy)
                        .Include(x => x.EntryVotes);
            var list = query.Select(x => new GetEntryDetailViewModel()
            {
                Id= x.Id,
                Subject= x.Subject,
                Content= x.Content,
                IsFavorited=request.UserId.HasValue && x.EntryFavorites.Any(y=>y.CreatedById==request.UserId),
                FavoritedCount=x.EntryFavorites.Count,
                CreatedByUserName=x.CreatedBy.UserName,
                VoteType=request.UserId.HasValue && x.EntryVotes.Any(z=>z.CreatedById==request.UserId)?
                         x.EntryVotes.FirstOrDefault(z=>z.CreatedById==request.UserId).VoteType
                         :Common.ViewModels.VoteType.None
            });

            var entries = await list.GetPaged(request.Page, request.PageSize);

            return new PagedViewModel<GetEntryDetailViewModel>(entries.Results, entries.PageInfo);
        }
    }
}
