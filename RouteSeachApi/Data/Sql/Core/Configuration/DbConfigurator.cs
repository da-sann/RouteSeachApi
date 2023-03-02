using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RouteSeachApi.Interfaces.Data.Configuration;

namespace RouteSeachApi.Data.Sql.Core.Configuration {
    public static class DbConfigurator {
        static DbConfigurator() {
            _configurationTypes = new ConcurrentDictionary<Assembly, Type[]>();
        }
        private static readonly ConcurrentDictionary<Assembly, Type[]> _configurationTypes;

        public static void Register(Assembly assembly) {
            var types = assembly.GetTypes().Where(a => !a.IsAbstract && !a.IsGenericType && a.Name.EndsWith("Configuration") && typeof(IEntityTypeConfiguration).IsAssignableFrom(a)).ToArray();
            _configurationTypes.AddOrUpdate(assembly, types, (a, o) => o);
        }

        public static void Configure<TContext>(ModelBuilder builder) where TContext : DbContext {
            if (_configurationTypes.TryGetValue(typeof(TContext).Assembly, out Type[] types))
                Array.ForEach(types, t => {
                    var obj = (IEntityTypeConfiguration)Activator.CreateInstance(t);
                    obj.Configure(builder);
                });
        }
    }
}
