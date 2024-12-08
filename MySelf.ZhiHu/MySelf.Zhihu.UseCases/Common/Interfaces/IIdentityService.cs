using MySelf.Zhihu.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<int>> CreateUserAsync(string username, string password);

        Task<Result<string>> GetAccessTokenAsync(string username, string password);
    }
}
