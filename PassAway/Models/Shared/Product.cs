using System;
using System.ComponentModel.DataAnnotations;


namespace PassAway.Models.Shared {
    public class Product {

        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }
        //public string Description { get; set; }
        public string URL { get; set; }

        public DateTime Launched { get; set; }

        [Required(ErrorMessage = "Please enter a Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter the stock amount")]
        public int Stock { get; set; }
        public Rating Rating { get; set; }

    }

}
