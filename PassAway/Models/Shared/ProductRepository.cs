using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Models.Shared {
    public class ProductRepository {

        private ProductContext context;


        public ProductRepository(ProductContext context) {
            this.context = context;
        }


        public IEnumerable<Product> Products => context.Products;

    }

}
