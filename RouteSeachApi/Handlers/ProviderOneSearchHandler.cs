using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RouteSeachApi.Interfaces.Services;
using RouteSeachApi.Models.Requests;
using RouteSeachApi.Models.Responses;

namespace RouteSeachApi.Handlers {
    public class ProviderOneSearchHandler : IRequestHandler<ProviderOneSearchRequest, ProviderOneSearchResponse> {
        public ProviderOneSearchHandler(IProviderOneSearchService searchService, IMapper mapper) {
            _searchService = searchService;
            _mapper = mapper;
        }
        private readonly IProviderOneSearchService _searchService;
        private readonly IMapper _mapper;

        public async Task<ProviderOneSearchResponse> Handle(ProviderOneSearchRequest request, CancellationToken cancellationToken) {
            var searchRequest = _mapper.Map<SearchRequest>(request);
            var response = await _searchService.SearchAsync(searchRequest, cancellationToken);
            return _mapper.Map<ProviderOneSearchResponse>(response);
        }
    }
}
