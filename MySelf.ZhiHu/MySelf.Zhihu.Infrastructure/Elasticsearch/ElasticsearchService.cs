using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Core.Search;
using Elastic.Clients.Elasticsearch.Nodes;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Clients.Elasticsearch;
using MySelf.Zhihu.SharedKernel.Paging;
using MySelf.Zhihu.SharedKernel.Search;
using MySelf.Zhihu.UseCases.Contracts.Common.Interfaces;
namespace MySelf.Zhihu.Infrastructure.Elasticsearch
{
    public class ElasticsearchService(ElasticsearchClient client) : ISearchService
    {
        public async Task<SearchResult<TDoc>> MatchQuery<TDoc>(string indices, string field, string query, Pagination pagination) where TDoc : class
        {
            var request = new SearchRequest(indices)
            {
                Query = new MatchQuery(new Field(field)) { Query = query },
                Highlight = new Highlight
                {
                    Fields = new Dictionary<Field, HighlightField>
                {
                    { new Field(field), new HighlightField() }
                }
                },
                From = (pagination.PageNumber - 1) * pagination.PageSize,
                Size = pagination.PageSize
            };

            var response = await client.SearchAsync<TDoc>(request);

            var items = new List<SearchResultItem<TDoc>>();
            items.AddRange(response.Hits.Select(hit => new SearchResultItem<TDoc>
            {
                Index = hit.Index,
                Score = hit.Score,
                Source = hit.Source,
                HighLight = hit.Highlight
            }));

            return new SearchResult<TDoc>(items, response.Total, pagination);
        }

        public async Task<SearchResult<TDoc>> ShouldMatchQuery<TDoc>(string indexs, string[] fields, string query, Pagination pagination) where TDoc : class
        {
            var request = new SearchRequest(indexs)
            {
                Query = new BoolQuery
                {
                    Should = fields
                     .Select(field => Query.Match(
                         new MatchQuery(new Field(field)) { Query = query })
                     )
                     .ToList()
                },
                Highlight = new Highlight
                {
                    Fields = fields.ToDictionary(
                     field => new Field(field),
                     _ => new HighlightField
                     {
                         FragmentSize = 50,
                         NumberOfFragments = 1,
                         NoMatchSize = 50
                     })
                },
                From = (pagination.PageNumber - 1) * pagination.PageSize,
                Size = pagination.PageSize
            };

            var response = await client.SearchAsync<TDoc>(request);

            var items = new List<SearchResultItem<TDoc>>();
            items.AddRange(response.Hits.Select(hit => new SearchResultItem<TDoc>
            {
                Index = hit.Index,
                Score = hit.Score,
                Source = hit.Source,
                HighLight = hit.Highlight
            }));

            return new SearchResult<TDoc>(items, response.Total, pagination);
        }
    }
}
