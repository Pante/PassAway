using System.Collections.Generic;


namespace PassAway.Models.ViewModels {

    public class ListElementViewModelRepository : IElementViewModelRepository {

        private List<ElementViewModel> elements;


        public ListElementViewModelRepository() {
            elements = new List<ElementViewModel>() {
                Home("Home", "Index"),
                Home("About", "About"),
                Home("Contact", "Contact"),
                Account("Login", "Login", ElementViewModel.ANONYMOUS),
                Account("Register", "Register", ElementViewModel.ANONYMOUS),
                new FormElementViewModel {Name = "Logout", Area = "", Controller = "Account", Action = "Logout", AllowedRoles = ElementViewModel.AUTHENTICATED },
                new ElementViewModel { Name = "Edit Roles", Area = "", Controller = "RoleAdmin", Action = "Index", AllowedRoles = ElementViewModel.ADMINISTRATOR },
                new ElementViewModel { Name = "Products", Area = "", Controller = "Product", Action = "List", AllowedRoles = ElementViewModel.ALL_ROLES},
                new ElementViewModel { Name = "Edit Products", Area = "", Controller = "AdminProduct", Action = "Product", AllowedRoles = ElementViewModel.ADMINISTRATOR}
            };
        }


        private ElementViewModel Account(string name, string action, List<string> roles) {
            return new ElementViewModel() {
                Name = name,
                Area = "",
                Controller = "Account",
                Action = action,
                AllowedRoles = roles
            };
        }

        private ElementViewModel Home(string name, string action) {
            return new ElementViewModel() {
                Name = name,
                Area = "",
                Controller = "Home",
                Action = action,
                AllowedRoles = ElementViewModel.ALL_ROLES
            };
        }


        public IEnumerable<ElementViewModel> Elements => elements;

    }


    public class FormElementViewModel : ElementViewModel {

    }

}
