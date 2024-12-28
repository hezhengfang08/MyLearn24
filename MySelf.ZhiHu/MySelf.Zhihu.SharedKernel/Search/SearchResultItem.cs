using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.SharedKernel.Search
{
    public class SearchResultItem<TDoc> where TDoc : class
    {
        public string Index { get; set; }
        public double? Score { get; set; }
        public TDoc? Source { get; set; }
        public IReadOnlyDictionary<string, IReadOnlyCollection<string>>? HighLight { get; set; }
    }
}
