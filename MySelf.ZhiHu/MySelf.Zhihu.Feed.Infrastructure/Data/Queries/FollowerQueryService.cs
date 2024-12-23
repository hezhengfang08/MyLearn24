using Microsoft.EntityFrameworkCore;
using MySelf.Zhihu.Feed.Infrastructure.Data.Contexts;
using MySelf.Zhihu.Feed.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Feed.Infrastructure.Data.Queries
{
    public class FollowUserQueryService(AppDbContext appDb) : IFollowUserQueryService
    {
        public async Task<int[]> GetFollowerIdsAsync(int userId)
        {
            var queryalbe = appDb.Database
                .SqlQuery<int>($"SELECT FollowerId FROM followusers WHERE FolloweeId = {userId}");

            return await queryalbe.ToArrayAsync();
        }
    }

}
