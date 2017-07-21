using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Utility {
    public static class URLs {

        public static string PathAndQuery(this HttpRequest request) {
            return request.QueryString.HasValue ? $"{request.Path}{request.QueryString}" : request.Path.ToString();
        }

    }

}
