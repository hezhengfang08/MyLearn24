using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MySelf.Zero.Application.Contracts
{
    public interface ITopicAppService : IApplicationService
    {
        /// <summary>
        /// 获取详情
        /// </summary>
        Task<TopicDto> GetTopicAsync(long id);

        /// <summary>
        /// 获取模块主题列表:分页
        /// </summary>
        Task<PagedResultDto<TopicDto>> GetTopicListAsync(long categoryId, int page = 1, int limit = 10);
    }

}
