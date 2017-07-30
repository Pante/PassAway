using PassAway.Models.Shared;

using System.Collections.Generic;


namespace PassAway.Models.ViewModels {
    public class ProductsViewModel {

        public IEnumerable<Product> Products { get; set; }
        public Pagination Pagination { get; set; }

    }

}
