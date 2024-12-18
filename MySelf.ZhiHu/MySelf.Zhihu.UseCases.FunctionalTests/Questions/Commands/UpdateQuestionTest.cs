﻿

using Microsoft.EntityFrameworkCore;
using MySelf.Zhihu.UseCases.Questions.Commands;

namespace MySelf.Zhihu.UseCases.FunctionalTests.Questions.Commands
{
    public class UpdateQuestionTest(IServiceProvider serviceProvider) : TestBase(serviceProvider)
    {
        [Fact]
        public async Task ShouldSuccess()
        {
            var result = await Sender.Send(new UpdateQuestionCommand(1, "这是一个问题吗？", "这是一个问题的描述"));

            var question = await DbContext.Questions.FirstAsync(q => q.Id == 1);
            question.Title.Should().Be("这是一个问题吗？");
            question.Description.Should().Be("这是一个问题的描述");
            question.LastModifiedBy.Should().Be(1);
            question.LastModifiedAt.Should().BeCloseTo(DateTimeOffset.Now, TimeSpan.FromMilliseconds(50));

            result.Status.Should().Be(ResultStatus.Ok);
        }

        [Fact]
        public async Task ShouldQuestionNoExists()
        {
            var result = await Sender.Send(new UpdateQuestionCommand(99, "这是一个问题吗？", "这是一个问题的描述"));
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain("问题不存在");
        }

        [Fact]
        public async Task ShouldValidationExceptionForNoQuestionMark()
        {
            var action = async () => { await Sender.Send(new UpdateQuestionCommand(1, "这不是一个问题", "这是一个问题的描述")); };
            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task ShouldValidationExceptionForLength()
        {
            var action = async () => { await Sender.Send(new UpdateQuestionCommand(1, "这是问题？", "这是一个问题的描述")); };
            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task ShouldValidationExceptionForEmpty()
        {
            var action = async () => { await Sender.Send(new UpdateQuestionCommand(1, "", "")); };
            await action.Should().ThrowAsync<ValidationException>();
        }
    }
}
