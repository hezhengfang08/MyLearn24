using Magicodes.ExporterAndImporter.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zero.Application.Contracts.Category
{
    /// 板块导入数据模型
    /// </summary>
    public class CategoryImportDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        [ImporterHeader(Name = "编号")]
        public long No { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [ImporterHeader(Name = "名称")]
        [MaxLength(50, ErrorMessage = "名称超出最大限制,请修改!")]
        public string Name { get; set; }

        /// <summary>
        /// 描叙
        /// </summary>
        [ImporterHeader(Name = "描叙")]
        public string Description { get; set; }

        /// <summary>
        /// 板块排序
        /// </summary>
        [ImporterHeader(Name = "板块排序")]
        public int SortOrder { get; set; }

        /// <summary>
        /// 图像
        /// </summary>
        [ImporterHeader(Name = "图像")]
        public string Image { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        [ImporterHeader(Name = "拼音")]
        public string Pinyin { get; set; }

        /// <summary>
        /// 父板块
        /// </summary>
        [ImporterHeader(Name = "父板块")]
        public long ParentCategory { get; set; }
    }
}
