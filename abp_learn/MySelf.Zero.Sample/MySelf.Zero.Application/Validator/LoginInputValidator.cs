using FluentValidation;
using MySelf.Zero.Application.Contracts.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MySelf.Zero.Application.Validator
{
    public class LoginInputValidator : AbstractValidator<LoginInput>
    {
        //手机号正则
        const string PatrnPhone = @"(^0{0,1}13[0-9]{9}$)|(13\d{9}$)|(14[0-9]\d{8}$)|(15[01235678-9]\d{8}$)|(17[0-9]\d{8}$)|(18[0-9]\d{8}$)";

        // 邮箱正则
        const string PatrnEmail = @"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$";

        public LoginInputValidator()
        {
            RuleFor(m => m.UserName)
                .NotEmpty().WithName("用户名").WithMessage("{PropertyName}为必填项！")
                .Length(5, 50).WithName("用户名").WithMessage("{PropertyName}长度5到50个字！");

            RuleFor(m => m.Password)
                .NotEmpty().WithName("密码").WithMessage("{PropertyName}为必填项！")
                .Length(6, 20).WithName("密码").WithMessage("{PropertyName}长度6到20个字！");

            RuleFor(m => m)
                .Custom((m, context) =>
                {
                    if (!string.IsNullOrEmpty(m.UserName))
                    {
                        var ifFlag = false;
                        var isPhone = m.UserName.Length == 11 &&
                            long.TryParse(m.UserName, out long phoneNumber);
                        if (isPhone)
                        {
                            Regex reg = new Regex(PatrnPhone, RegexOptions.Compiled);
                            if (!reg.IsMatch(m.UserName))
                            {
                                context.AddFailure("输入手机号格式不正确!");
                            }

                            ifFlag = true;
                        }
                        else if (m.UserName.Contains('@'))
                        {
                            Regex reg = new Regex(PatrnEmail, RegexOptions.Compiled);
                            if (!reg.IsMatch(m.UserName))
                            {
                                context.AddFailure("输入邮箱格式不正确!");
                            }

                            ifFlag = true;
                        }

                        if (!ifFlag)
                        {
                            context.AddFailure("输入用户名必须是手机号或者邮箱!");
                        }
                    }
                });
        }
    }

}
