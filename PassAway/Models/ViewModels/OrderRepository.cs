using PassAway.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Models.ViewModels {
    public class OrderRepository {

        private ApplicationContext context;


        public OrderRepository(ApplicationContext context) {
            this.context = context;
        }


        public IEnumerable<Order> Orders => context.Orders;

    }

}
