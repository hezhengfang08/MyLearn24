using MediatR;
using MySelf.Zhihu.Core.QuestionAggregate.Events;
using MySelf.Zhihu.HotService.Data;

namespace MySelf.Zhihu.HotService.Handlers
{
    public class AnswerCreatedEventHandler(QuestionStatManager questionStatManager) : INotificationHandler<AnswerCreatedEvent>
    {
        public Task Handle(AnswerCreatedEvent notification, CancellationToken cancellationToken)
        {
            questionStatManager.AddAnswerCount(notification.QuestionId);
            return Task.CompletedTask;
        }
    }
}
