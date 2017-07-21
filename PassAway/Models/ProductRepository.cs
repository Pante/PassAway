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

    }

}
