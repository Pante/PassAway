using System.Collections.Generic;


namespace PassAway.Models.ViewModels {
    public interface IElementViewModelRepository {

        IEnumerable<ElementViewModel> Elements { get;}

    }

}
