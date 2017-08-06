using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Models {
    public class Rating {
       
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string Email { get; set; }
        public double Amount { get; set; }

    }

}
