using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Models {
    public class Pagination {

        public int TotalItems { get; set; }
        public int Current { get; set; }
        public int Size { get; set; }


        public int GetTotalPages() {
            return (int) Math.Ceiling((decimal)TotalItems / Size);
        }
    }

}
