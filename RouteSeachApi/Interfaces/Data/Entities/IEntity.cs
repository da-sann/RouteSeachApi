using System;

namespace RouteSeachApi.Interfaces.Data.Entities {
    public interface IEntity<TKey> {
        TKey Id { get; set; }
    }
}
