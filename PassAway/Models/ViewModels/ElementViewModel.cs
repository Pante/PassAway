using System.Collections.Generic;


namespace PassAway.Models.ViewModels {
    public class ElementViewModel {

        public static List<string> ALL_ROLES = new List<string> { "", "Administrator", "Customer" };
        public static List<string> AUTHENTICATED = new List<string> { "Administrator", "Customer" };
        public static List<string> ADMINISTRATOR = new List<string> { "Administrator" };


        public string Name { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public List<string> AllowedRoles { get; set; }

    }

}
