using Microsoft.AspNetCore.Mvc;
using PassAway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Components {

    public class CartSummaryViewComponent : ViewComponent {

        private Cart cart;


        public CartSummaryViewComponent(Cart cart) {
            this.cart = cart;
        }


        public IViewComponentResult Invoke() {
            return View(cart);
        }

    }

}
