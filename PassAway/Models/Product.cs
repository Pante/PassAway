using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Models.Shared {
    public class Product {

        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        public DateTime Launched { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }

    }

}
