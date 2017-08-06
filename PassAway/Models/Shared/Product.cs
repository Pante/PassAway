using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PassAway.Models.Shared {

    public class Product {

        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }

        public DateTime Produced { get; set; }

        [Required(ErrorMessage = "Please enter a Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter the stock amount")]
        public int Stock { get; set; }

        public bool Promotion { get; set; }


        [ForeignKey("ProductID")]
        public List<Rating> Ratings { get; set; }


        public double GetProductRating() {
            return Ratings.Sum(rating => rating.Amount) / Ratings.Count();
        }

    }

}
