using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Queries.EntryComment.CreateVote
{
    public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
    {
        public Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.VoteExchangedName,
                exchangeType: SozlukContants.DefaultExchangeType,
                queueName: SozlukContants.CreateEntryCommentVoteQueueName,
                obj: new CreateEntryCommentVoteEvent()
                {
                    EntryCommentId = request.EntryCommentId,
                    CreateBy= request.CreateBy,
                    VoteType= request.VoteType,
                });
            return Task.FromResult(true);
        }
    }
}
