using MySelf.Zero.Application.Contracts;
using MySelf.Zero.Domain.Entities;
using MySelf.Zero.Domain.Repositories;
using System.Collections.Concurrent;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MySelf.Zero.Application
{
    public class CategoryAppService : ApplicationService, ICategoryAppService
    {
        //构造函数注入
        private readonly ICategoryRepositroy categoryRepositroy;
        private readonly ITopicRepository topicRepository;
        public CategoryAppService(ICategoryRepositroy _categoryRepositroy, ITopicRepository _topicRepository)
        {
            categoryRepositroy = _categoryRepositroy;
            topicRepository = _topicRepository;
        }
        public async Task<CategoryDto> GetAsync(long id)
        {
            var categoryQueryable = await categoryRepositroy.GetQueryableAsync();
            var topicQueryable = await topicRepository.GetQueryableAsync();
            var category = await categoryRepositroy.GetAsync(id);
            var categoryDto = ObjectMapper.Map<CategoryEntity, CategoryDto>(category);

            // 自身和子板块主题数
            var categoryIdArray = GetSubCategoryIds(categoryQueryable, id);
            var topicTimes = topicQueryable.Count(t => categoryIdArray.Contains(t.Category.Id));
            categoryDto.TopicTimes = topicTimes;

            return categoryDto;
        }

        public async Task<ListResultDto<CategoryDto>> GetListAsync()
        {
            var categoryQueryable = await categoryRepositroy.GetQueryableAsync();
            var topicQueryable = await topicRepository.GetQueryableAsync();

            var parentCategoryList = await categoryRepositroy.GetListAsync(c =>
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
}
