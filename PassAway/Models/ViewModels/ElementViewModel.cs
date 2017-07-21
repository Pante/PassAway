using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Models.ViewModels {
    public class ElementViewModel {

        public string Name { get; set; }
        public string URL { get; set; }
        public List<string> AllowedRoles { get; set; }

    }

}
