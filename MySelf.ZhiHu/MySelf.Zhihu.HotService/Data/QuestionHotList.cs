using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.HotService.Data
{
    public record QuestionHotList
    {
        public int Id { get; set; }

        public int HotValue { get; set; }

        public string Title { get; set; } = null!;

        public string? Summary { get; set; }
    }
}
