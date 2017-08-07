using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Models.ViewModels {

    public class CreateProductViewModel {

        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }

        [Required(ErrorMessage = "Please enter a product date")]
        public string Produced { get; set; }

        [Required(ErrorMessage = "Please enter a Price")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Please enter the stock amount")]
        public string Stock { get; set; }

        public string Promotion { get; set; }

    }

}
