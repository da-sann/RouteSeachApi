using System;
using MediatR;
using System.Threading.Tasks;
using RouteSeachApi.Models.Requests;
using System.Threading;
using RouteSeachApi.Interfaces.Services;

namespace RouteSeachApi.Handlers {
    public class ProviderTwoAvailableHandler : IRequestHandler<ProviderTwoAvailableRequest, bool> {
        public ProviderTwoAvailableHandler(IProviderTwoSearchService searchService) {
            _searchService = searchService;
        }
        private readonly IProviderTwoSearchService _searchService;

        public Task<bool> Handle(ProviderTwoAvailableRequest request, CancellationToken cancellationToken) {
            return _searchService.IsAvailableAsync(cancellationToken);
        }
    }
}
