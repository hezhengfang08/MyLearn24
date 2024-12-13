using FluentAssertions;
using MySelf.Zhihu.SharedKernel.Result;
using MySelf.Zhihu.UseCases.AppUsers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.FunctionalTests.AppUsers.Commands
{
    public class CreateFollowQuestionTest(IServiceProvider serviceProvider) : TestBase(serviceProvider)
    {
        [Fact]
        public async Task ShouldSuccess()
        {
            var result = await Sender.Send(new CreateFollowQuestionCommand(1));

            result.IsSuccess.Should().Be(true);
            DbContext.FollowQuestions
                .Count(f => f.UserId == CurrentUser.Id).Should().Be(1);
        }

        [Fact]
        public async Task ShouldQuestionNoExists()
        {
            var result = await Sender.Send(new CreateFollowQuestionCommand(99));

            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain("关注问题不存在");
        }

        [Fact]
        public async Task ShouldQuestionFollowed()
        {
            await Sender.Send(new CreateFollowQuestionCommand(1));
            var result = await Sender.Send(new CreateFollowQuestionCommand(1));
            result.Status.Should().Be(ResultStatus.Invalid);
            result.Errors.Should().Contain("问题已关注");
        }
    }
}
