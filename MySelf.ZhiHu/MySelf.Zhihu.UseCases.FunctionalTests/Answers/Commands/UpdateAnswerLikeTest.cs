



using MySelf.Zhihu.UseCases.Answers.Commands;

namespace MySelf.Zhihu.UseCases.FunctionalTests.Answers.Commands
{
    public class UpdateAnswerLikeTest(IServiceProvider serviceProvider) : TestBase(serviceProvider)
    {

        [Fact]
        public async Task ShouldSuccess()
        {
            await Sender.Send(new CreateAnswerLikeCommand(1, 1, true));
            var result = await Sender.Send(new UpdateAnswerLikeCommand(1, 1, false));
            result.Status.Should().Be(ResultStatus.Ok);

            var like = DbContext.AnswerLikes.First(like => like.AnswerId == 1);
            like.IsLike.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldLikeUnchange()
        {
            await Sender.Send(new CreateAnswerLikeCommand(1, 1, true));
            var result = await Sender.Send(new UpdateAnswerLikeCommand(1, 1, true));
            result.Status.Should().Be(ResultStatus.Error);
            result.Errors.Should().Contain("已赞或已踩");
        }
    }
}
