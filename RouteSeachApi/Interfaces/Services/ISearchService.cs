using System;
using System.Threading;
using System.Threading.Tasks;
using RouteSeachApi.Models.Requests;
using RouteSeachApi.Models.Responses;

namespace RouteSeachApi.Interfaces {
    public interface ISearchService {
        Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken);
        Task<bool> IsAvailableAsync(CancellationToken cancellationToken);
    }
}
