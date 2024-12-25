using AutoMapper;
using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.AppUsers.Commands
{
    public record CreateAppUserCommand(int UserId) : ICommand<Result<CreatedAppUserDto>>;

    public class CreateAppUserCommandValidator : AbstractValidator<CreateAppUserCommand>
    {
        public CreateAppUserCommandValidator()
        {
            RuleFor(command => command.UserId)
                .GreaterThan(0);
        }
    }

    public class CreateAppUserCommandHandler(
        IRepository<AppUser> userRepo,
        IMapper mapper) : ICommandHandler<CreateAppUserCommand, Result<CreatedAppUserDto>>
    {
        public async Task<Result<CreatedAppUserDto>> Handle(CreateAppUserCommand command,
            CancellationToken cancellationToken)
        {
            var user = userRepo.Add(new AppUser(command.UserId)
            {
                Nickname = $"新用户{command.UserId}"
            });

            await userRepo.SaveChangesAsync(cancellationToken);

            return Result.Success(mapper.Map<CreatedAppUserDto>(user));
        }
    }
}
