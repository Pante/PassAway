using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PassAway.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PassAway.Models.Shared;

namespace PassAway.Models {
    public class SessionCart : Cart {

        public static Cart GetCart(IServiceProvider services) {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            SessionCart cart = session?.GetJSON<SessionCart>("Cart")
                ?? new SessionCart();
            cart.Session = session;
            return cart;
        }


        [JsonIgnore]
        public ISession Session { get; set; }


        public override void Add(Product product, int quantity) {
            base.Add(product, quantity);
            Session.SetJSON("Cart", this);
        }

        public override void Remove(Product product) {
            base.Remove(product);
            Session.SetJSON("Cart", this);
        }

        public override void Clear() {
            base.Clear();
            Session.Remove("Cart");
        }

    }

}
