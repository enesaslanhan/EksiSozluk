using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorSozluk.Api.Application.Features.Queries.GetEntries;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetEntriesQueryHandler : IRequestHandler<GetEntiresQuery, List<GetEntiresViewModel>>
{
    private readonly IEntryRepository _entryRepository;
    private readonly IMapper _mapper;

    public GetEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
    {
        _entryRepository = entryRepository;
        _mapper = mapper;
    }

    public async Task<List<GetEntiresViewModel>> Handle(GetEntiresQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        if (request.TodaysEntries)
        {
            query = query.Where(x => x.CreatedDate >= DateTime.Now.Date)
                         .Where(x=> x.CreatedDate<=DateTime.Now.Date);
        }
        query=query.Include(x=>x.EntryComments).OrderBy(x=>Guid.NewGuid()).Take(request.Count);

        return await query.ProjectTo<GetEntiresViewModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}

