using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using PassAway.Models;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;


namespace PassAway.Controllers {

    [Authorize(Roles = "Adminstrator")]
    public class RoleAdminController : MasterController {

        private RoleManager<IdentityRole> roles;
        private UserManager<User> users;


        public RoleAdminController(RoleManager<IdentityRole> roles, UserManager<User> users) {
            this.roles = roles;
            this.users = users;
        }


        public ViewResult Index() {
            return View(roles.Roles);
        }

        public IActionResult Create() {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Required] string name) {
            if (ModelState.IsValid && await IsSuccessfulAsync(roles.CreateAsync(new IdentityRole(name)))) {
                 return RedirectToAction("Index");

            } else {
                return View(name);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id) {
            var role = await Exists(roles.FindByIdAsync(id));

            if (role != null && await IsSuccessfulAsync(roles.DeleteAsync(role))) {
                return RedirectToAction("Index");

            } else {
                return View("Index", roles.Roles);
            }
        }

        public async Task<IActionResult> Edit(string id) {
            var role = await roles.FindByIdAsync(id);
            var members = new List<User>();
            var nonMembers = new List<User>();

            foreach (User user in users.Users) {
                var list = await users.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }

            return View(new RoleEditModel {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleModificationModel model) {
            if (ModelState.IsValid) {

                foreach (var id in model.IdsToAdd ?? new string[] { }) {
                    User user = await users.FindByIdAsync(id);
                    if (user != null) {
                        await IsSuccessfulAsync(users.AddToRoleAsync(user, model.RoleName));
                    }
                }

                foreach (var id in model.IdsToDelete ?? new string[] { }) {
                    User user = await users.FindByIdAsync(id);
                    if (user != null) {
                        await IsSuccessfulAsync(users.RemoveFromRoleAsync(user, model.RoleName));
                    }
                }
            }

            if (ModelState.IsValid) {
                return RedirectToAction(nameof(Index));

            } else {
                return await Edit(model.RoleId);
            }
        }

    }

}
