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
    public record DeleteFolloweeUserCommand(int FolloweeId) : ICommand<Result>;
    public class DeleteFolloweeUserCommandValidator : AbstractValidator<DeleteFolloweeUserCommand>
    {
        public DeleteFolloweeUserCommandValidator()
        {
            RuleFor(command => command.FolloweeId)
                .GreaterThan(0);
        }
    }

    public class DeleteFolloweeUserCommandHandler(
        IRepository<AppUser> appusers,
        IUser user) : ICommandHandler<DeleteFolloweeUserCommand, Result>
    {
        public async Task<Result> Handle(DeleteFolloweeUserCommand request, CancellationToken cancellationToken)
        {
            var spec = new FolloweeUserByIdSpec(user.Id!.Value, request.FolloweeId);
            var appuser = await appusers.GetSingleOrDefaultAsync(spec, cancellationToken);
            if (appuser == null) return Result.NotFound("用户不存在");

            appuser.RemoveFollowee(request.FolloweeId);

            await appusers.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
