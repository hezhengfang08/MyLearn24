﻿using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Zhaoxi.Forum.Application.Contracts;

public interface ITopicAppService : IApplicationService
{
    Task<bool> AnyAsync();
    /// <summary>
    /// 获取详情
    /// </summary>
    Task<TopicDto> GetTopicAsync(long id);

    /// <summary>
    /// 获取模块主题列表:分页
    /// </summary>
    Task<PagedResultDto<TopicDto>> GetTopicListAsync(long categoryId, int page = 1, int limit = 10);

    /// <summary>
    /// xlsx数据导入
    /// </summary>
    Task ImportAsync(IEnumerable<TopicImportDto> importDtos);
}
