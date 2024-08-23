using FluentValidation;
using MySelf.Zero.Application.Contracts.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zero.Application.Validator
{
    public class SendTopicInputValidator : AbstractValidator<SendTopicInput>
    {
        public SendTopicInputValidator()
        {
            RuleFor(m => m.CategoryId).GreaterThan(0)
                .WithName("板块").WithMessage("{PropertyName}为必选项！");

            RuleFor(m => m.UserId).GreaterThan(0)
                .WithName("用户").WithMessage("{PropertyName}为必选项！");

            RuleFor(m => m.TopicName)
                .NotEmpty().WithName("主题名").WithMessage("{PropertyName}为必填项！")
                .Length(6, 50).WithName("主题名").WithMessage("{PropertyName}长度10到50个字！");

            RuleFor(m => m.TopicContent)
                .NotEmpty().WithName("主题内容").WithMessage("{PropertyName}为必填项！")
                .Length(10, 2000).WithName("主题内容").WithMessage("{PropertyName}长度10到2000个字！");
        }
    }

}
