using PassAway.Models.Shared;

using System.Collections.Generic;


namespace PassAway.Models.ViewModels {
    public class OrderRepository {

        private ApplicationContext context;


        public OrderRepository(ApplicationContext context) {
            this.context = context;
        }


        public IEnumerable<Order> Orders => context.Orders;

    }

}
