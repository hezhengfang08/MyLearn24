using MediatR;
using MySelf.Zhihu.Core.AppUserAggregate.Events;
using MySelf.Zhihu.HotService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.HotService.Handlers
{
    public class QuestionFollowedEventHandler(QuestionStatManager questionStatManager)
      : INotificationHandler<FollowQuestionAddedEvent>
    {
        public Task Handle(FollowQuestionAddedEvent notification, CancellationToken cancellationToken)
        {
            questionStatManager.AddFollowCount(notification.FollowQuestion.QuestionId);
            return Task.CompletedTask;
        }
    }

}
