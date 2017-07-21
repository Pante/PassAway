using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Models.ViewModels {
    public interface IElementViewModelRepository {

        IEnumerable<ElementViewModel> Elements { get;}

    }

}
