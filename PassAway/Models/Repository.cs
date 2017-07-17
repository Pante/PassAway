using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassAway.Models
{
    public static class Repository
    {
        private static List<Customer> customers = new List<Customer>();

        public static IEnumerable<Customer> Customers { get { return customers; } }

        public static void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }
    }
}