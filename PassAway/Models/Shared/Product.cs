using System;
using System.ComponentModel.DataAnnotations;


namespace PassAway.Models.Shared {

    public class Product {

        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }

        public DateTime Produced { get; set; }
        public string Region { get; set; }
        public string Type { get; set; }

        [Required(ErrorMessage = "Please enter a Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter the stock amount")]
        public int Stock { get; set; }

        public bool Promotion { get; set; }

    }

}
