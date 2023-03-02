using RouteSeachApi.Interfaces.Data;
using System;

namespace RouteSeachApi.Services {
    public class ShardNameProvider : IShardNameProvider {

        private string _key = String.Empty;
        public string GetKey() => _key;
        public void SetKey(string key) => _key = key;
    }
}
