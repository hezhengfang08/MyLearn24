using MySelf.Zhihu.SharedKernel.Paging;
using MySelf.Zhihu.UseCases.Contracts.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZiggyCreatures.Caching.Fusion;

namespace MySelf.Zhihu.Infrastructure.Cache
{
    public class CacheService<TValue>(IFusionCache cache) : ICacheService<TValue> where TValue : class
    {
        public string Key { get; set; }=typeof(TValue).ToString();

        public async ValueTask<TValue?> GetOrSetByIdAsync(int id, Func<CancellationToken, Task<TValue?>> factory)
        {
            return await cache.GetOrSetAsync<TValue?>($"{Key}:{id}",factory);
        }

        public async ValueTask<TValue?> GetOrSetByIdAsync(int fid, int sid, Func<CancellationToken, Task<TValue?>> factory)
        {
            return await cache.GetOrSetAsync<TValue?>($"{Key}:{fid}:{sid}", factory);
        }

        public async ValueTask<TValue?> GetOrSetByKeyAsync(Func<CancellationToken, Task<TValue?>> factory)
        {
            return await cache.GetOrSetAsync<TValue?>(Key, factory);
        }

        public async ValueTask<PagedList<TValue>?> GetOrSetListByPageAsync(int id, Pagination pagination, Func<CancellationToken, Task<PagedList<TValue>?>> factory)
        {
            var key = $"{Key}:{id}:{pagination.PageNumber}-{pagination.PageSize}";

            return await cache.GetOrSetAsync<PagedList<TValue>?>(key, factory);
        }
    }
}
