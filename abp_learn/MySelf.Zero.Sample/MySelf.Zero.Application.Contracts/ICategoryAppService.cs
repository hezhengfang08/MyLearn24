using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MySelf.Zero.Application.Contracts
{
    public interface ICategoryAppService : IApplicationService
    {
        /// <summary>
        /// 获取板块
        /// </summary>
        Task<CategoryDto> GetAsync(long id);

        /// <summary>
        /// 获取板块列表
        /// </summary>
        Task<ListResultDto<CategoryDto>> GetListAsync();
    }

}
