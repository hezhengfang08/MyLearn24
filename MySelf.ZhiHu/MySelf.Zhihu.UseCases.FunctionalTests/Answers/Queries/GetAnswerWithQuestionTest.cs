

using MySelf.Zhihu.UseCases.Answers.Queries;

namespace MySelf.Zhihu.UseCases.FunctionalTests.Answers.Queries
{
    public class GetAnswerWithQuestionTest(IServiceProvider serviceProvider) : TestBase(serviceProvider)
    {
        [Fact]
        public async Task ShouldSuccess()
        {
            var result = await Sender.Send(new GetAnswerWithQuestionQuery(1, 1));
            result.Value.Should().NotBeNull();
            result.Status.Should().Be(ResultStatus.Ok);
        }

        [Fact]
        public async Task ShouldAnswerNoExists()
        {
            var result = await Sender.Send(new GetAnswerWithQuestionQuery(1, 99));
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain("回答不存在");
        }
    }
}
