

using MySelf.Zhihu.UseCases.Answers.Commands;

namespace MySelf.Zhihu.UseCases.FunctionalTests.Answers.Commands
{
    public class CreateAnswerTest(IServiceProvider serviceProvider):TestBase(serviceProvider)
    {
        [Fact]
        public async Task ShouldSuccess()
        {
            var result = await Sender.Send(new CreateAnswerCommand(1, "这是个问题的回答"));
            result.Value!.AnswerId.Should().Be(0);
            result.Status.Should().Be(ResultStatus.Ok);
        }
        [Fact]
        public async Task ShouldQuestionNoExists()
        {
            var result = await Sender.Send(new CreateAnswerCommand(99, "这是一个问题的回答"));
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain("问题不存在");
        }

        [Fact]
        public async Task ShouldAnswerIsEmpty()
        {
            var action = async () => { await Sender.Send(new CreateAnswerCommand(1, "")); };
            await action.Should().ThrowAsync<ValidationException>();
        }
    }
}
