

using MySelf.Zhihu.UseCases.Answers.Commands;

namespace MySelf.Zhihu.UseCases.FunctionalTests.Answers.Commands
{
    public class DeleteAnswerLikeTest(IServiceProvider serviceProvider):TestBase(serviceProvider)
    {
        [Fact]
        public async Task ShouldSuccess()
        {
            await Sender.Send(new CreateAnswerLikeCommand(1, 1, true));
            var result = await Sender.Send(new DeleteAnswerLikeCommand(1, 1));
            result.Status.Should().Be(ResultStatus.Ok);
        }

        [Fact]
        public async Task ShouldLikeNoExists()
        {
            var result = await Sender.Send(new DeleteAnswerLikeCommand(1, 1));
            result.Status.Should().Be(ResultStatus.NotFound);
        }
    }
}
