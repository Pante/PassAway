using System.Collections.Generic;


namespace PassAway.Models.ViewModels {
    public class ElementViewModel {

        public string Name { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public List<string> AllowedRoles { get; set; }

    }

}
