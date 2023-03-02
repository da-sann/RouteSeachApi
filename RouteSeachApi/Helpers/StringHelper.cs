using System;
using System.Linq;

namespace RouteSeachApi.Helpers {
    public static class StringHelper {
        public static string ReplaceAll(this string seed, string[] chars, string replacementCharacter) {
            return chars.Aggregate(seed, (str, cItem) => str.Replace(cItem, replacementCharacter));
        }
    }
}
