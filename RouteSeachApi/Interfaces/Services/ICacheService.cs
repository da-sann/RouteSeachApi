using System;
using System.Collections.Generic;

namespace RouteSeachApi.Interfaces.Services {
    public interface ICacheService<Tuser, TKey, TVal> {
        public TVal GetOrAdd(TKey key, Func<TKey, TVal> valueFactory);
        public TVal AddOrUpdate(TKey key, Func<TKey, TVal> addValueFactory, Func<TKey, TVal, TVal> updateValueFactory);
        public IEnumerable<TVal> GetAll();
    }
}
