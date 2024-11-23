using MySelf.Zero.Application.Contracts.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zero.Application.Contracts.Category
{
    public class CategoryDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool? IsLocked { get; set; }

        public int SortOrder { get; set; }

        public string Image { get; set; }

        public string Pinyin { get; set; }

        public int? ParentCategory { get; set; }

        public int TopicTimes { get; set; }

        public bool? Enabled { get; set; }

        /// <summary>
        /// 子板块集合
        /// </summary>
        public List<CategoryDto> SubCategories { get; set; }

        /// <summary>
        /// 热门主题
        /// </summary>
        public List<TopicDto> HotTopices { get; set; }
    }
}
