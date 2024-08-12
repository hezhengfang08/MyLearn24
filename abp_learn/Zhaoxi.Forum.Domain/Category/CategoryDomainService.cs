using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Zhaoxi.Forum.Domain.Topic;

namespace Zhaoxi.Forum.Domain.Category
{
    public class CategoryDomainService : DomainService
    {
        private readonly ICategoryRepositroy _categoryRepositroy;
        private readonly ITopicRepository _topicRepository;

        public CategoryDomainService(ICategoryRepositroy categoryRepositroy, ITopicRepository topicRepository)
        {
            _categoryRepositroy = categoryRepositroy;
            _topicRepository = topicRepository;
        }

        /// <summary>
        /// 获取自身和子板块主题数
        /// </summary>
        public async Task<Tuple<CategoryEntity, int>> GetAsync(long categoryId)
        {
            var categoryQueryable = await _categoryRepositroy.GetQueryableAsync();
            var topicQueryable = await _topicRepository.GetQueryableAsync();
            var category = await _categoryRepositroy.GetAsync(c => c.Id == categoryId);

            // 自身和子板块主题数
            var categoryIdArray = GetSubCategoryIds(categoryQueryable, categoryId);
            var topicTimes = topicQueryable.Count(t => categoryIdArray.Contains(t.Category.Id));

            return new Tuple<CategoryEntity, int>(category, topicTimes);
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
