using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;


namespace PassAway.Extensions {

    public static class SessionExtensions {

        public static void SetJSON(this ISession session, string key, object value) {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJSON<T>(this ISession session, string key) {
            var data = session.GetString(key);
            return data == null ? default(T) : JsonConvert.DeserializeObject<T>(data);
        }

    }

}
