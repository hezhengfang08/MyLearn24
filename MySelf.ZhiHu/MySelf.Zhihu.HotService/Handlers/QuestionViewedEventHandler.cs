using MediatR;
using MySelf.Zhihu.Core.QuestionAggregate.Events;
using MySelf.Zhihu.HotService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.HotService.Handlers
{
    public class QuestionViewedEventHandler(QuestionStatManager questionStatManager)
    : INotificationHandler<QuestionViewedEvent>
    {
        public Task Handle(QuestionViewedEvent notification, CancellationToken cancellationToken)
        {
            questionStatManager.AddViewCount(notification.QuestionId);
            return Task.CompletedTask;
        }
    }
}
