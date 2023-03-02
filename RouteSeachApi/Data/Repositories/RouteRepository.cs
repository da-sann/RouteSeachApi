using System;
using RouteSeachApi.Data.Entities;
using RouteSeachApi.Interfaces.Data;
using RouteSeachApi.Interfaces.Data.Repositories;

namespace RouteSeachApi.Data.Repositories {
    public class RouteRepository : BaseRepository<Route, Guid>, IRouteRepository {
        public RouteRepository(IDbContext context) : base(context) { }
    }
}
