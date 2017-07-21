using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Utility {
    public static class Sessions {

        public static void SetJSON(this ISession session, string key, object value) {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJSON<T>(this ISession session, string key) {
            var data = session.GetString(key);
            return data == null ? default(T) : JsonConvert.DeserializeObject<T>(data);
        }

    }

}
