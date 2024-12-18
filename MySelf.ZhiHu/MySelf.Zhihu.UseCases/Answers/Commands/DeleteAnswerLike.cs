using MySelf.Zhihu.Core.Data;
using MySelf.Zhihu.Core.QuestionAggregate.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Answers.Commands
{
    public record DeleteAnswerLikeCommand(int QuestionId,int AnswerId) : ICommand<Result>;
    public class DeleteAnswerLikeCommandValidator : AbstractValidator<DeleteAnswerLikeCommand>
    {
        public DeleteAnswerLikeCommandValidator()
        {
            RuleFor(command => command.AnswerId).GreaterThan(0);
        }
    }
    public class DeleteAnswerLikeCommandHandler(IAnswerRepositoy answers, IUser user)
    : ICommandHandler<DeleteAnswerLikeCommand, Result>
    {
        public async Task<Result> Handle(DeleteAnswerLikeCommand request, CancellationToken cancellationToken)
        {
            var spec = new AnswerByIdWithLikeByUserIdSpec(request.QuestionId, request.AnswerId, user.Id!.Value);
            var answer = await answers.GetAnswerByIdWithLikeByUserIdAsync(spec, cancellationToken);
            if (answer == null) return Result.NotFound("回答不存在");

            answer.RemoveLike(user.Id!.Value);

            await answers.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
