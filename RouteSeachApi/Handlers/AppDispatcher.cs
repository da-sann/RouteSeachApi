using System;
using MediatR;
using RouteSeachApi.Interfaces.Services;

namespace RouteSeachApi.Handlers {
    public class AppDispatcher : Mediator, IAppDispatcher {
        public AppDispatcher(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
