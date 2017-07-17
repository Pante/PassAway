using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace PassAway.Models
{
    public class Customer
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }




        public string CustomerID { get; set; }

        public string Name { get; set; }
        

        public string Address { get; set; }

        public DateTime dob { get;set; }

        public char gender { get; set; }

        public string telNo { get; set; }

        public string country { get; set; }

    }
}