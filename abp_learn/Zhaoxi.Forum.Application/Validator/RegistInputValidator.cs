using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.Forum.Application.Contracts.Auth;

namespace Zhaoxi.Forum.Application.Validator
{
    public class RegistInputValidator : AbstractValidator<RegistInput>
    {
        public RegistInputValidator()
        {
            RuleFor(dto => dto.Phone)
            .NotEmpty().WithName("手机号").WithMessage("{PropertyName}为必填项！")
            .Length(11).WithName("手机号").WithMessage("{PropertyName}为11位数字！");

            RuleFor(dto => dto.Password)
                .NotEmpty().WithName("密码").WithMessage("{PropertyName}为必填项！")
                .Length(6, 20).WithName("密码").WithMessage("{PropertyName}长度6到20个字！");

            RuleFor(dto => dto.NickName)
                .NotEmpty().WithName("昵称").WithMessage("{PropertyName}为必填项！")
                .Length(1, 20).WithName("昵称").WithMessage("{PropertyName}不能超过20个字！");

            RuleFor(dto => dto.Email)
                .NotEmpty().WithName("邮箱").WithMessage("{PropertyName}为必填项！")
                .Length(6, 50).WithName("邮箱").WithMessage("{PropertyName}长度6到50个字！")
                .EmailAddress().WithName("邮箱").WithMessage("{PropertyName}格式不正确！");

            RuleFor(dto => dto.Sex)
                .InclusiveBetween(0, 1).WithName("性别").WithMessage("{PropertyName}必须是男或女！");
        }
    }
}
