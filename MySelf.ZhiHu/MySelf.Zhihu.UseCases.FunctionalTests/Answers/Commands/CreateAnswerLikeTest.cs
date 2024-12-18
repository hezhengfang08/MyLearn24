

using MySelf.Zhihu.UseCases.Answers.Commands;

namespace MySelf.Zhihu.UseCases.FunctionalTests.Answers.Commands
{

    public class CreateAnswerLikeTest(IServiceProvider  serviceProvider):TestBase(serviceProvider)
    {
        [Fact]
        public async Task ShouldSuccess()
        {
            var result = await Sender.Send(new CreateAnswerLikeCommand(1, 1, true));
            result.Status.Should().Be(ResultStatus.Ok);
            var like = DbContext.AnswerLikes.FirstOrDefault(like => like.AnswerId == 1);
            like.Should().NotBeNull();
            like!.IsLike.Should().BeTrue();

        }
        [Fact]
        public async Task ShouldAnswerNotExists()
        {
            var result = await Sender.Send(new CreateAnswerLikeCommand(1, 9999, true));
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain("回答不存在");
        }
        [Fact]
        public async Task ShouldAnswerLiked()
        {
            await Sender.Send(new CreateAnswerLikeCommand(1, 1, true));
            var result = await Sender.Send(new CreateAnswerLikeCommand(1, 1, true));
            result.Status.Should().Be(ResultStatus.Error);
            result.Errors.Should().Contain("已赞或已踩");
        }
    }
}
