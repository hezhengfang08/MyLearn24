using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.HotService.Data
{
    public record QuestionStat
    {
        public int ViewCount { get; set; }

        public int AnswerCount { get; set; }

        public int LikeCount { get; set; }

        public int FollowCount { get; set; }
    }

}
