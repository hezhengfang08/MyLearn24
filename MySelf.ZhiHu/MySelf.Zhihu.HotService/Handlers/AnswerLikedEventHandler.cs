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
    internal class AnswerLikedEventHandler(QuestionStatManager questionStatManager) : INotificationHandler<AnswerLikedEvent>
    {
        public Task Handle(AnswerLikedEvent notification, CancellationToken cancellationToken)
        {
            questionStatManager.AddLikeCount(notification.QuestionId);
            return Task.CompletedTask;
        }
    }
}
