using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using MySelf.Zhihu.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.AppUserAggregate.Events
{
    public class FollowQuestionRemovedEvent(FollowQuestion followQuestion) :BaseEvent
    {
        public FollowQuestion FollowQuestion { get; } = followQuestion;
    }
}
