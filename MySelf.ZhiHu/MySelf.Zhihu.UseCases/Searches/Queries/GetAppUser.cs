using MySelf.Zhihu.SharedKernel.Paging;
using MySelf.Zhihu.SharedKernel.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Searches.Queries
{
    public record GetAppUserQuery(string Query, Pagination Pagination) : IQuery<Result<SearchResult<SearchAppUserDto>>>;

    public class GetAppUserQueryValidator : AbstractValidator<GetAppUserQuery>
    {
        public GetAppUserQueryValidator()
        {
            RuleFor(command => command.Query).NotEmpty();
        }
    }

    public class GetAppUserQueryHandler(ISearchService searchService)
        : IQueryHandler<GetAppUserQuery, Result<SearchResult<SearchAppUserDto>>>
    {
        public async Task<Result<SearchResult<SearchAppUserDto>>> Handle(GetAppUserQuery request,
            CancellationToken cancellationToken)
        {
            const string indices = "appusers";
            const string field = "nickname";
            var result =
                await searchService.MatchQuery<SearchAppUserDto>(indices, field, request.Query, request.Pagination);

            return Result.Success(result);
        } 
    }
}