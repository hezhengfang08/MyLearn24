using MySelf.Zero.Application.Contracts;
using MySelf.Zero.Domain.Entities;
using MySelf.Zero.Domain.Repositories;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MySelf.Zero.Application
{
    public class TopicAppService : ApplicationService, ITopicAppService
    {
        private readonly ITopicRepository topicRepository;

        public TopicAppService(ITopicRepository _topicRepository)
        {
            topicRepository = _topicRepository;
        }
        public async Task<TopicDto> GetTopicAsync(long id)
        {
            var queryable = await topicRepository.GetQueryableAsync();
            var topicEntity = queryable.FirstOrDefault(t => t.Id == id);
            if (topicEntity == null)
            {
                throw new ArgumentNullException(nameof(topicEntity));
            }

            return ObjectMapper.Map<TopicEntity, TopicDto>(topicEntity);
        }

        public async Task<PagedResultDto<TopicDto>> GetTopicListAsync(long categoryId, int page = 1, int limit = 10)
        {
            var result = await topicRepository.GetTopicByCategory(categoryId, page, limit);

            var topicList = ObjectMapper.Map<List<TopicEntity>, List<TopicDto>>(result.Item2);

            return new PagedResultDto<TopicDto>(result.Item1, topicList.AsReadOnly());
        }
    }
}
