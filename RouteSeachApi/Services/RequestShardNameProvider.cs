using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using RouteSeachApi.Helpers;
using RouteSeachApi.Interfaces.Data;

namespace RouteSeachApi.Services {
    public class RequestShardNameProvider : IShardNameProvider {
        private const string _pattern = "provider-(.*?)\\/";
        private string[] _redundant = new string[] { "/", "-" };
        public RequestShardNameProvider(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string GetKey() {
            var regex = new Regex(_pattern);
            var test = _httpContextAccessor.HttpContext.Request.Path;
            var match = regex.Match(test);
            return match.Success ? match.Value.ReplaceAll(_redundant, string.Empty) : string.Empty;
        }

        public void SetKey(string key) {
           /* if (_httpContextAccessor.HttpContext.GetRouteData().Values.Keys.Contains(TenantKey))
                _httpContextAccessor.HttpContext.GetRouteData().Values[TenantKey] = key;
            else
                _httpContextAccessor.HttpContext.GetRouteData().Values.Add(TenantKey, key);*/
        }
    }
}
