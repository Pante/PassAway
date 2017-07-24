using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PassAway.Models.Shared {
    public class Product {

        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }
        public string URL { get; set; }

        public DateTime Launched { get; set; }

        [Required(ErrorMessage = "Please enter a Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter the stock amount")]
        public int Stock { get; set; }
        public Rating Rating { get; set; }

    }

}
