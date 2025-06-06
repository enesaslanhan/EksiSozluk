﻿using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.CreateFav
{
    public class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>
    {
        public Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.FavExchangedName,
                exchangeType: SozlukContants.DefaultExchangeType,
                queueName: SozlukContants.CreateEntryCommentFavQueueName, obj: new CreatedEntryCommentFavEvent()
                {
                    CreateBy = request.UserId,
                    EntryCommentId = request.EntryCommentId,
                });

            return Task.FromResult(true);
        }
    }
}
