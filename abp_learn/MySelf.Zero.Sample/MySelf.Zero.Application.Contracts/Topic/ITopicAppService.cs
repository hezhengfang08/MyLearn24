using MySelf.Zero.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MySelf.Zero.Application.Contracts.Topic
{
    public interface ITopicAppService : IApplicationService
    {
        Task<bool> AnyAsync();

        /// <summary>
        /// xlsx数据导入
        /// </summary>
        Task ImportAsync(IEnumerable<TopicImportDto> importDtos);

        /// <summary>
        /// 获取详情
        /// </summary>
        Task<ApiResponse<TopicDto>> GetTopicAsync(long id);

        /// <summary>
        /// 获取模块主题列表:分页
        /// </summary>
        Task<ApiResponse<PagedResultDto<TopicDto>>> GetTopicListAsync(long categoryId, int page = 1, int limit = 10);

        /// <summary>
        /// 获取回复列表:分页
        /// </summary>
        Task<ApiResponse<PagedResultDto<PostsDto>>> GetPostsAsync(long topicId, int page = 1, int limit = 10);

        /// <summary>
        /// 发帖
        /// </summary>
        Task<ApiResponse> SendTopicAsync(SendTopicInput input);

        /// <summary>
        /// 回帖
        /// </summary>
        Task<ApiResponse> ReplyPostsAsync(ReplyPostsInput input);
    }
}
