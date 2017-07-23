using PassAway.Models.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Models {
    public class Item {

        public User Postedby { get; set; }


        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        public DateTime Posted { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }

}
