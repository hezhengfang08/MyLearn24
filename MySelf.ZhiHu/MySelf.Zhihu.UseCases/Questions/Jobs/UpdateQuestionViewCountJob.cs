using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using MySelf.Zhihu.Core.QuestionAggregate.Specifications;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Questions.Jobs
{
    public class UpdateQuestionViewCountJob(IRepository<Question> questions) : IJob
    {
        public static readonly JobKey Key = new(nameof(UpdateQuestionViewCountJob), nameof(Questions));

        private const int BatchSize = 20;
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var questionViewDict = QuestionViewCount.GetAndReset();
                if (questionViewDict == null) return;

                var totalBatch = (int)Math.Ceiling((double)questionViewDict.Count / BatchSize);

                for (var i = 1; i <= totalBatch; i++)
                {
                    var batchData = questionViewDict.Skip((i - 1) * totalBatch).Take(BatchSize).ToDictionary();
                    var ids = batchData.Keys.ToArray();
                    var questionEntities = await questions.GetListAsync(new QuestionsByIdsSpec(ids));

                    if (questionEntities.Count == 0) return;

                    foreach (var questionEntity in questionEntities) questionEntity.AddViewCount(batchData[questionEntity.Id]);

                    await questions.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new JobExecutionException(ex.Message, ex, true);
            }
        }
    }
}
