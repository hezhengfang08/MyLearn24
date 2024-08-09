using System.Collections.Concurrent;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Zhaoxi.Forum.Application.Contracts.Category;
using Zhaoxi.Forum.Domain.Category;
using Zhaoxi.Forum.Domain.Topic;

namespace Zhaoxi.Forum.Application;

public class CategoryAppService : ApplicationService, ICategoryAppService
{
    private readonly ICategoryRepositroy _categoryRepositroy;
    private readonly ITopicRepository _topicRepository;

    public async Task<bool> AnyAsync()
    {
        return await _categoryRepositroy.GetCountAsync() > 0;
    }
    public CategoryAppService(ICategoryRepositroy categoryRepositroy, ITopicRepository topicRepository)
    {
        _categoryRepositroy = categoryRepositroy;
        _topicRepository = topicRepository;
    }
    public async Task ImportAsync(IEnumerable<CategoryImportDto> importDtos)
    {
        var categories = ObjectMapper.Map<IEnumerable<CategoryImportDto>,
            IEnumerable<CategoryEntity>>(importDtos);

        await _categoryRepositroy.InsertManyAsync(categories);
    }
    public async Task<List<CategoryEntity>> GetListOfIdArrayAsync(long[] userIds)
    {
        return (await _categoryRepositroy.GetQueryableAsync()).Where(t => userIds.Contains(t.Id)).ToList();
    }
    public async Task<CategoryDto> GetAsync(long id)
    {
        var categoryQueryable = await _categoryRepositroy.GetQueryableAsync();
        var topicQueryable = await _topicRepository.GetQueryableAsync();

        var category = await _categoryRepositroy.GetAsync(id);
        var categoryDto = ObjectMapper.Map<CategoryEntity, CategoryDto>(category);

        // 自身和子板块主题数
        var categoryIdArray = GetSubCategoryIds(categoryQueryable, id);
        var topicTimes = topicQueryable.Count(t => categoryIdArray.Contains(t.Category.Id));
        categoryDto.TopicTimes = topicTimes;

        return categoryDto;
    }

    public async Task<ListResultDto<CategoryDto>> GetListAsync()
    {
        var categoryQueryable = await _categoryRepositroy.GetQueryableAsync();
        var topicQueryable = await _topicRepository.GetQueryableAsync();

        var parentCategoryList = await _categoryRepositroy.GetListAsync(c =>
            !c.ParentCategory.HasValue || c.ParentCategory == 0);
        var parentCategoryIdArray = parentCategoryList.Select(c => c.Id).ToList();
        var parentCategoryDtoList = ObjectMapper.Map<List<CategoryEntity>, List<CategoryDto>>(parentCategoryList);

        var stackIds = new ConcurrentStack<long>();
        parentCategoryIdArray.ForEach(parentCategoryId => stackIds.Push(parentCategoryId));
        while (stackIds.TryPop(out long categoryId))
        {
            var subCategories = categoryQueryable.Where(c => c.ParentCategory == categoryId).ToList();
            if (subCategories.Any())
            {
                var curCategoryDto = parentCategoryDtoList.Single(c => c.Id == categoryId);
                var subDtoCategories = ObjectMapper.Map<List<CategoryEntity>, List<CategoryDto>>(subCategories);
                curCategoryDto.SubCategories = subDtoCategories;

                var categoryIdArray = GetSubCategoryIds(categoryQueryable, categoryId);
                var topicTimes = topicQueryable.Count(t => categoryIdArray.Contains(t.Category.Id));
                curCategoryDto.TopicTimes = topicTimes;

                subDtoCategories.ForEach(subCategory =>
                {
                    var cid = subCategory.Id;
                    stackIds.Push(cid);

                    var subCategoryIdArray = GetSubCategoryIds(categoryQueryable, cid);
                    var topicTimes = topicQueryable.Count(t => subCategoryIdArray.Contains(t.Category.Id));
                    subCategory.TopicTimes = topicTimes;
                });
            }
        }

        return new ListResultDto<CategoryDto>(parentCategoryDtoList);
    }

    private ConcurrentBag<long> GetSubCategoryIds(IQueryable<CategoryEntity> categoryQueryable, long parentCategoryId)
    {
        var categoryIdList = new ConcurrentBag<long>();
        var stackIds = new ConcurrentStack<long>();
        stackIds.Push(parentCategoryId);
        while (stackIds.TryPop(out long categoryId))
        {
            categoryIdList.Add(categoryId);
            var subCategoryIds = categoryQueryable
                .Where(c => c.ParentCategory == categoryId)
                .Select(c => c.Id).ToArray();
            if (subCategoryIds.Any())
            {
                foreach (var item in subCategoryIds)
                {
                    stackIds.Push(item);
                }
            }
        }

        return categoryIdList;
    }
}