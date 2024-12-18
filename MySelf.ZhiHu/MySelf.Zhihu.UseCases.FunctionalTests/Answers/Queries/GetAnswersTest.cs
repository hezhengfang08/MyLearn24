

using MySelf.Zhihu.SharedKernel.Paging;
using MySelf.Zhihu.UseCases.Answers.Queries;

namespace MySelf.Zhihu.UseCases.FunctionalTests.Answers.Queries
{
    public class GetAnswersTest(IServiceProvider serviceProvider) : TestBase(serviceProvider)
    {
        [Fact]
        public async Task ShouldSuccess()
        {
            var result = await Sender.Send(new GetAnswersQuery(1, new Pagination()));
            result.Value?.Count.Should().BeLessThanOrEqualTo(10);
            result.Status.Should().Be(ResultStatus.Ok);
        }

        [Fact]
        public async Task ShouldAnswerNoExists()
        {
            var result = await Sender.Send(new GetAnswersQuery(99, new Pagination()));
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain("回答不存在");
        }
    }
}
