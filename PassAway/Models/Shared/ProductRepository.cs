using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Models.Shared {
    public class ProductRepository {

        private ApplicationContext context;


        public ProductRepository(ApplicationContext context) {
            this.context = context;
        }


        public IEnumerable<Product> Products => context.Products;

        public void SaveProduct(Product product)
        {
            if (product.ID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products
                .FirstOrDefault(p => p.ID == product.ID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Stock = product.Stock;
                    dbEntry.Price = product.Price;
                }
            }
            context.SaveChanges();
        }

    }

}
