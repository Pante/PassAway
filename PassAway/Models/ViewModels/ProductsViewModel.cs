using PassAway.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Models.ViewModels {
    public class ProductsViewModel {

        public IEnumerable<Product> Products { get; set; }
        public Pagination Pagination { get; set; }

    }

}
