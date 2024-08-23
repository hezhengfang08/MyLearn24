using FluentValidation;
using MySelf.Zero.Application.Contracts.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zero.Application.Validator
{
    public class ReplyPostsInputValidator : AbstractValidator<ReplyPostsInput>
    {
        public ReplyPostsInputValidator()
        {
            RuleFor(m => m.TopicId).GreaterThan(0)
                .WithName("主题").WithMessage("{PropertyName}为必选项！");

            RuleFor(m => m.UserId).GreaterThan(0)
                .WithName("用户").WithMessage("{PropertyName}为必选项！");

            //RuleFor(m => m.PostId).GreaterThan(0)
            //    .WithName("某一个回复").WithMessage("{PropertyName}为必选项！");

            RuleFor(m => m.Content)
                .NotEmpty().WithName("回复内容").WithMessage("{PropertyName}为必填项！")
                .Length(10, 2000).WithName("回复内容").WithMessage("{PropertyName}长度10到2000个字！");
        }
    }
}
