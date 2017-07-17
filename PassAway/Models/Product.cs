using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassAway.Models
{
    public class Product
    {
        public string productID { get; set; }

        public string name { get; set; }

        public double price { get; set; }

        public double inventorycount { get; set; }

        public DateTime launchDate {get;set;}


    }
}