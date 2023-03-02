using System;

namespace RouteSeachApi.Interfaces.Data {
    public interface IShardNameProvider {
        string GetKey();
        void SetKey(string key);
    }
}
