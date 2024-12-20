using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Questions.Jobs
{
    public class QuestionViewCountService
    {
        private readonly object _lock = new();
        private Dictionary<int, int> Item { get; } = new();

        public void Add(int id, int count = 1)
        {
            lock (_lock)
            {
                if (!Item.TryAdd(id, count)) Item[id] += count;
            }
        }

        public Dictionary<int, int>? GetAndReset()
        {
            if (Item.Count == 0) return null;
            lock (_lock)
            {
                var result = new Dictionary<int, int>(Item);
                Item.Clear();
                return result;
            }
        }
    }
}
