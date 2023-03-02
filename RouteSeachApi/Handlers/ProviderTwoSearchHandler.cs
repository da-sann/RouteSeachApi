using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RouteSeachApi.Interfaces.Services;
using RouteSeachApi.Models.Requests;
using RouteSeachApi.Models.Responses;

namespace RouteSeachApi.Handlers {
    public class ProviderTwoSearchHandler : IRequestHandler<ProviderTwoSearchRequest, ProviderTwoSearchResponse> {
        public ProviderTwoSearchHandler(IProviderTwoSearchService searchService, IMapper mapper) {
            _searchService = searchService;
            _mapper = mapper;
        }
        private readonly IProviderTwoSearchService _searchService;
        private readonly IMapper _mapper;

        public async Task<ProviderTwoSearchResponse> Handle(ProviderTwoSearchRequest request, CancellationToken cancellationToken) {
            var searchRequest = _mapper.Map<SearchRequest>(request);
            var response = await _searchService.SearchAsync(searchRequest, cancellationToken);
            return _mapper.Map<ProviderTwoSearchResponse>(response);
        }
    }
}
