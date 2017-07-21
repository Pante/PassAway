using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Models.ViewModels {
    public class ListElementViewModelRepository : IElementViewModelRepository {

        private List<ElementViewModel> elements;


        public ListElementViewModelRepository() {
            elements = new List<ElementViewModel>();
        }


        public IEnumerable<ElementViewModel> Elements => elements;

    }
}
