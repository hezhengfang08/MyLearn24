using MySelf.Zhihu.SharedKernel.Paging;
using MySelf.Zhihu.SharedKernel.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MySelf.Zhihu.UseCases.Contracts.Common.Interfaces
{
    public interface ISearchService
    {
        Task<SearchResult<TDoc>> ShouldMatchQuery<TDoc>(string indexs,string[] fields, string query,Pagination pagination)
            where TDoc : class;
        Task<SearchResult<TDoc>> MatchQuery<TDoc>(string indices, string field, string query,
       Pagination pagination) where TDoc : class;
    }
}
