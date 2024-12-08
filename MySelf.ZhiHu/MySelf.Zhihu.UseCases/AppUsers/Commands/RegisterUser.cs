using AutoMapper;
using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using MySelf.Zhihu.Core.Data;
using MySelf.Zhihu.SharedKernel.Messaging;
using MySelf.Zhihu.SharedKernel.Result;
using MySelf.Zhihu.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.AppUsers.Commands
{
    public record RegisterUserCommand(string UserName, string Password) :ICommand<Result<AppUserDto>>;
    public class RegisterUserCommandHandler(
     IIdentityService identityService,
     IRepository<AppUser> userRepo,
  IMapper mapper) : ICommandHandler<RegisterUserCommand, Result<AppUserDto>>
    {
        public async Task<Result<AppUserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await identityService.CreateUserAsync(request.UserName, request.Password);

            if (result.IsSuccess)
            {
                var user = userRepo.Add(new AppUser(result.Value)
                {
                    Nickname = $"新用户{result.Value}"
                });
                await userRepo.SaveChangesAsync(cancellationToken);

                return Result.Success(mapper.Map<AppUserDto>(user));
            }

            return Result.Failure(result.Errors);
        }
    }
}
