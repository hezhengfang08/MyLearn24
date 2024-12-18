using MySelf.Zhihu.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.QuestionAggregate.Events
{
    public class AnswerLikedEvent(int questionId) : BaseEvent
    {
        public int QuestionId { get; set; } = questionId;
    }

}
