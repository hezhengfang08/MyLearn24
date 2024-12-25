using MySelf.Zhihu.HotService.Data;
using MySelf.Zhihu.SharedKernel.Result;
using StackExchange.Redis;
using System.Text.Json;


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
        public async Task<List<QuestionHotList>> GetTopHotRankAsync(int top = 50)
        {
            var hotrank =
                await _db.SortedSetRangeByRankWithScoresAsync(RedisConstant.HotRanking, 0, top - 1, Order.Descending);

            var hotlist = hotrank.Select(entry => new QuestionHotList
            {
                Id = Convert.ToInt32(entry.Element),
                HotValue = Convert.ToInt32(entry.Score),
            }).ToList();
            return hotlist;
        }
        public async Task UpdateHotListAsync(List<QuestionHotList> questionLists)
        {
            await _db.StringSetAsync(RedisConstant.HotList, JsonSerializer.Serialize(questionLists));
        }
        public async Task<Result<List<QuestionHotList>>> GetHotListAsync()
        {
            var storedJson = await _db.StringGetAsync(RedisConstant.HotList);
            if (!storedJson.HasValue) return Result.NotFound();

            var result = JsonSerializer.Deserialize<List<QuestionHotList>>(storedJson.ToString());
            return Result.Success(result!);
        }
    }
}
