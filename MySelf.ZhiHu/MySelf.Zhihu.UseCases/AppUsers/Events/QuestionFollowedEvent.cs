using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.AppUsers.Events
{
    public record QuestionFollowedEvent(int QuestionId) : INotification;

}
