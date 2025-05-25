using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Queries.User.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailComfirmationRepository _emailComfirmationRepository;

        public ConfirmEmailCommandHandler(IUserRepository userRepository, IEmailComfirmationRepository emailComfirmationRepository)
        {
            _userRepository = userRepository;
            _emailComfirmationRepository = emailComfirmationRepository;
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var confirmation = await _emailComfirmationRepository.GetByIdAsync(request.ConfirmationId);
            if (confirmation == null)
                throw new DatabaseValidateException("Confirmation not found!");
            var dbUser = await _userRepository.GetSingleAsync(x => x.Email == confirmation.NewEmailAddress);

            if (dbUser == null)
                throw new DatabaseValidateException("User not found! with this email!");
            if (dbUser.EmailConfirmed)
                throw new DatabaseValidateException("Email address is already confirmed!");
            dbUser.EmailConfirmed = true;
            return true;
        }
    }
}
