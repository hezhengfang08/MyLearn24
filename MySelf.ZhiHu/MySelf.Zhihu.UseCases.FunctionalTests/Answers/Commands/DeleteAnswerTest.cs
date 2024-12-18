

using MySelf.Zhihu.UseCases.Answers.Commands;

namespace MySelf.Zhihu.UseCases.FunctionalTests.Answers.Commands
{
    public class DeleteAnswerTest(IServiceProvider serviceProvider) : TestBase(serviceProvider)
    {
        [Fact]
        public async Task ShouldSuccess()
        {
            var result = await Sender.Send(new DeleteAnswerCommand(1, 1));
            result.Status.Should().Be(ResultStatus.Ok);
        }

        [Fact]
        public async Task ShouldQuestionNoExists()
        {
            var result = await Sender.Send(new DeleteAnswerCommand(99, 1));
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain("问题不存在");
        }

        [Fact]
        public async Task ShouldAnswerNoExists()
        {
            var result = await Sender.Send(new DeleteAnswerCommand(1, 99));
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain("回答不存在");
        }
    }
}
