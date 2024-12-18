using MySelf.Zhihu.HotService.Data;
using StackExchange.Redis;


namespace MySelf.Zhihu.HotService.Core
{
    public class HotRankManager(IConnectionMultiplexer redis)
    {
        private readonly IDatabase  _db = redis.GetDatabase();
        private const int FollowWeight = 100;
        private const int AnswerWeight = 20;
        private const int LikeWeight = 1;
        private const int ViewWeight = 1;
        private static int CalcHeatValue(QuestionStat stat)
        {
            return stat.FollowCount * FollowWeight
                   + stat.ViewCount * ViewWeight
                   + stat.AnswerCount * AnswerWeight
                   + stat.LikeCount * LikeWeight;
        }
        public async Task CreateHotRankAsync(Dictionary<int, QuestionStat> questionStats)
        {
            // id:heatvalue
            // 有序集合==》SortedSet==>zset
            var sortedSetEntries = questionStats
                .Select(stat => new SortedSetEntry(stat.Key, CalcHeatValue(stat.Value)))
                .ToArray();
            // ZADD
            await _db.SortedSetAddAsync(RedisConstant.HotRanking, sortedSetEntries);
        }
        public async Task UpdateHotRankAsync(Dictionary<int, QuestionStat> questionStats)
        {
            var batch = _db.CreateBatch();
            var batchTasks = new List<Task>();

            foreach (var stat in questionStats)
            {
                var heatValue = CalcHeatValue(stat.Value);
                if (heatValue == 0) continue;

                batchTasks.Add(
                    // zincrby 无法批量更新
                    batch.SortedSetIncrementAsync(
                        RedisConstant.HotRanking,
                        stat.Key,
                        heatValue));
            }

            if (batchTasks.Count == 0) return;
            batch.Execute();
            await Task.WhenAll(batchTasks);
        }
        public async Task ClearHotRankAsync()
        {
            await _db.KeyDeleteAsync(RedisConstant.HotRanking);
        }
    }
}
