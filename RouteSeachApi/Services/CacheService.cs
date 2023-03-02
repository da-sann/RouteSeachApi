using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading;
using System.Collections.Generic;
using RouteSeachApi.Interfaces.Services;

namespace RouteSeachApi.Services {
    public class CacheService<Tuser, TKey, TVal> : ICacheService<Tuser, TKey, TVal> {
        public CacheService() {
            _inner = new ConcurrentDictionary<TKey, Lazy<TVal>>();
        }

        private readonly ConcurrentDictionary<TKey, Lazy<TVal>> _inner;

        public TVal GetOrAdd(TKey key, Func<TKey, TVal> valueFactory) {
            var lazyResult = _inner.GetOrAdd(key, k => new Lazy<TVal>(() => valueFactory(k), LazyThreadSafetyMode.ExecutionAndPublication));
           
            return lazyResult.Value;
        }

        public TVal AddOrUpdate(TKey key, Func<TKey, TVal> addValueFactory, Func<TKey, TVal, TVal> updateValueFactory) {
            var lazyResult = _inner.AddOrUpdate(key, new Lazy<TVal>(() => addValueFactory(key)), (key2, old) => new Lazy<TVal>(() => updateValueFactory(key2, old.Value)));

            return lazyResult.Value;
        }

        public IEnumerable<TVal> GetAll() {
           return _inner.Values.Where(v => v.IsValueCreated).Select(v => v.Value);
        }
    }
}
