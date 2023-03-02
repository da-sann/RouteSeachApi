using System;
using MediatR;
using System.Threading.Tasks;
using RouteSeachApi.Models.Requests;
using System.Threading;
using RouteSeachApi.Interfaces.Services;

namespace RouteSeachApi.Handlers {
    public class ProviderOneAvailableHandler : IRequestHandler<ProviderOneAvailableRequest, bool> {
        public ProviderOneAvailableHandler(IProviderOneSearchService searchService) {
            _searchService = searchService;
        }
        private readonly IProviderOneSearchService _searchService;

        public Task<bool> Handle(ProviderOneAvailableRequest request, CancellationToken cancellationToken) {
            return _searchService.IsAvailableAsync(cancellationToken);
        }
    }
}
