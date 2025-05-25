using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Queries.Entry.CreateFav
{
    public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
    {
        public Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.FavExchangedName,
                                                exchangeType: SozlukContants.DefaultExchangeType,
                                                queueName: SozlukContants.CreateEntryFavQueueName,
                                                obj: new CreateEntryFavEvent()
                                                {
                                                    EntryId=request.EntryId.Value,
                                                    CreateBy=request.UserId.Value
                                                });
            return Task.FromResult(true);
        }
    }
}
