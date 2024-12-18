using MySelf.Zhihu.Core.Data;
using MySelf.Zhihu.Core.QuestionAggregate.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Answers.Commands
{

    public record CreateAnswerLikeCommand(int QuestionId, int AnswerId, bool IsLike) : ICommand<Result>;

    public class CreateAnswerLikeCommandValidator : AbstractValidator<CreateAnswerLikeCommand>
    {
        public CreateAnswerLikeCommandValidator()
        {
            RuleFor(command => command.QuestionId).GreaterThan(0);
            RuleFor(command => command.AnswerId).GreaterThan(0);
        }
    }
    public class CreateAnswerLikeCommandHandler(IAnswerRepositoy answers, IUser user)
     : ICommandHandler<CreateAnswerLikeCommand, Result>
    {
        public async Task<Result> Handle(CreateAnswerLikeCommand request, CancellationToken cancellationToken)
        {
            var spec = new AnswerByIdWithLikeByUserIdSpec(request.QuestionId, request.AnswerId, user.Id!.Value);
            var answer = await answers.GetAnswerByIdWithLikeByUserIdAsync(spec, cancellationToken);
            if (answer == null) return Result.NotFound("回答不存在");

            var result = answer.AddLike(user.Id!.Value, request.IsLike);

            if (!result.IsSuccess) return result;

            await answers.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
