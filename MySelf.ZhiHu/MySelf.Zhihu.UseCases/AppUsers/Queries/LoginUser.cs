using MySelf.Zhihu.SharedKernel.Messaging;
using MySelf.Zhihu.SharedKernel.Result;
using MySelf.Zhihu.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.AppUsers.Queries
{

    public record LoginUserQuery(string Username, string Password) : IQuery<Result<TokenDto>>;
    public class LoginUserQueryHandler
    (IIdentityService identityService) : IQueryHandler<LoginUserQuery, Result<TokenDto>>
    {
        public async Task<Result<TokenDto>> Handle(LoginUserQuery command, CancellationToken cancellationToken)
        {
            var result = await identityService.GetAccessTokenAsync(command.Username, command.Password);

            if (result.IsSuccess)
            {
                var token = new TokenDto(result.Value!);
                return Result.Success(token);
            }

            return Result.From(result);
        }
    }
}
