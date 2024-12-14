using FluentValidation;
using MySelf.Zhihu.Core.Data;
using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using MySelf.Zhihu.Core.QuestionAggregate.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Questions.Commands
{
    [Authorize]
    public record UpdateQuestionCommand(int Id, string Title, string? Description) : ICommand<IResult>;
    public class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
    {
        public UpdateQuestionCommandValidator()
        {
            RuleFor(command => command.Id)
                .GreaterThan(0);

            RuleFor(command => command.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(6, DataSchemaConstants.DefaultQuestionTitleLength)
                .Must(t => t.EndsWith('?') || t.EndsWith('？')).WithMessage("问题标题必须以问号结尾");

            RuleFor(command => command.Description)
                .MaximumLength(DataSchemaConstants.DefaultDescriptionTitleLength);
        }
    }
    public class UpdateQuestionCommandHandler(
    IRepository<Question> questions,
    IUser user,
    IMapper mapper) : ICommandHandler<UpdateQuestionCommand, IResult>
    {
        public async Task<IResult> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var spec = new QuestionByCreatedBy(user.Id!.Value, request.Id);
            var question = await questions.GetSingleOrDefaultAsync(spec, cancellationToken);
            if (question == null) return Result.NotFound("问题不存在");

            mapper.Map(request, question);

            await questions.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
