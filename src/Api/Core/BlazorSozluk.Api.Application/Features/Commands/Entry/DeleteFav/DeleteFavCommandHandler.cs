using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteFav
{
    public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand, bool>
    {
        Task<bool> IRequestHandler<DeleteEntryFavCommand, bool>.Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.FavExchangedName,
                exchangeType: SozlukContants.DefaultExchangeType,
                queueName: SozlukContants.DeleteEntryFavQueueName,
                obj: new DeleteEntryFavEvent()
                {
                    EntryId = request.EntryId,
                    CreateBy = request.UserId,
                });

            return Task.FromResult(true);
        }
    }
}
