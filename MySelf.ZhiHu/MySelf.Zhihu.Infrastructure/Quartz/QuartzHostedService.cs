using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Infrastructure.Quartz
{
    public class QuartzHostedService(ISchedulerFactory schedulerFactory,IJobFactory jobFactory):IHostedService
    {
        private IReadOnlyList<IScheduler>? AllSchedulers { get; } = schedulerFactory.GetAllSchedulers().Result;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (AllSchedulers == null) return;

            foreach (var scheduler in AllSchedulers)
            {
                scheduler.JobFactory = jobFactory;
                scheduler.AddJobSchedule();
                await scheduler.StartDelayed(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (AllSchedulers == null) return;
            foreach (var scheduler in AllSchedulers)
            {
                await scheduler.Shutdown(cancellationToken);
            }
        }
    }
}
