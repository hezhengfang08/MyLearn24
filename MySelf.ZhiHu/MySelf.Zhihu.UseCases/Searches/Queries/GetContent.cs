using MySelf.Zhihu.SharedKernel.Paging;
using MySelf.Zhihu.SharedKernel.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Searches.Queries
{
    public record GetContentQuery(string Query, Pagination Pagination) : IQuery<Result<SearchResult<SearchContentDto>>>;

    public class GetContentQueryValidator : AbstractValidator<GetContentQuery>
    {
        public GetContentQueryValidator()
        {
            RuleFor(command => command.Query).NotEmpty();
        }
    }

    public class GetContentHandler(ISearchService searchService)
        : IQueryHandler<GetContentQuery, Result<SearchResult<SearchContentDto>>>
    {
        public async Task<Result<SearchResult<SearchContentDto>>> Handle(GetContentQuery request,
            CancellationToken cancellationToken)
        {
            const string indices = "questions,answers";
            string[] fields = ["title", "content", "description"];

            var result =
                await searchService.ShouldMatchQuery<SearchContentDto>(indices, fields,
                    request.Query, request.Pagination);

            return Result.Success(result);
        }
    }

}
