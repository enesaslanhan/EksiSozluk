﻿using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Queries.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existsUser = await _userRepository.GetSingleAsync(x => x.Email == request.Email);

            if (existsUser is not null)
                throw new DatabaseValidateException("User already exists");

            var dbUser = _mapper.Map<Domain.Models.User>(request);
            var rows = await _userRepository.AddAsync(dbUser);

            //Email Changed/Created
            if (rows>0)
            {
                var @event=new UserEmailChangedEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress=dbUser.Email,
                };
                QueueFactory.SendMessageToExchange(exchangeName:SozlukContants.UserExchangedName,
                                                   exchangeType:SozlukContants.DefaultExchangeType,
                                                   queueName:SozlukContants.UserEmailExchangedQueueName,
                                                   obj:@event);
            }
            return dbUser.Id;
        }
    }
}
