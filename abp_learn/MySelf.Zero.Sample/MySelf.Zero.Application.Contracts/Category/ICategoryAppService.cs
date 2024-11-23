using MySelf.Zero.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MySelf.Zero.Application.Contracts.Category
{
    public interface ICategoryAppService : IApplicationService
    {
        Task<bool> AnyAsync();

        /// <summary>
        /// xlsx数据导入
        /// </summary>
        Task ImportAsync(IEnumerable<CategoryImportDto> importDtos);

        /// <summary>
        /// 获取板块
        /// </summary>
        Task<ApiResponse<CategoryDto>> GetAsync(long id);

        /// <summary>
        /// 获取板块列表
        /// </summary>
        Task<ApiResponse<ListResultDto<CategoryDto>>> GetListAsync();
    }

}
