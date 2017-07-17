using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassAway.Models
{
    public interface IRepository
    {
        IEnumerable<Staff> Staffs { get; }
        IEnumerable<Product> Products { get; }
        IEnumerable<Transaction> Transactions { get; }
        IEnumerable<Customer> Customers { get; }

    }
}
