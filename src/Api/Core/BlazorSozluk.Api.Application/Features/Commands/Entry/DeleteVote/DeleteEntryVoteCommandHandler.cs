using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteVote
{
    public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
    {
        public Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.VoteExchangedName,
                exchangeType: SozlukContants.DefaultExchangeType,
                queueName: SozlukContants.DeleteEntryVoteQueueName,
                obj: new DeleteEntryVoteEvent()
                {
                    EntryId = request.EntryId,
                    UserId = request.UserId,
                });
            return Task.FromResult(true);
        }
    }
}
