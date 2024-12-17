using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Answers.Commands
{
    [Authorize]
    public record CreateAnswerCommand(int QustionId, string Content) : ICommand<Result<CreatedAnswerDto>>;


    public class CreateAnswerCommandValidator : AbstractValidator<CreateAnswerCommand>
    {
        public CreateAnswerCommandValidator()
        {
            RuleFor(command => command.QustionId).GreaterThan(0);
            RuleFor(command => command.Content)
          .NotEmpty();
        }
    }
    public class CreateAnswerCommandHandler(IRepository<Question> questions, IMapper mapper) : ICommandHandler<CreateAnswerCommand, Result<CreatedAnswerDto>>
    {
        public async Task<Result<CreatedAnswerDto>> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var question = await questions.GetByIdAsync(request.QustionId, cancellationToken);
            if(question ==null) return Result.NotFound("问题不存在");
            var answer = mapper.Map<Answer>(request);
            question.Answers.Add(answer);
            await questions.SaveChangesAsync(cancellationToken);
            return Result.Success(new CreatedAnswerDto(question.Id, answer.Id));
        }
    }
}
