using System;
using Autofac;
using Microsoft.AspNetCore.Http;
using RouteSeachApi.Data.Entities;
using RouteSeachApi.Interfaces.Services;
using RouteSeachApi.Services;

namespace RouteSeachApi.Modules {
    public class ProviderServiceModule : Module {
        protected override void Load(ContainerBuilder builder) {
            base.Load(builder);
            var random = new Random();
            var oneAvaliable = random.Next(2) == 1;
            builder.RegisterType<ProviderOneSearchService>().As<IProviderOneSearchService>()
                .WithParameter("isAvaliable", oneAvaliable);
            var twoAvaliable = random.Next(2) == 1;
            builder.RegisterType<ProviderTwoSearchService>().As<IProviderTwoSearchService>()
                .WithParameter("isAvaliable", twoAvaliable);
            builder.RegisterInstance(new HttpContextAccessor()).As<IHttpContextAccessor>();

            builder.RegisterType<CacheService<IProviderOneSearchService, Guid, Route>>().As<ICacheService<IProviderOneSearchService, Guid, Route>>().SingleInstance();
            builder.RegisterType<CacheService<IProviderTwoSearchService, Guid, Route>>().As<ICacheService<IProviderTwoSearchService, Guid, Route>>().SingleInstance();
        }
    }
}
