using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Questions.Jobs
{
    public static class QuestionViewCount
    {
        private static readonly object  Lock = new object();
        private static Dictionary<int, int> Items { get; } = new ();
        public static void Add(int id, int count = 1)
        {
            lock (Lock)
            {
                if (!Items.TryAdd(id, count)) Items[id] += count;
            }
        }

        public static Dictionary<int, int>? GetAndReset()
        {
            if (Items.Count == 0) return null;
            lock (Lock)
            {
                var result = new Dictionary<int, int>(Items);
                Items.Clear();
                return result;
            }
        }
    }
}
