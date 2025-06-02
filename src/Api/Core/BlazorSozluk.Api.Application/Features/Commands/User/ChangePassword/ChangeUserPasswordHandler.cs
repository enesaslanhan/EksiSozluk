using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Queries.User.ChangePassword
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand,bool>
    {
        private IUserRepository _userRepository;

        public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.UserId.HasValue)
                throw new ArgumentNullException(nameof(request.UserId));

            var dbUser = await _userRepository.GetByIdAsync(request.UserId.Value);
            if (dbUser == null)
                throw new DatabaseValidateException("User not found!");

            if (dbUser.Password != PasswordEncryptor.Encrpt(request.OldPassword))
                throw new DatabaseValidateException("Old Password wrong!");

            dbUser.Password=PasswordEncryptor.Encrpt(request.NewPassword);
            await _userRepository.UpdateAsync(dbUser);
            return true;
        }
    }
}
