using System.Collections.Generic;


namespace PassAway.Models.ViewModels {

    public class ListElementViewModelRepository : IElementViewModelRepository {

        private List<ElementViewModel> elements;


        public ListElementViewModelRepository() {
            elements = new List<ElementViewModel>();
        }


        public IEnumerable<ElementViewModel> Elements => elements;

    }
}
