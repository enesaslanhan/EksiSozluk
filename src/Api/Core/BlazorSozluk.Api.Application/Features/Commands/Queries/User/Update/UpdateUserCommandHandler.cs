using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Queries.User.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.GetByIdAsync(request.Id); 
           
            if (dbUser is null)
                throw new DatabaseValidateException("User Not Found");

            var dbEmail = dbUser.Email;
            var emailChanged = string.CompareOrdinal(dbEmail, request.EmailAdress)!=0;
            _mapper.Map(request,dbUser);
            //dbUser = _mapper.Map<Domain.Models.User>(request);
            var rows = await _userRepository.UpdateAsync(dbUser);

            //Check If Email Changed
            if (emailChanged && rows>0)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAddress = dbEmail,
                    NewEmailAddress = dbUser.Email,
                };
                QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.UserExchangedName,
                                                   exchangeType: SozlukContants.DefaultExchangeType,
                                                   queueName: SozlukContants.UserEmailExchangedQueueName,
                                                   obj: @event);
                dbUser.EmailConfirmed = false;
                await _userRepository.UpdateAsync(dbUser);
            }
            return dbUser.Id;
        }
    }
}
