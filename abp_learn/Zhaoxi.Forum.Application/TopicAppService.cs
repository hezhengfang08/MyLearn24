using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Zhaoxi.Forum.Application.Contracts.Topic;
using Zhaoxi.Forum.Domain;
using Zhaoxi.Forum.Domain.Category;
using Zhaoxi.Forum.Domain.Topic;

namespace Zhaoxi.Forum.Application;

public class TopicAppService : ApplicationService, ITopicAppService
{
    private readonly ITopicRepository _topicRepository;
    private readonly ICategoryRepositroy _categoryRepositroy;

    public TopicAppService(ITopicRepository topicRepository, ICategoryRepositroy categoryRepositroy)
    {
        _topicRepository = topicRepository;
        _categoryRepositroy = categoryRepositroy;
    }

    public async Task<bool> AnyAsync()
    {
        return await _topicRepository.GetCountAsync() > 0;
    }
    public async Task ImportAsync(IEnumerable<TopicImportDto> importDtos)
    {
        var categoryIds = importDtos.Select(d => d.CategoryId).ToArray();
        var categories = await _categoryRepositroy.GetListOfIdArrayAsync(categoryIds);
        var topics = new List<TopicEntity>(categoryIds.Length);
        foreach (var dto in importDtos)
        {
            var topic = ObjectMapper.Map<TopicImportDto, TopicEntity>(dto);
            topic.Category = categories.Single(c => c.Id == dto.CategoryId);
            topics.Add(topic);
        }

        await _topicRepository.InsertManyAsync(topics);
    }
    public async Task<TopicDto> GetTopicAsync(long id)
    {
        var queryable = await _topicRepository.GetQueryableAsync();
        var topicEntity = queryable.FirstOrDefault(t => t.Id == id);
        if (topicEntity == null)
        {
            throw new ArgumentNullException(nameof(topicEntity));
        }

        return ObjectMapper.Map<TopicEntity, TopicDto>(topicEntity);
    }

    public async Task<PagedResultDto<TopicDto>> GetTopicListAsync(long categoryId, int page = 1, int limit = 10)
    {
        var result = await _topicRepository.GetTopicByCategory(categoryId, page, limit);

        var topicList = ObjectMapper.Map<List<TopicEntity>, List<TopicDto>>(result.Item2);

        return new PagedResultDto<TopicDto>(result.Item1, topicList.AsReadOnly());
    }
}