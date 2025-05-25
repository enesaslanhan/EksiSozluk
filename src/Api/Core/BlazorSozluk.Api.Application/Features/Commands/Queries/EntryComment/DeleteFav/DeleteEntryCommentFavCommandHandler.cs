using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Queries.EntryComment.DeleteFav
{
    public class DeleteEntryCommentFavCommandHandler : IRequestHandler<DeleteEntryCommentFavCommand, bool>
    {
        public Task<bool> Handle(DeleteEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.FavExchangedName,
                exchangeType: SozlukContants.DefaultExchangeType,
                queueName: SozlukContants.DeleteEntryCommentFavQueueName,
                obj: new DeleteEntryCommentFavEvent()
                {
                    EntryCommentId = request.EntryCommentId,
                    CreateBy=request.UserId
                });
            return Task.FromResult(true);
        }
    }
}
