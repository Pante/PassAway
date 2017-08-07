using System.Collections.Generic;

using static PassAway.Models.ViewModels.ElementViewModel;


namespace PassAway.Models.ViewModels {

    public class ElementViewModelRepository {

        private List<ElementViewModel> elements;


        public ElementViewModelRepository() {
            elements = new List<ElementViewModel>() {
                Home("Home", "Index"),
                Account("Profile", "Validate", AUTHENTICATED),
                Home("About", "About"),
                Home("Contact", "Contact"),
                Account("Login", "Login", ANONYMOUS),
                Account("Register", "Register", ANONYMOUS),
                new FormElementViewModel {Name = "Logout", Area = "", Controller = "Account", Action = "Logout", AllowedRoles = AUTHENTICATED },
                new ElementViewModel { Name = "Edit Roles", Area = "", Controller = "RoleAdmin", Action = "Index", AllowedRoles = ADMINISTRATOR },
                new ElementViewModel { Name = "Products", Area = "", Controller = "Product", Action = "List", AllowedRoles = ALL_ROLES},
                new ElementViewModel { Name = "Create Products", Area = "", Controller = "AdminProduct", Action = "Create", AllowedRoles = ADMINISTRATOR},
                new ElementViewModel { Name = "Edit Products", Area = "", Controller = "AdminProduct", Action = "List", AllowedRoles = ADMINISTRATOR},
            };
        }


        public IEnumerable<ElementViewModel> Elements => elements;

    }

}
