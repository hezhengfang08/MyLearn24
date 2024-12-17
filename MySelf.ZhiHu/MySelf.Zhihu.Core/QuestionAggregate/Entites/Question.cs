using MySelf.Zhihu.Core.Common;
using MySelf.Zhihu.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.QuestionAggregate.Entites
{
    public class Question : AuditWithUserEntity, IAggregateRoot
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int ViewCount { get; private set; }

        public int FollowerCount { get; set; }

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();

        public void AddViewCount(int count)
        {
            ViewCount += count;
        }
    }
}
