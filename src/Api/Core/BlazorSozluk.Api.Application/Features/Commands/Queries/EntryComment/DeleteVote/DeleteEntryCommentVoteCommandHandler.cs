using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Queries.EntryComment.DeleteVote
{
    public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
    {
        public Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.VoteExchangedName,
                exchangeType: SozlukContants.DefaultExchangeType,
                queueName: SozlukContants.DeleteEntryCommentVoteQueueName,
                obj: new DeleteEntryCommentVoteEvent()
                {
                    EntryCommentId = request.EntryCommentId,
                    CreateBy=request.UserId
                });
            return Task.FromResult(true);
        }
    }
}
