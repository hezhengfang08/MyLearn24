using MySelf.Zero.Application.Contracts.Category;
using MySelf.Zero.Application.Contracts.Topic;
using MySelf.Zero.Domain.Entities;
using MySelf.Zero.Domain.Repositories;
using MySelf.Zero.Domain.Services;
using MySelf.Zero.Domain.Shared;
using System.Collections.Concurrent;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MySelf.Zero.Application
{
    public class CategoryAppService : ApplicationService, ICategoryAppService
    {
        private readonly ICategoryRepositroy categoryRepositroy;
        private readonly CategoryDomainService categoryDomainService;
        private readonly ITopicRepository topicRepository;

        public CategoryAppService(ICategoryRepositroy categoryRepositroy,
            CategoryDomainService categoryDomainService,
            ITopicRepository topicRepository)
        {
            this.categoryRepositroy = categoryRepositroy;
            this.categoryDomainService = categoryDomainService;
            this.topicRepository = topicRepository;
        }

        public async Task<bool> AnyAsync()
        {
            var count = await categoryRepositroy.GetCountAsync();
            return count > 0;
        }

        public async Task ImportAsync(IEnumerable<CategoryImportDto> importDtos)
        {
            var categories = ObjectMapper.Map<IEnumerable<CategoryImportDto>,
                IEnumerable<CategoryEntity>>(importDtos);

            await categoryRepositroy.InsertManyAsync(categories);
        }

        public async Task<ApiResponse<CategoryDto>> GetAsync(long id)
        {
            var response = new ApiResponse<CategoryDto>();

            var tupleResult = await categoryDomainService.GetAsync(id);
            var categoryDto = ObjectMapper.Map<CategoryEntity, CategoryDto>(tupleResult.Item1);
            categoryDto.TopicTimes = tupleResult.Item2;

            response.Result = categoryDto;

            return response;
        }

        public async Task<ApiResponse<ListResultDto<CategoryDto>>> GetListAsync()
        {
            var response = new ApiResponse<ListResultDto<CategoryDto>>();
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

                    subDtoCategories.ForEach(async subCategory =>
                    {
                        var cid = subCategory.Id;
                        stackIds.Push(cid);

                        var hotTopices = await topicRepository.GetHotTopicsAsync(cid);
                        subCategory.HotTopices = ObjectMapper.Map<List<TopicEntity>, List<TopicDto>>(hotTopices);

                        var subCategoryIdArray = GetSubCategoryIds(categoryQueryable, cid);
                        var topicTimes = topicQueryable.Count(t => subCategoryIdArray.Contains(t.Category.Id));
                        subCategory.TopicTimes = topicTimes;
                    });
                }
            }
            response.Result = new ListResultDto<CategoryDto>(parentCategoryDtoList);
            return response;
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
