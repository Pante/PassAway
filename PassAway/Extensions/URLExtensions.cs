using Microsoft.AspNetCore.Http;


namespace PassAway.Extensions {

    public static class URLExtensions {

        public static string PathAndQuery(this HttpRequest request) {
            return request.QueryString.HasValue ? $"{request.Path}{request.QueryString}" : request.Path.ToString();
        }

    }

}
