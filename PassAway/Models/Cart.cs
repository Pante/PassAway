using PassAway.Models.Shared;

using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace PassAway.Models {

    public class Cart {

        
        public int ID { get; set; }
        public List<CartItem> Items { get; }


        public Cart() {
            Items = new List<CartItem>();
        }


        public virtual void Add(Product product, int quantity) {
            var item = Items.Where(i => i.Product.ID == product.ID).FirstOrDefault();
            if (item != null) {
                item.Quantity += quantity;

            } else {
                Items.Add(new CartItem() {
                    Product = product,
                    Quantity = quantity
                });
            }
        }

        public virtual void Remove(Product product) {
            Items.RemoveAll(item => item.Product.ID == product.ID);
        }


        public virtual decimal GetTotalPrice() {
            return Items.Sum(item => item.Product.Price * item.Quantity);
        }


        public virtual void Clear() {
            Items.Clear();
        }



        public class CartItem {

            public int ID { get; set; }

            public Product Product { get; set; }
            public int Quantity { get; set; }

        }

    }

}
