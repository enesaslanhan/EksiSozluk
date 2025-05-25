using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Queries.Entry.CreateVote
{
    public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
    {
        public Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.VoteExchangedName,
                exchangeType: SozlukContants.DefaultExchangeType,
                queueName: SozlukContants.CreateEntryVoteQueueName,
                obj: new CreateEntryVoteEvent()
                {
                    EntryId= request.EntryId,
                    CreateBy= request.CreateBy,
                    VoteType= request.VoteType,
                });
            return Task.FromResult(true);
        }
    }
}
