using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RouteSeachApi.Data.Criterias;
using RouteSeachApi.Data.Entities;
using RouteSeachApi.Interfaces.Data.Repositories;
using RouteSeachApi.Interfaces.Services;
using RouteSeachApi.Models.Requests;
using RouteSeachApi.Models.Responses;

namespace RouteSeachApi.Services {
    public class ProviderTwoSearchService : IProviderTwoSearchService {
        public ProviderTwoSearchService(IRouteRepository routeRepository, IMapper mapper, ICacheService<IProviderTwoSearchService, Guid, Route> cacheService, bool isAvaliable) {
            _routeRepository = routeRepository;
            _isAvailable = isAvaliable;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        private readonly IRouteRepository _routeRepository;
        private readonly bool _isAvailable;
        private readonly IMapper _mapper;
        private readonly ICacheService<IProviderTwoSearchService, Guid, Route> _cacheService;

        public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken) {
            await Task.CompletedTask;
            return _isAvailable;
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken) {
            var search = _mapper.Map<RouteSearch>(request);
            var result = new SearchResponse();
            Route[] routes = null;

            if (search.OnlyCached == true) {
                var cachedData = _cacheService.GetAll();
                var filteredRoutes = cachedData.Where(search.BuildCriteria().Compile()).ToArray();
                routes = filteredRoutes;
            }
            else {
                routes = await _routeRepository.Where(search.BuildCriteria()).ToArrayAsync(cancellationToken);
            }

            result.Routes = routes;
            if (routes.Any()) {
                result.MaxPrice = routes.Max(r => r.Price);
                result.MinPrice = routes.Min(r => r.Price);
                result.MaxMinutesRoute = routes.Max(r => r.GetRouteMinutes());
                result.MinMinutesRoute = routes.Min(r => r.GetRouteMinutes());

                if (search.OnlyCached != true) {
                    foreach (var r in routes) {
                        _cacheService.AddOrUpdate(r.Id, (v) => r, (k, v) => r);
                    }
                }

            }
            return result;
        }
    }
}
