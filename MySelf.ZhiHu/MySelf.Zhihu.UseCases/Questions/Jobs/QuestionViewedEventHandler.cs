using MySelf.Zhihu.Core.QuestionAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Questions.Jobs
{
    public class QuestionViewedEventHandler(QuestionViewCountService questionViewCountService)
        : INotificationHandler<QuestionViewedEvent>
    {
        public Task Handle(QuestionViewedEvent notification, CancellationToken cancellationToken)
        {
            questionViewCountService.Add(notification.QuestionId);
            return Task.CompletedTask;
        }
    }
}
