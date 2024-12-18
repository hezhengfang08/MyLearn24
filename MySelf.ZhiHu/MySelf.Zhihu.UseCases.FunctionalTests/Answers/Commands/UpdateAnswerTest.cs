

using Microsoft.EntityFrameworkCore;
using MySelf.Zhihu.Core.QuestionAggregate.Specifications;
using MySelf.Zhihu.Infrastructure.Data.Repositories;
using MySelf.Zhihu.UseCases.Answers.Commands;

namespace MySelf.Zhihu.UseCases.FunctionalTests.Answers.Commands
{
    public class UpdateAnswerTest(IServiceProvider serviceProvider) : TestBase(serviceProvider)
    {
        [Fact]
        public async Task ShouldSuccess()
        {
            var result = await Sender.Send(new UpdateAnswerCommand(1, 1, "这是更新的回答"));

            var spec = new AnswerByIdAndCreatedBySpec(CurrentUser.Id!.Value, 1, 1);
            var question = await SpecificationEvaluator.GetQuery(DbContext.Questions, spec).FirstOrDefaultAsync();
            var answer = question!.Answers.First(a => a.Id == 1);
            answer.Content.Should().Be("这是更新的回答");
            answer.LastModifiedBy.Should().Be(CurrentUser.Id!.Value);
            answer.LastModifiedAt.Should().BeCloseTo(DateTimeOffset.Now, TimeSpan.FromMilliseconds(50));
            result.Status.Should().Be(ResultStatus.Ok);
        }

        [Fact]
        public async Task ShouldQuestionNoExists()
        {
            var result = await Sender.Send(new UpdateAnswerCommand(99, 1, "这个问题不存在"));
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain("问题不存在");
        }

        [Fact]
        public async Task ShouldAnswerNoExists()
        {
            var result = await Sender.Send(new UpdateAnswerCommand(1, 99, "这个回答不存在"));
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain("回答不存在");
        }

        [Fact]
        public async Task ShouldAnswerIsEmpty()
        {
            var action = async () => { await Sender.Send(new UpdateAnswerCommand(1, 1, "")); };
            await action.Should().ThrowAsync<ValidationException>();
        }
    }
}
