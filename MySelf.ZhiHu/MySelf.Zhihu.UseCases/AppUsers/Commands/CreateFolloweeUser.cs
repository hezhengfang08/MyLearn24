using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using MySelf.Zhihu.Core.AppUserAggregate.Specifications;
using MySelf.Zhihu.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.AppUsers.Commands
{
    public record CreateFolloweeUserCommand(int FolloweeId) : ICommand<Result>;
    public class CreateFolloweeUserCommandValidator : AbstractValidator<CreateFolloweeUserCommand>
    {
        public CreateFolloweeUserCommandValidator()
        {
            RuleFor(command => command.FolloweeId)
                .GreaterThan(0);
        }
    }

    public class CreateFolloweeUserCommandHandler(
    IRepository<AppUser> userRepo,
    IAppUserService appUserService,
    IUser user) : ICommandHandler<CreateFolloweeUserCommand, Result>
    {
        public async Task<Result> Handle(CreateFolloweeUserCommand request, CancellationToken cancellationToken)
        {
            var spec = new FolloweeUserByIdSpec(user.Id!.Value, request.FolloweeId);
            var appuser = await userRepo.GetSingleOrDefaultAsync(spec, cancellationToken);
            if (appuser == null) return Result.NotFound("用户不存在");

            var result = await appUserService.FolloweeUserAsync(appuser, request.FolloweeId, cancellationToken);
            if (!result.IsSuccess) return result;

            await userRepo.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
