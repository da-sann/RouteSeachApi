using RouteSeachApi.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteSeachApi.Services {
    public class SingleTenantProvider : IShardNameProvider {

        private string _key = String.Empty;
        public string GetKey() => _key;
        public void SetKey(string key) => _key = key;
    }
}
