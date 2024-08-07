using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Zhaoxi.Forum.Domain.Entities;

namespace Zhaoxi.Forum.Application.Contracts;

public interface ICategoryAppService : IApplicationService
{
    Task<bool> AnyAsync();

    /// <summary>
    /// 获取板块
    /// </summary>
    Task<CategoryDto> GetAsync(long id);

    /// <summary>
    /// 获取板块列表
    /// </summary>
    Task<ListResultDto<CategoryDto>> GetListAsync();
    /// <summary>
    /// xlsx数据导入
    /// </summary>
    Task ImportAsync(IEnumerable<CategoryImportDto> importDtos);
    Task<List<CategoryEntity>> GetListOfIdArrayAsync(long[] userIds);
}
