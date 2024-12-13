using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using MySelf.Zhihu.Core.AppUserAggregate.Specifications;

using MySelf.Zhihu.Core.Interfaces;
using MySelf.Zhihu.SharedKernel.Messaging;
using MySelf.Zhihu.SharedKernel.Result;
using MySelf.Zhihu.UseCases.Common.Attributes;
using MySelf.Zhihu.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySelf.Zhihu.SharedKernel.Repositoy;

namespace MySelf.Zhihu.UseCases.AppUsers.Commands
{
    [Authorize]
    public record CreateFollowQuestionCommand(int QuestionId) : ICommand<IResult>;
    internal class CreateFollowQuestionCommandHanlderr(
    IRepository<AppUser> userRepo,
    IFollowQuestionService followQuestionService,
    IUser user) : ICommandHandler<CreateFollowQuestionCommand, IResult>
    {
        public async Task<IResult> Handle(CreateFollowQuestionCommand request, CancellationToken cancellationToken)
        {
            var spec = new FollowQuestionByIdSpec(user.Id!.Value, request.QuestionId);
            var appuser = await userRepo.GetSingleOrDefaultAsync(spec, cancellationToken);
            if (appuser == null) return Result.NotFound("用户不存在");

            var result = await followQuestionService.FollowAsync(appuser, request.QuestionId, cancellationToken);
            if (!result.IsSuccess) return result;

            await userRepo.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
