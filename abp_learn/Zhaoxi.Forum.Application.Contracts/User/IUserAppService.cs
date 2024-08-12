using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Zhaoxi.Forum.Application.Contracts.User
{
    public interface IUserAppService : IApplicationService
    {
        Task<bool> AnyAsync();

        /// <summary>
        /// xlsx数据导入
        /// </summary>
        Task ImportAsync(IEnumerable<UserImportDto> importDtos);
    }
}
