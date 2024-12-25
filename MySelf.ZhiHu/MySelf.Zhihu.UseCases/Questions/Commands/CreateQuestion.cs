using FluentValidation;
using MySelf.Zhihu.Core.Data;
using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using MySelf.Zhihu.SharedModels;
using MySelf.Zhihu.UseCases.Contracts.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Questions.Commands
{
    [Authorize]
    public record CreateQuestionCommand(string Title, string? Description) : ICommand<Result<CreatedQuestionDto>>;
    public class CreateQuestionCommandValidator:AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator() {
            RuleFor(command => command.Title)
                  .Cascade(CascadeMode.Stop)
                  .NotEmpty()
                  .Length(6, DataSchemaConstants.DefaultQuestionTitleLength)
                   .Must(t => t.EndsWith("?") || t.EndsWith("？")).WithMessage("问题结尾必须以问号结尾！");
            RuleFor(command => command.Description)
           .MaximumLength(DataSchemaConstants.DefaultDescriptionTitleLength);
        }
    }


    public class CreateQuestionCommandHandler(
    IUser user,
    IRepository<Question> questions,
    IMapper mapper,
    IMessageBusService bus) : ICommandHandler<CreateQuestionCommand, Result<CreatedQuestionDto>>
    {
        public async Task<Result<CreatedQuestionDto>> Handle(CreateQuestionCommand request,
            CancellationToken cancellationToken)
        {
            var question = mapper.Map<Question>(request);

            question.GenerateSummary();

            questions.Add(question);

            await questions.SaveChangesAsync(cancellationToken);

            await bus.PushishAsync(new FeedCreatedEvent
            {
                FeedType = FeedType.Quesiton,
                FeedId = question.Id,
                UserId = user.Id!.Value
            });

            return Result.Success(new CreatedQuestionDto(question.Id));
        }
    }

}
