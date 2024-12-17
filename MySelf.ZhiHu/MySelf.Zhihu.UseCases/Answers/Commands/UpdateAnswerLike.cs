using MySelf.Zhihu.Core.Data;
using MySelf.Zhihu.Core.QuestionAggregate.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Answers.Commands
{
    public record UpdateAnswerLikeCommand(int AnswerId, bool IsLike) : ICommand<Result>;
    public class UpdateAnswerLikeCommandValidator : AbstractValidator<UpdateAnswerLikeCommand>
    {
        public UpdateAnswerLikeCommandValidator()
        {
            RuleFor(command => command.AnswerId).GreaterThan(0);
        }
    }
    public class UpdateAnswerLikeCommandHandler(IAnswerRepositoy answers, IUser user)
        : ICommandHandler<UpdateAnswerLikeCommand, Result>
    {
        public async Task<Result> Handle(UpdateAnswerLikeCommand request, CancellationToken cancellationToken)
        {
            var spec = new AnswerByIdWithLikeByUserIdSpec(request.AnswerId, user.Id!.Value);
            var answer = await answers.GetAnswerByIdWithLikeByUserIdAsync(spec, cancellationToken);
            if (answer == null) return Result.NotFound("回答不存在");

            var result = answer.UpdateLike(user.Id!.Value, request.IsLike);

            if (!result.IsSuccess) return result;

            await answers.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
