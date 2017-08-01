using System.Collections.Generic;


namespace PassAway.Models.ViewModels {

    public class FormElementViewModel : ElementViewModel {

    }

    public class ElementViewModel {

        public static List<string> ALL_ROLES = new List<string> { "", "Admins", "Customers" };
        public static List<string> AUTHENTICATED = new List<string> { "Admins", "Customers" };
        public static List<string> ADMINISTRATOR = new List<string> { "Admins" };
        public static List<string> ANONYMOUS = new List<string> { "" };


        public static ElementViewModel Account(string name, string action, List<string> roles) {
            return new ElementViewModel() {
                Name = name,
                Area = "",
                Controller = "Account",
                Action = action,
                AllowedRoles = roles
            };
        }

        public static ElementViewModel Home(string name, string action) {
            return new ElementViewModel() {
                Name = name,
                Area = "",
                Controller = "Home",
                Action = action,
                AllowedRoles = ALL_ROLES
            };
        }


        public string Name { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public List<string> AllowedRoles { get; set; }

    }

}
