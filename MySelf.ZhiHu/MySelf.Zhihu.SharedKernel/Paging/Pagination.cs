using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.SharedKernel.Paging
{
    public class Pagination
    {
        public Pagination() {
            PageNumber = 1;
            PageSize = 10;
        }
        public Pagination(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
        }
        private const int MaxPageSize = 100;

        public int PageNumber { get; }

        public int PageSize { get; }
    }
}
