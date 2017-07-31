using System.Collections.Generic;


namespace PassAway.Models.ViewModels {

    public class ListElementViewModelRepository : IElementViewModelRepository {

        private List<ElementViewModel> elements;


        public ListElementViewModelRepository() {
            elements = new List<ElementViewModel>() {
                Home("Home", "Index"),
                Home("About", "About"),
                Home("Contact", "Contact"),
                Home("Login", "Login"),
                new ElementViewModel { Name = "Register", Area = "", Controller = "Account", Action = "Register", AllowedRoles = ElementViewModel.ALL_ROLES },
                new ElementViewModel { Name = "Edit Users", Area = "", Controller = "Admin", Action = "Index", AllowedRoles = ElementViewModel.ADMINISTRATOR },
                new ElementViewModel { Name = "Products", Area = "", Controller = "Product", Action = "List", AllowedRoles = ElementViewModel.ALL_ROLES},
                new ElementViewModel { Name = "Edit Products", Area = "", Controller = "AdminProduct", Action = "Product", AllowedRoles = ElementViewModel.ADMINISTRATOR}
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
}
