using Microsoft.AspNetCore.Mvc;
using PassAway.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassAway.Components {
    public class NavigationBarViewComponent : ViewComponent {

        private IElementViewModelRepository repository;

        
        public NavigationBarViewComponent(IElementViewModelRepository repository) {
            this.repository = repository;
        }


        public IViewComponentResult Invoke() {
            return View(repository.Elements.Where(element => element.AllowedRoles.Where(role => HttpContext.User.IsInRole(role)).Count() != 0));
        }

    }

}
