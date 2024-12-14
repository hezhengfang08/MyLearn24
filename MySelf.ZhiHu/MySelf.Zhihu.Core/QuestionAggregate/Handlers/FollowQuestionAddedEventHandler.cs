using MediatR;
using MySelf.Zhihu.Core.AppUserAggregate.Events;
using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using MySelf.Zhihu.SharedKernel.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.QuestionAggregate.Handlers
{
    public class FollowQuestionAddedEventHandler(IRepository<Question> questions) : INotificationHandler<FollowQuestionAddedEvent>

    {
        public async Task Handle(FollowQuestionAddedEvent notification, CancellationToken cancellationToken)
        {
            var question = await questions.GetByIdAsync(notification.FollowQuestion.QuestionId, cancellationToken);
            if (question == null) return;
            question.FollowerCount += 1;
            await questions.SaveChangesAsync(cancellationToken);
        }
    }
}
