using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using MySelf.Zhihu.Core.AppUserAggregate.Specifications;
using MySelf.Zhihu.SharedKernel.Messaging;
using MySelf.Zhihu.SharedKernel.Result;
using MySelf.Zhihu.UseCases.Common.Attributes;
using MySelf.Zhihu.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MySelf.Zhihu.SharedKernel.Repositoy;
namespace MySelf.Zhihu.UseCases.AppUsers.Commands
{
    [Authorize]
    public record DeleteFollowQuestionCommand(int QuestionId):ICommand<IResult>;
    public class DeleteFollowQuestionCommandHandler(IRepository<AppUser> userRepository, IUser user) : ICommandHandler<DeleteFollowQuestionCommand, IResult>
    {
        public async Task<IResult> Handle(DeleteFollowQuestionCommand request, CancellationToken cancellationToken)
        {
            var spec = new FollowQuestionByIdSpec(user.Id!.Value, request.QuestionId);
            var appuser = await userRepository.GetSingleOrDefaultAsync(spec, cancellationToken);
            if (appuser == null) return Result.NotFound("用户不存在");

            appuser.RemoveFollowQuestion(request.QuestionId);

            await userRepository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
